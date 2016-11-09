using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RequireStackableAbove : UntargetedAbilityConstraint {

    [SerializeField]
    protected bool requiredAboveState; //false for "must not have someone above us", true for "require someone above us"

    Stackable stackable;

    protected override void Start() {
        base.Start();
        stackable = GetComponentInParent<Stackable>();
        Assert.IsNotNull(stackable);
    }

    public override bool isAbilityActivatible() {
        return (stackable.Above != null) == requiredAboveState;
    }

    public override void Activate() { }
}
