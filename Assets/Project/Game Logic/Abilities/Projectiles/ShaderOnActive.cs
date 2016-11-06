using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnimateShader))]
public class ShaderOnActive : ProjectileLifetimeAction {

    AnimateShader target;

	protected override void Awake () {
        base.Awake();
        target = GetComponent<AnimateShader>();
	}

    protected override void OnProjectileCreated() {
        target.Play();
    }
}
