using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleOnActive : ProjectileLifetimeAction {

    ParticleSystem target;

    protected override void Awake() {
        target = GetComponent<ParticleSystem>();
        base.Awake();
    }

    protected override void OnProjectileCreated() {
        target.Play();
    }
}
