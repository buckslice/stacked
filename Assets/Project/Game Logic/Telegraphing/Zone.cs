using UnityEngine;
using System.Collections;

public enum ZoneType {
    STATIONARY, // stays still once placed
    MOVING,     // moves around with given velocity
    EMANATING,  // pulses out from a location (defaults to circle shape)
    //WANDER,   // wanders around randomly from starting position
}

public enum ZoneShape {
    SQUARE,
    CIRCLE
}

public enum ZoneAction {
    HEALING_OVER_TIME,
    DAMAGE_OVER_TIME,
    EXPLODE_ON_CONTACT,
    EXPLODE_AFTER_TIMER
}

public enum FlightType {    // todo make other types of flight movement
    LINEAR,     // move linearly towards target position
    LERP,       // lerps towards target position
    ARC,        // lerps in the xz direction but arcs in the y
    //RANDOM,   // something like an overshooting homing missile, but with random start direction
}

public class Zone : MonoBehaviour {
    // WARNING : THIS CLASS USES CUSTOM EDITOR SO NEW VARIABLES WONT APPEAR WITHOUT ADDING TO THAT CLASS
    public ZoneType type;
    public ZoneShape shape;
    public ZoneAction action;
    public float healthChange = 1.0f;   // depends on zone action
    public float mainDuration = 10.0f;  // depends on zone action

    public ParticleSystem mainParticles;

    public bool isTelegraphed = false;
    public ParticleSystem telegraphParticles;
    public float telegraphDuration = 3.0f;

    public bool fliesToDestination = false;
    public FlightType flightType;
    public ParticleSystem flightParticles;
    public bool speedBasedOnDuration = true;
    public float flightDuration = 2.0f;
    public float flightSpeedHorizontal = 5.0f;
    public float initialYVel = 10.0f;

    public Vector3 velocity;
    public float emanationSpeed = 1.0f;
    public float emanationRadius = 1.0f;    // radius of ring

    Vector3 flightTarget;
    Vector3 flightStart;

    // todo: add flexibility to work with gameobjects that have subparticle systems
    // todo: add velocity / movement so you can have death lines that you have to jump over or wandering fires

    Collider[] buffer = new Collider[32];
    Collider col;   // this zones collider

    bool dying = false;
    bool setup = false; // for auto setup if Setup() is not called

    // Use this for initialization
    void Awake() {
        Debug.Assert(mainParticles, "Zone needs a particle system prefab");

        if (shape == ZoneShape.SQUARE || type == ZoneType.EMANATING) {
            shape = ZoneShape.SQUARE;
            BoxCollider bc = gameObject.AddComponent<BoxCollider>();
            bc.center = Vector3.up * 0.5f;
            col = bc;
        } else if (shape == ZoneShape.CIRCLE) { // circle uses sphere collider for now, could add in custom puck shaped collider
            col = gameObject.AddComponent<SphereCollider>();
        }
        col.isTrigger = true;
    }

    void SetupParticleSystem(ref ParticleSystem ps, float scaleToVolume) {
        // reassign here so we dont edit the original prefab, but instead the copy of the prefab (this is why ref)
        ps = (ParticleSystem)Instantiate(ps, transform, false);

        // save original particle system scale
        // make sure scales are at least 1 and then calc volume
        Vector3 psScale = ps.transform.localScale;
        psScale.x = Mathf.Max(1.0f, psScale.x);
        psScale.y = Mathf.Max(1.0f, psScale.y);
        psScale.z = Mathf.Max(1.0f, psScale.z);
        float origVolume = psScale.x * psScale.y * psScale.z;

        ParticleSystem.ShapeModule pssm = ps.shape;

        if (type == ZoneType.EMANATING) {
            origVolume /= psScale.x;    // doing it by area
            ps.transform.localPosition = Vector3.up * 0.5f;
            ps.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
            ps.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
            pssm.shapeType = ParticleSystemShapeType.CircleEdge;
        } else if (shape == ZoneShape.SQUARE) {
            ps.transform.localPosition = Vector3.up * 0.5f;
            ps.transform.localRotation = Quaternion.identity;
            ps.transform.localScale = Vector3.one;
            pssm.shapeType = ParticleSystemShapeType.Box;
        } else if (shape == ZoneShape.CIRCLE) { // circle uses sphere collider for now, could add in custom puck shaped collider
            ps.transform.localPosition = Vector3.zero;
            ps.transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            ps.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
            if (pssm.shapeType != ParticleSystemShapeType.CircleEdge) { // todo: need to figure out better way to handle different kinds of shapes
                pssm.shapeType = ParticleSystemShapeType.Hemisphere;
            }
        }

        // scales the particle system to the volume of the collider based off its original volume
        ParticleSystem.EmissionModule psem = ps.emission;
        float ratio = scaleToVolume / origVolume;

        // scale rate
        if (psem.rate.constant > 0) {   // only do if constant rate is being used (todo: curves)
            psem.rate = psem.rate.constant * ratio;
        }
        // scale bursts (if any)
        if (psem.burstCount > 0) {
            ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[psem.burstCount];
            psem.GetBursts(bursts);
            for (int i = 0; i < bursts.Length; ++i) {
                bursts[i].minCount = (short)(bursts[i].minCount * ratio);
                bursts[i].maxCount = (short)(bursts[i].maxCount * ratio);
            }
            psem.SetBursts(bursts);
        }
        // scale max particles
        ps.maxParticles = (int)(ps.maxParticles * ratio);

    }

    // helper overloads
    public void Setup(Vector3 position) {
        Setup(position, Vector3.one, position);
    }
    public void Setup(Vector3 position, Vector3 scale) {
        Setup(position, scale, position);
    }
    //public void Setup(Vector3 position, Vector3 scale, Vector3 velocity) {

    //}

    // sets position and size of zone
    // position is specified by the bottom center
    // size is width height length
    public void Setup(Vector3 position, Vector3 scale, Vector3 destination) {
        if (type == ZoneType.EMANATING) {
            scale = Vector3.one;
        } else if (shape == ZoneShape.CIRCLE) { // makes sure scale vector for circles is uniform
            float maxDim = Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z));
            scale = Vector3.one * maxDim;
        }
        flightTarget = destination;
        flightStart = position;

        // set new position and scale
        transform.position = position;
        transform.localScale = scale;

        Vector3 colSize = col.bounds.size;
        float ySize = shape == ZoneShape.CIRCLE ? 1.0f : colSize.y;
        float colVolume = colSize.x * ySize * colSize.z;

        if (fliesToDestination) {
            // no auto scaling done for flight particles yet..
            Vector3 locScale = flightParticles.transform.localScale;
            flightParticles = (ParticleSystem)Instantiate(flightParticles, transform, false);
            flightParticles.transform.localPosition = Vector3.zero;
            flightParticles.transform.localScale = locScale;
        }
        if (isTelegraphed) {
            SetupParticleSystem(ref telegraphParticles, colVolume);
        }
        // setup main particle system
        SetupParticleSystem(ref mainParticles, colVolume);
        // play on awake is disabled since emission rate is being set after initialization
        // this is so the prewarm option will work correctly (prewarm is still optional though)

        setup = true;

        StartCoroutine(MainRoutine());
    }

    // Update is called once per frame
    void Update() {
        if (!setup) {   // if not setup by now then just call setup with current transform
            Setup(transform.position, transform.localScale);
        }
    }

    IEnumerator ArcFlightRoutine() {
        float time = flightDuration;
        if (!speedBasedOnDuration) {
            float xd = flightTarget.x - flightStart.x;
            float zd = flightTarget.z - flightStart.z;
            float xzDist = Mathf.Sqrt(xd * xd + zd * zd);
            time = xzDist / flightSpeedHorizontal;
        }
        float vel = initialYVel;
        float accel = 2 * (-flightTarget.y + flightStart.y + vel * time) / (time * time);
        float t = 0.0f;
        while (t < time) {
            float lerpX = Mathf.Lerp(flightStart.x, flightTarget.x, t / time);
            float lerpZ = Mathf.Lerp(flightStart.z, flightTarget.z, t / time);
            vel -= accel * Time.deltaTime;
            float py = transform.position.y + vel * Time.deltaTime;
            transform.position = new Vector3(lerpX, py, lerpZ);
            t += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator BasicFlightRoutine(bool linear) {
        float t = 0.0f;
        while (t < flightDuration) {
            transform.position = Vector3.Lerp(linear ? flightStart : transform.position, flightTarget, t / flightDuration);
            t += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator EmanationRoutine() {
        // grow zone based upon speed until duration is over
        // increase particle emission based on scale
        // radius can be function of particle duration
        ParticleSystem.EmissionModule psem = mainParticles.emission;
        float c = psem.rate.constant;
        float t = 0.0f;
        while (t < mainDuration) {
            Vector3 scale = transform.localScale;
            float radiusGrowthSpeed = emanationSpeed * 2.0f * Time.deltaTime;
            scale.x += radiusGrowthSpeed;
            scale.z += radiusGrowthSpeed;
            transform.localScale = scale;

            psem.rate = scale.x * c;
            mainParticles.maxParticles = (int)(psem.rate.constant * mainParticles.startLifetime);

            t += Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator MainRoutine() {
        if (fliesToDestination) {
            flightParticles.Play();

            switch (flightType) {
                default:
                case FlightType.ARC:
                    yield return StartCoroutine(ArcFlightRoutine());
                    break;
                case FlightType.LERP:
                    yield return StartCoroutine(BasicFlightRoutine(false));
                    break;
                case FlightType.LINEAR:
                    yield return StartCoroutine(BasicFlightRoutine(true));
                    break;
            }

            fliesToDestination = false;
            flightParticles.Stop();
            transform.position = flightTarget;
        }

        if (isTelegraphed) {
            telegraphParticles.Play();
            yield return new WaitForSeconds(telegraphDuration);
            telegraphParticles.Stop();
            isTelegraphed = false;
        }

        mainParticles.Play();
        if (type == ZoneType.EMANATING) {
            yield return StartCoroutine(EmanationRoutine());
        } else {
            yield return new WaitForSeconds(mainDuration);
        }
        if (action == ZoneAction.EXPLODE_AFTER_TIMER) {
            DamageAllPlayersInContact();
        }
        DestroyZone();
    }

    void DamageAllPlayersInContact() {
        int count = 0;

        if (shape == ZoneShape.SQUARE) {
            Vector3 half = transform.localScale / 2.0f;
            Vector3 worldCenter = transform.position + Vector3.up * half.y;
            count = Physics.OverlapBoxNonAlloc(worldCenter, half, buffer);
        } else {
            float radius = transform.localScale.x / 2.0f;   // each component of scale vector is same
            count = Physics.OverlapSphereNonAlloc(transform.position, radius, buffer);
        }

        for (int i = 0; i < count; ++i) {
            if (buffer[i].CompareTag(Tags.Player)) {
                buffer[i].GetComponent<Damageable>().Damage(healthChange);
            }
        }
    }

    void OnTriggerStay(Collider c) {
        if (isTelegraphed || !c.CompareTag(Tags.Player)) {
            return;
        }
        if (action == ZoneAction.DAMAGE_OVER_TIME || action == ZoneAction.HEALING_OVER_TIME) {
            c.GetComponent<Damageable>().Damage(healthChange * Time.fixedDeltaTime);
        } else if (action == ZoneAction.EXPLODE_ON_CONTACT) {
            DamageAllPlayersInContact();
            DestroyZone();
        }
    }

    // destroys this object after the particle system ends
    void DestroyZone() {
        if (!dying) {
            dying = true;
            StopAllCoroutines();
            Destroy(col);
            mainParticles.Stop();
            Destroy(gameObject, mainParticles.startLifetime);
        }
    }
}
