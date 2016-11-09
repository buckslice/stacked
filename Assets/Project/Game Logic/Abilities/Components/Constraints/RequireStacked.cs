using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RequireStacked : UntargetedAbilityConstraint {

    Stackable stackable;

    protected override void Start() {
        base.Start();
        stackable = GetComponentInParent<Stackable>();
        Assert.IsNotNull(stackable);
    }

    public override bool isAbilityActivatible() {
        return stackable.Below != null || stackable.Above != null;
    }

    public override void Activate() { }
}
