using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Node of a union-find data structure, with no compression. The chain of parents to get to the head is used to specify exactly what did damage.
/// </summary>
public interface IDamageHolder {
    IDamageTracker DamageTracker { get; }
}

public static class IDamageHolderExtension {
    public static IDamageTracker GetRootDamageTracker(this IDamageHolder self) {
        IDamageTracker result = self.DamageTracker;

        while (result != self) {
            self = result;
            result = result.DamageTracker;
        }
        return result;
    }
}

public interface IDamageTracker : IDamageHolder {
    float DamageDealt { get; }
    void AddDamageDealt(float damage);

    float HealingDone { get; }
    void AddHealingDone(float healing);
}

[System.Serializable]
public abstract class AbstractDamageTracker : IDamageTracker {
    [ReadOnly]
    [SerializeField]
    protected float damageDealt = 0;
    public float DamageDealt { get { return damageDealt; } }
    public virtual void AddDamageDealt(float damage) { damageDealt += damage; }

    [ReadOnly]
    [SerializeField]
    protected float healingDone = 0;
    public float HealingDone { get { return healingDone; } }
    public virtual void AddHealingDone(float healing) { healingDone += healing; }

    public IDamageTracker DamageTracker { get { return this; } }

    [SerializeField]
    IDamageHolder holder;
    public MonoBehaviour Holder { get { return (MonoBehaviour)holder; }}

    public AbstractDamageTracker(IDamageHolder holder) {
        this.holder = holder;
    }

    public abstract void Destroy();
}

public class DamageHolder : MonoBehaviour, IDamageHolder, IPlayerID {

    IDamageHolder trackerReference = null;

    public IDamageTracker DamageTracker { get { return trackerReference.DamageTracker; } }

    int IPlayerID.PlayerID {
        get {
            Player player = this.GetRootDamageTracker() as Player;
            if (player != null) {
                return player.PlayerID;
            } else {
                Debug.LogWarning("Holder is not a player; returning playerID -1", this);
                return -1;
            }
        }
    }

    public void Initialize(IDamageHolder trackerReference) {
        this.trackerReference = trackerReference;
    }

    void OnDestroy() {
        if (trackerReference is AbstractDamageTracker) {
            ((AbstractDamageTracker)trackerReference).Destroy();
        }
    }
}