using UnityEngine;
using System.Collections;

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

    private float durTimer = 0.0f;

    // TODO : change to max player count, also pool these zones
    private Collider[] buffer = new Collider[10];
    private BoxCollider col;
    private ParticleSystem ps;

    // Use this for initialization
    void Awake() {
        col = GetComponent<BoxCollider>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // sets position and size of zone
    // position is specified by the bottom center
    // size is width height length
    public void Setup(Vector3 position, Vector3 size) {
        transform.position = position;
        transform.localScale = size;
        CalculateParticleEmission();
        ps.Play();  // play on awake is disabled since emission rate is being set after initialization
        // this is so the prewarm option will work correctly
    }

    // set emission rate based on volume of collider
    void CalculateParticleEmission() {
        ParticleSystem.EmissionModule psem = ps.emission;
        Vector3 size = col.bounds.size;
        float volume = size.x * size.y * size.z;
        psem.rate = volume;
    }

    // Update is called once per frame
    void Update() {
        durTimer += Time.deltaTime;
        if (durTimer >= duration) {
            if (type == ZoneType.EXPLODE_AFTER_TIMER) {
                DamageAllPlayersInContact();
            }
            Destroy(gameObject);
        }
    }

    void DamageAllPlayersInContact() {
        // calculate worldspace box where collider is
        Vector3 half = transform.localScale / 2.0f;
        Vector3 worldCenter = transform.position + Vector3.up * half.y;

        int count = Physics.OverlapBoxNonAlloc(worldCenter, half, buffer);
        for (int i = 0; i < count; ++i) {
            if (buffer[i].CompareTag(Tags.Player)) {
                buffer[i].GetComponent<Damageable>().Damage(healthChange);
            }
        }
    }

    void OnCollisionEnter(Collision c) {
        if (type == ZoneType.EXPLODE_ON_CONTACT && c.collider.CompareTag(Tags.Player)) {
            DamageAllPlayersInContact();
            Destroy(gameObject);
        }
    }

    void OnCollisionStay(Collision c) {
        if ((type == ZoneType.DAMAGE_OVER_TIME || type == ZoneType.HEALING_OVER_TIME) 
            && c.collider.CompareTag(Tags.Player)) {
            c.collider.GetComponent<Damageable>().Damage(healthChange);
        }
    }


}
