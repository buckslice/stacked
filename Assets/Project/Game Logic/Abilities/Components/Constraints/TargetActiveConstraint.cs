using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class TargetActiveConstraint : UntargetedAbilityConstraint {

    [SerializeField]
    protected GameObject target;

    [SerializeField]
    protected bool requiredState;

    public override bool isAbilityActivatible() {
        return target.activeSelf == requiredState;
    }

    public override void Activate() {
    }
}
