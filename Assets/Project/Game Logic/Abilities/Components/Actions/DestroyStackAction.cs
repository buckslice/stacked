using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DestroyStackAction : TypedTargetedAbilityAction {

    public override bool isAbilityActivatible(GameObject target) {
        return target.GetComponentInParent<Stackable>() != null;
    }

    public override bool Activate(GameObject target, PhotonStream stream) {
        Debug.Log(target);
        Stackable stackable = target.GetComponentInParent<Stackable>();
        if (stackable == null) { return false; }

        stackable.DestackAll();
        return true;
    }
}

