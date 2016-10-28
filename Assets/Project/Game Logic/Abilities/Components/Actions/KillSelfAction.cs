using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class KillSelfAction : AbstractAbilityAction {

    Health health;

    protected override void Start() {
        base.Start();
        health = GetComponentInParent<Health>();
    }

    public override bool Activate(PhotonStream stream) {
        health.Kill();
        return true;
    }
}
