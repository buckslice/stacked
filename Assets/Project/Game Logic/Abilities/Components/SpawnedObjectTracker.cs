using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Component added at runtime by SpawnAbility to keep track of spawned objects
/// </summary>

[RequireComponent(typeof(IActivationNetworking))]
public class SpawnedObjectTracker : ProjectileLifetimeAction, IDamageHolder {

    public delegate void ProjectileDestroyed(SpawnedObjectTracker self);
    public event ProjectileDestroyed onProjectileDestroyed = (self) => { };

    IDamageHolder trackerReference;
    public IDamageTracker DamageTracker { get { return trackerReference.DamageTracker; } }

    IActivationNetworking activationNetworking;
    public IActivationNetworking ActivationNetworking { get { return activationNetworking; } }

    protected override void Awake() {
        base.Awake();
        activationNetworking = GetComponent<IActivationNetworking>();
    }

    public void Initialize(IDamageHolder tracker) {
        this.trackerReference = tracker;
    }

    void Start() {
        Assert.IsNotNull(trackerReference);
    }

    protected override void OnProjectileCreated() {
    }

    protected override void OnProjectileDestroyed() {
        base.OnProjectileDestroyed();
        onProjectileDestroyed(this);
    }

    protected override void OnProjectileDeactivated() {
        base.OnProjectileDeactivated();
    }
}
