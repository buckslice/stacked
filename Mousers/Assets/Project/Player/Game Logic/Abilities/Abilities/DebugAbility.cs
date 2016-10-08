using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DebugAbility : TargetedAbilityAction {

    public override bool Activate(GameObject context, PhotonStream stream) {
        Debug.LogFormat(this, "Activation with target {0}", context);
        return false;
    }
}
