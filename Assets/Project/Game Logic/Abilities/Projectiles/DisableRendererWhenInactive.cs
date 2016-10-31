using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class DisableRendererWhenInactive : ProjectileLifetimeAction {

    Renderer rend;

    protected override void Awake() {
        rend = GetComponent<Renderer>();
    }

    protected override void OnProjectileCreated() { }

    public override void OnProjectileDeactivated() {
        rend.enabled = false;
        base.OnProjectileDeactivated();
    }

    protected override void OnProjectileDestroyed() {
        rend.enabled = true;
        base.OnProjectileDestroyed();
    }
}
