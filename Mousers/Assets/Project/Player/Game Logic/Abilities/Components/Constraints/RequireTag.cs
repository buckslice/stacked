using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RequireTag : TargetedAbilityConstraint {

    [SerializeField]
    protected Tags.TagsMask tagsMask;

    public override bool isAbilityActivatible(GameObject target) {
        return target.CompareTag(tagsMask);
    }

    public override void Activate(GameObject context) { }
}
