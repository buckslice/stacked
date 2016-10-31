using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A particle system will live for its natual lifetime instead of immediately vanishing when deactivated.
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class GracefulParticleDeactivation : ProjectileLifetimeAction, IProjectileDeactivation {

    ParticleSystem particles;

    protected override void Awake() {
        base.Awake();
        particles = GetComponent<ParticleSystem>();
    }

    protected override void OnProjectileCreated() { }

    public override void OnProjectileDeactivated() {
        base.OnProjectileDeactivated();
        particles.Stop();
    }

    float IProjectileDeactivation.getDeactivationTime() {
        return particles.startLifetime;
    }
}
