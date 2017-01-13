using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class StackableDisconnectBelowAction : AbstractAbilityAction {

    Stackable stackable;

    protected override void Start() {
        base.Start();
        stackable = GetComponentInParent<Stackable>();
        Assert.IsNotNull(stackable);
    }

    public override bool Activate(PhotonStream stream) {
        return stackable.DisconnectBelow();
    }
}