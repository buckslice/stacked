using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class TargetHolder : TargetedAbilityAction {

    public GameObject target {get; set;}

    public override bool Activate(GameObject context, PhotonStream stream) {
        target = context;
        return true;
    }
}
