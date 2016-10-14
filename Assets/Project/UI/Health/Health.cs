using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// add this script to anything that should have health and be damageable
public class Health : MonoBehaviour {

    public delegate void OnDamage(float amount, int playerID);

    public delegate void HealthChanged();

    public event OnDamage onDamage = delegate { };
    public event HealthChanged onHealthChanged = delegate { };

    [SerializeField]
    protected float _health = 100f;     // current health
    public float health { get { return _health; } }
    /// <summary>
    /// Returns the amount by which the health was actually changed.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private float setHealth(float value) {
        float healthBefore = _health;
        _health = value;
        if (_health > maxHealth) {
            _health = maxHealth;
        }

        onHealthChanged();

        if (_health <= 0) {
            Destroy(gameObject);
        }

        return _health - healthBefore;
    }
    protected float maxHealth;
    public float healthPercent { get { return health / maxHealth; } }

    //private HealthBar bar;

    void Awake() {
        maxHealth = health;
    }

    public virtual float Damage(float amount) {
        return setHealth(_health - amount);
    }

    public float Damage(float amount, int playerID)
    {
        float result = Damage(amount);
        onDamage(amount, playerID);
        return result;
    }

    public float Damage(float amount, Player playerReference)
    {
        return Damage(amount, playerReference.PlayerID);
    }

    public float Damage(float amount, IDamageTracker trackerReference) {
        if (trackerReference is Player) {
            return Damage(amount, (Player)trackerReference);
        } else {
            return Damage(amount);
        }
    }

    public float Heal(float amount) {
        return -Damage(-amount);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(health);
        } else {
            setHealth((float)stream.ReceiveNext());
        }
    }
}
