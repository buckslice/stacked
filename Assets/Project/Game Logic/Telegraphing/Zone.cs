using UnityEngine;
using System.Collections;

public enum ZoneShape {
    SQUARE,
    CIRCLE
}

public enum ZoneType {
    HEALING_OVER_TIME,
    DAMAGE_OVER_TIME,
    EXPLODE_ON_CONTACT,
    EXPLODE_AFTER_TIMER
}

public enum FlightType {
    LINEAR,
    LERP,
    ARC,
    RANDOM,
}

public class Zone : MonoBehaviour {
    // WARNING : THIS CLASS USES CUSTOM EDITOR SO NEW VARIABLES WONT APPEAR WITHOUT ADDING TO THAT CLASS
    public ZoneShape shape;
    public ZoneType type;
    public float healthChange = 1.0f;
    public float duration = 10.0f;  // changes meaning depending on zonetype

    public ParticleSystem mainParticles;

    public bool isTelegraphed = false;
    public ParticleSystem telegraphParticles;
    public float telegraphDuration = 3.0f;

    public bool fliesToDestination = false;
    public ParticleSystem flightParticles;
    public float flightDuration = 2.0f;
    public FlightType flightType;
    Vector3 destination;

    // todo: add flexibility to work with gameobjects that have subparticle systems
    // todo: add velocity / movement so you can have death lines that you have to jump over or wandering fires

    float durTimer = 0.0f;

    Collider[] buffer = new Collider[32];
    Collider col;   // this zones collider

    bool dying = false;
    bool setup = false; // for auto setup if Setup() is not called

    // Use this for initialization
    void Awake() {
        Debug.Assert(mainParticles, "Zone needs a particle system prefab");

        if (shape == ZoneShape.SQUARE) {
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

        if (shape == ZoneShape.SQUARE) {
            ps.transform.localPosition = Vector3.up * 0.5f;
            ps.transform.localRotation = Quaternion.identity;
            ps.transform.localScale = Vector3.one;
            pssm.shapeType = ParticleSystemShapeType.Box;
        } else if (shape == ZoneShape.CIRCLE) { // circle uses sphere collider for now, could add in custom puck shaped collider
            ps.transform.localPosition = Vector3.zero;
            ps.transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            ps.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
            if (pssm.shapeType != ParticleSystemShapeType.Circle) { // todo: need to figure out better way to handle different kinds of shapes
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

    // sets position and size of zone
    // position is specified by the bottom center
    // size is width height length
    public void Setup(Vector3 position, Vector3 scale, Vector3 destination) {
        if (shape == ZoneShape.CIRCLE) { // makes sure scale vector for circles is uniform
            float maxDim = Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z));
            scale = Vector3.one * maxDim;
        }

        // set new position and scale
        transform.position = position;
        transform.localScale = scale;
        this.destination = destination;

        Vector3 colSize = col.bounds.size;
        float ySize = shape == ZoneShape.CIRCLE ? 1.0f : colSize.y;
        float colVolume = colSize.x * ySize * colSize.z;

        // setup main particles
        SetupParticleSystem(ref mainParticles, colVolume);

        if (fliesToDestination) {
            flightParticles = (ParticleSystem)Instantiate(flightParticles, transform, false);
            flightParticles.Play();
        }else if (isTelegraphed) {
            SetupParticleSystem(ref telegraphParticles, colVolume);
            telegraphParticles.Play();
        } else {
            mainParticles.Play();  // play on awake is disabled since emission rate is being set after initialization
            // this is so the prewarm option will work correctly (prewarm is still optional though)
        }

        setup = true;
    }

    // Update is called once per frame
    void Update() {
        if (dying) {
            return;
        }
        if (!setup) {   // if not setup by now then just call setup with current transform
            Setup(transform.position, transform.localScale);
        }

        if (fliesToDestination) {
            durTimer += Time.deltaTime;
            switch (flightType) {
                default:
                case FlightType.LINEAR:

                    break;
            }
        }


        if (isTelegraphed) {
            durTimer += Time.deltaTime;
            if (durTimer >= telegraphDuration) {
                durTimer = 0.0f;
                isTelegraphed = false;
                telegraphParticles.Stop();
                mainParticles.Play();
            }
        } else {
            durTimer += Time.deltaTime;
            if (durTimer >= duration) {
                if (type == ZoneType.EXPLODE_AFTER_TIMER) {
                    DamageAllPlayersInContact();
                }
                DestroyZone();
            }
        }
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
        if (type == ZoneType.DAMAGE_OVER_TIME || type == ZoneType.HEALING_OVER_TIME) {
            c.GetComponent<Damageable>().Damage(healthChange * Time.fixedDeltaTime);
        } else if (type == ZoneType.EXPLODE_ON_CONTACT) {
            DamageAllPlayersInContact();
            DestroyZone();
        }
    }

    // destroys this object after the particle system ends
    void DestroyZone() {
        if (!dying) {
            dying = true;
            Destroy(col);
            mainParticles.Stop();
            Destroy(gameObject, mainParticles.startLifetime);
        }
    }
}
