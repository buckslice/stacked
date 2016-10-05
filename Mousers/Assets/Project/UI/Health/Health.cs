using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// add this script to anything that should have health and be damageable
public class Health : MonoBehaviour {

    [SerializeField]
    private float health = 100f;     // current health
    private float maxHealth;

    public HealthBarType barType = HealthBarType.FLOATING;

    private HealthBar bar;

    void Awake() {
        maxHealth = health;
    }

    void Start() {
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        CanvasHelper ch = canvasRoot.GetComponent<CanvasHelper>();

        GameObject healthBar;
        if (barType == HealthBarType.PLAYER) {
            healthBar = (GameObject)Instantiate(ch.playerHealthBarPrefab, ch.playerHealthBarGroup);
            healthBar.transform.localScale = Vector3.one;
            bar = healthBar.GetComponent<HealthBar>();
        } else {   // need to implement boss bars still so this is temp
            healthBar = (GameObject)Instantiate(ch.floatingHealthBarPrefab, ch.floatingHealthBarGroup);
            healthBar.transform.localScale = Vector3.one;
            bar = healthBar.GetComponent<HealthBar>();
            bar.followTransform = gameObject.transform;
            Debug.Assert(ch.scaler, "Need canvas scaler on canvas!");
            bar.scaler = ch.scaler;

            // try to find bounds for object to use as floating health bar offset
            Bounds bounds = new Bounds();
            Collider col = GetComponent<Collider>();
            if (col) {
                bounds = col.bounds;
            } else {
                Renderer rend = GetComponent<Renderer>();
                if (rend) {
                    bounds = rend.bounds;
                }
            }

            bar.followOffset = new Vector3(0.0f, bounds.size.y * 1.5f, 0.0f);
        }

        if (bar) {
            bar.type = barType;
            bar.SetText(gameObject.name);
        }

    }

    void OnDestroy() {
        if (bar) {
            Destroy(bar.gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        if (health <= 0.0f) {    // temp, might not want to always destroy when zero health
            Destroy(gameObject);
        }
    }

    public void Damage(float amount) {
        health -= amount;
        if (bar) {
            bar.SetPercent(health / maxHealth);
        }
    }

    public void Heal(float amount) {
        Damage(-amount);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(health);
        } else {
            health = (float)stream.ReceiveNext();
            if (bar) {
                bar.SetPercent(health / maxHealth);
            }
        }
    }
}
