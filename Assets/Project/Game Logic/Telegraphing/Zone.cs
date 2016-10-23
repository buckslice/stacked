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

public class Zone : MonoBehaviour {
    // WARNING : THIS CLASS USES CUSTOM EDITOR SO NEW VARIABLES WONT APPEAR WITHOUT ADDING TO THAT CLASS
    public ZoneShape shape;
    public ZoneType type;
    public float healthChange = 1.0f;
    public float duration = 10.0f;  // changes meaning depending on zonetype

    public ParticleSystem ps;

    public bool isTelegraphed = false;
    public ParticleSystem tps;
    public float telegraphDuration = 3.0f;

    // add particle system for telegraphing
    // and particle system for once zone is active
    // bool hasTelegraph (opens up options)
    // add flexibility to work with gameobjects that have subparticle systems
    // add velocity to these! so you can have lines that you have to jump over

    float durTimer = 0.0f;

    // TODO : change to max player count, also pool these zones
    Collider[] buffer = new Collider[10];
    Collider col;

    bool dying = false;
    bool setup = false;

    float origPSVolume;

    // Use this for initialization
    void Awake() {
        Debug.Assert(ps, "Zone needs a particle system prefab");
        // reassign here so we dont edit the original prefab, but instead the copy of the prefab
        ps = (ParticleSystem)Instantiate(ps, transform, false);

        // save original particle system scale
        // make sure scales are at least 1 and then calc volume
        Vector3 psScale = ps.transform.localScale;
        psScale.x = Mathf.Max(1.0f, psScale.x);
        psScale.y = Mathf.Max(1.0f, psScale.y);
        psScale.z = Mathf.Max(1.0f, psScale.z);
        origPSVolume = psScale.x * psScale.y * psScale.z;

        ParticleSystem.ShapeModule pssm = ps.shape;
        if (shape == ZoneShape.SQUARE) {
            BoxCollider bc = gameObject.AddComponent<BoxCollider>();
            Vector3 halfUp = Vector3.up * 0.5f;
            bc.center = halfUp;
            col = bc;

            ps.transform.localPosition = halfUp;
            ps.transform.localRotation = Quaternion.identity;
            ps.transform.localScale = Vector3.one;
            pssm.shapeType = ParticleSystemShapeType.Box;
        } else if (shape == ZoneShape.CIRCLE) { // circle uses sphere collider for now, could add in custom puck shaped collider
            col = gameObject.AddComponent<SphereCollider>();

            ps.transform.localPosition = Vector3.zero;
            ps.transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            ps.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
            pssm.shapeType = ParticleSystemShapeType.Hemisphere;
        }
    }

    // sets position and size of zone
    // position is specified by the bottom center
    // size is width height length
    public void Setup(Vector3 position, Vector3 scale) {
        if (shape == ZoneShape.CIRCLE) { // makes sure scale vector for circles is uniform
            float maxDim = Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z));
            scale = Vector3.one * maxDim;
        }

        // set new position and scale
        transform.position = position;
        transform.localScale = scale;

        Vector3 colSize = col.bounds.size;
        float ySize = shape == ZoneShape.CIRCLE ? 1.0f : colSize.y;
        float colVolume = colSize.x * ySize * colSize.z;

        // scales the particle system to the volume of the collider
        // but bases it off of old particle system volume and settings
        ParticleSystem.EmissionModule psem = ps.emission;
        float newRate = psem.rate.constant * colVolume / origPSVolume;
        psem.rate = newRate;
        ps.maxParticles = (int)(newRate * ps.startLifetime);

        if (!isTelegraphed) {
            ps.Play();  // play on awake is disabled since emission rate is being set after initialization
                        // this is so the prewarm option will work correctly (prewarm is still optional though)
        } else {
            //psem = tps.emission;
            //newRate = psem.rate.constant * colVolume / origTPSVolume;
            //psem.rate = newRate;
            //tps.maxParticles = (int)(newRate * tps.startLifetime);

            //tps.Play();
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

        durTimer += Time.deltaTime;
        if (durTimer >= duration) {
            if (type == ZoneType.EXPLODE_AFTER_TIMER) {
                DamageAllPlayersInContact();
            }
            DestroyZone();
        }
    }

    void DamageAllPlayersInContact() {
        int count = 0;

        if (shape == ZoneShape.SQUARE) {
            Vector3 half = transform.localScale / 2.0f;
            Vector3 worldCenter = transform.position + Vector3.up * half.y;
            count = Physics.OverlapBoxNonAlloc(worldCenter, half, buffer);
        } else {
            Vector3 scale = transform.localScale;
            float radius = Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z));
            count = Physics.OverlapSphereNonAlloc(transform.position, radius, buffer);
        }
        
        for (int i = 0; i < count; ++i) {
            if (buffer[i].CompareTag(Tags.Player)) {
                buffer[i].GetComponent<Damageable>().Damage(healthChange);
            }
        }
    }

    void OnTriggerEnter(Collider c) {
        if (type == ZoneType.EXPLODE_ON_CONTACT && c.CompareTag(Tags.Player)) {
            DamageAllPlayersInContact();
            DestroyZone();
        }
    }

    void OnTriggerStay(Collider c) {
        if ((type == ZoneType.DAMAGE_OVER_TIME || type == ZoneType.HEALING_OVER_TIME) 
            && c.CompareTag(Tags.Player)) {
            c.GetComponent<Damageable>().Damage(healthChange * Time.fixedDeltaTime);
        }
    }

    // destroys this object after the particle system runs its course
    void DestroyZone() {
        if (!dying) {
            dying = true;
            Destroy(col);
            ps.Stop();
            Destroy(gameObject, ps.startLifetime);
        }
    }
}
