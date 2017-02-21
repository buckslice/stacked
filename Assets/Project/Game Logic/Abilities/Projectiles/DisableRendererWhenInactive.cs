using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class DisableRendererWhenInactive : ProjectileLifetimeAction {

    Renderer rend;
    Light myLight;

    protected override void Awake() {
        rend = GetComponent<Renderer>();
        myLight = GetComponent<Light>();
    }

    protected override void OnProjectileCreated() { }

    public override void OnProjectileDeactivated() {
        if (rend) { rend.enabled = false; }
        if (myLight) { myLight.enabled = false; }
        
        base.OnProjectileDeactivated();
    }

    protected override void OnProjectileDestroyed() {
        if (rend) { rend.enabled = true; }
        if (myLight) { myLight.enabled = true; }

        base.OnProjectileDestroyed();
    }
}
