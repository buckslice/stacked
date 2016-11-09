using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RequireStackableBelow : UntargetedAbilityConstraint {

    [SerializeField]
    protected bool requiredBelowState; //false for "must not have someone below us", true for "require someone below us"

    Stackable stackable;

    protected override void Start() {
        base.Start();
        stackable = GetComponentInParent<Stackable>();
        Assert.IsNotNull(stackable);
    }

    public override bool isAbilityActivatible() {
        Debug.Log((stackable.Below != null) == requiredBelowState, this);
        return (stackable.Below != null) == requiredBelowState;
    }

    public override void Activate() { }
}
