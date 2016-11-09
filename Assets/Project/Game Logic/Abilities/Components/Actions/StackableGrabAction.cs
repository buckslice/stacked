using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class StackableGrabAction : TypedTargetedAbilityAction {

    Stackable stackable;

    protected override void Start() {
        base.Start();
        stackable = GetComponentInParent<Stackable>();
        Assert.IsNotNull(stackable);
    }

    public override bool isAbilityActivatible(GameObject target) {
        Stackable targetStackable = target.GetComponentInParent<Stackable>();
        if (targetStackable == null) {
            return false;
        }

        //other constraints

        return true;
    }

    public override bool Activate(GameObject context, PhotonStream stream) {
        Stackable targetStackable = context.GetComponentInParent<Stackable>();
        stackable.Grab(targetStackable.bottommost);
        return true;
    }
}
