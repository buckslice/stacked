using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using UnityEngine.UI;

// add this script to anything that should have health and be damageable
[RequireComponent(typeof(PhotonView))]
public class Health : MonoBehaviour {

    public delegate void OnDamage(float amount, int playerID);
    public delegate void OnHeal(float amount, int playerID);

    public delegate void HealthChanged();

    public delegate void OnDeath();

    public event OnDamage onDamage = delegate { };
    public event OnDamage onHeal = delegate { };
    public event HealthChanged onHealthChanged = delegate { };
    public event OnDeath onDeath = delegate { };

    [SerializeField]
    protected float _health = 100f;     // current health
    public float health { get { return _health; } }
    /// <summary>
    /// Returns the amount by which the health was actually changed.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private float SetHealth(float value) {
        float healthBefore = _health;
        _health = value;
        if (_health > maxHealth) {
            _health = maxHealth;
        }

        onHealthChanged();

        if (_health <= 0) {
            //Don't use PhotonNetwork.Destroy, since we didn't use PhotonNetwork.Instantiate()
            onDeath();
        }

        return healthBefore - _health;
    }

    // explicitely set health and max health
    public void SetHealth(float health, float max) {
        _health = health;
        maxHealth = max;
        if(_health > maxHealth) {
            health = maxHealth;
        }
        onHealthChanged();
        if (_health <= 0) {
            onDeath();
        }
    }

    protected float maxHealth;
    public float healthPercent { get { return health / maxHealth; } }

    //private HealthBar bar;

    void Awake() {
        maxHealth = health;
    }

    public virtual float Damage(float amount) {
        Assert.IsTrue(amount >= 0);
        return SetHealth(_health - amount);
    }

    public float Damage(float amount, int playerID)
    {
        Assert.IsTrue(amount >= 0);
        float result = Damage(amount);
        onDamage(amount, playerID);
        return result;
    }

    public float Damage(float amount, Player playerReference) {
        Assert.IsTrue(amount >= 0);
        return Damage(amount, playerReference.PlayerID);
    }

    public float Damage(float amount, IDamageTracker trackerReference) {
        Assert.IsTrue(amount >= 0);
        if (trackerReference is Player) {
            return Damage(amount, (Player)trackerReference);
        } else {
            return Damage(amount);
        }
    }

    public virtual float Heal(float amount) {
        Assert.IsTrue(amount >= 0);
        return setHealth(_health + amount);
    }

    public float Heal(float amount, int playerID) {
        Assert.IsTrue(amount >= 0);
        float result = Heal(amount);
        onHeal(amount, playerID);
        return result;
    }

    public float Heal(float amount, Player playerReference) {
        Assert.IsTrue(amount >= 0);
        return Heal(amount, playerReference.PlayerID);
    }

    public float Heal(float amount, IDamageTracker trackerReference) {
        Assert.IsTrue(amount >= 0);
        if (trackerReference is Player) {
            return Heal(amount, (Player)trackerReference);
        } else {
            return Heal(amount);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(health);
        } else {
            SetHealth((float)stream.ReceiveNext());
        }
    }

    public void Kill() {
        SetHealth(0);
    }
}
