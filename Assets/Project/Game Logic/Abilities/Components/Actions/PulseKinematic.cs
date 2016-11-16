using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class PulseKinematic : AbstractAbilityAction {

    Rigidbody rigid;

    protected override void Start() {
        base.Start();
        rigid = GetComponentInParent<Rigidbody>();
    }

    public override bool Activate(PhotonStream stream) {
        rigid.isKinematic = true;

        Callback.FireForUpdate(() => rigid.isKinematic = false, this);
        return true;
    }
}
