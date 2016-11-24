using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

public class RestartAbilityScripting : AbstractAbilityAction {

    [SerializeField]
    AbilityScripting target;

    public override bool Activate(PhotonStream stream) {
        target.ResetCycle();
        return true;
    }
}
