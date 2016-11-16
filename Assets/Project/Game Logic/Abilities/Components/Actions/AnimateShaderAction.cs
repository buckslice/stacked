using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnimateShader))]
public class AnimateShaderAction : AbstractAbilityAction {

    AnimateShader target;

    protected override void Awake() {
        base.Awake();
        target = GetComponent<AnimateShader>();
    }

    public override bool Activate(PhotonStream stream) {
        target.Play();
        return true;
    }
}
