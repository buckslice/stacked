using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IShape))]
public class OptionalNearestTargetCast : NearestTargetCast {

    public override bool isAbilityActivatible() {
        return true;
    }
}
