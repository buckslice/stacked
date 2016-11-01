using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundOnActive : ProjectileLifetimeAction {

    AudioSource target;

    protected override void Awake() {
        target = GetComponent<AudioSource>();
        base.Awake();
    }

    protected override void OnProjectileCreated() {
        target.Play();
    }
}
