using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
public class ShockwaveSetup : ProjectileLifetimeAction {

    [SerializeField]
    protected float duration;

    Material shockwaveDistortionMat;
    // Use this for initialization
    protected override void Awake() {
        base.Awake();
        MeshRenderer rend = GetComponent<MeshRenderer>();
        shockwaveDistortionMat = rend.material = rend.material; //craete a duplicate, since the setter does an implicit instantiate() call
    }

    protected override void OnProjectileCreated() {
        Callback.DoLerp((float l) => {
            shockwaveDistortionMat.SetFloat("_MaxRange", l / 2);
            shockwaveDistortionMat.SetFloat("_Annulus", 1 - (l * l));
        }, duration, this).FollowedBy(() => {
            shockwaveDistortionMat.SetFloat("_MaxRange", 0);
        }, this);
    }
}
