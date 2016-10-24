using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleOnActive : ProjectileLifetimeAction {

    ParticleSystem target;

    void Awake() {
        target = GetComponent<ParticleSystem>();
    }

    protected override void OnProjectileCreated() {
        target.Play();
    }
}
