﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

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
}

[System.Serializable]
public abstract class AbstractDamageTracker : IDamageTracker {
    [ReadOnly]
    [SerializeField]
    protected float damageDealt = 0;
    public float DamageDealt { get { return damageDealt; } }
    public virtual void AddDamageDealt(float damage) { damageDealt += damage; }

    public IDamageTracker DamageTracker { get { return this; } }

    [SerializeField]
    IDamageHolder holder;
    public MonoBehaviour Holder { get { return (MonoBehaviour)holder; }}

    public AbstractDamageTracker(IDamageHolder holder) {
        this.holder = holder;
    }
}

public class DamageHolder : MonoBehaviour, IDamageHolder {

    IDamageHolder trackerReference = null;

    public IDamageTracker DamageTracker { get { return trackerReference.DamageTracker; } }

    public void Initialize(IDamageHolder trackerReference) {
        this.trackerReference = trackerReference;
    }
}