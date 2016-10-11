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
    public ZoneType type;
    public float healthChange = 1.0f;
    public float duration = 3.0f;

    float durTimer = 0.0f;
    ZoneShape shape;

    // TODO : change to max player count, also pool these zones
    Collider[] buffer = new Collider[10];
    Collider col;
    ParticleSystem ps;

    bool dying = false;

    // Use this for initialization
    void Awake() {
        col = GetComponent<Collider>();
        if(col.GetType() == typeof(BoxCollider)) {  // square uses short box collider 
            shape = ZoneShape.SQUARE;
        } else {    // circle uses sphere collider for now, could add in custom puck shaped collider
            shape = ZoneShape.CIRCLE;
        }
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // sets position and size of zone
    // position is specified by the bottom center
    // size is width height length
    public void Setup(Vector3 position, Vector3 scale) {
        if(shape == ZoneShape.CIRCLE) { // makes sure scale vector for circles is uniform
            float maxDim = Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z));
            scale = Vector3.one * maxDim;
        }

        // calculate original bounding volume of zone
        Vector3 origScale = transform.localScale;
        Vector3 origSize = col.bounds.size;
        float origVolume = origSize.x * 1.0f * origSize.z; // basically just an area for now

        // set new position and scale
        transform.position = position;
        transform.localScale = scale;

        Vector3 newSize = col.bounds.size;
        float newVolume = newSize.x * 1.0f * newSize.z;

        // scales the particle system to the volume of the collider
        // but bases it off of old volume and settings
        ParticleSystem.EmissionModule psem = ps.emission;
        float newRate = psem.rate.constant * newVolume / origVolume;
        psem.rate = newRate;
        ps.maxParticles = (int)(newRate * ps.startLifetime);

        ps.Play();  // play on awake is disabled since emission rate is being set after initialization
        // this is so the prewarm option will work correctly
    }

    // Update is called once per frame
    void Update() {
        if (dying) {
            return;
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
