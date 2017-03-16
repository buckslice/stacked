using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RequireStacked : UntargetedAbilityConstraint {

    Stackable stackable;

    [SerializeField]
    protected bool requiredStackedState = true;

    protected override void Start() {
        base.Start();
        stackable = GetComponentInParent<Stackable>();
        Assert.IsNotNull(stackable);
    }

    public override bool isAbilityActivatible() {
        return requiredStackedState == (stackable.Below != null || stackable.Above != null);
    }

    public override void Activate() { }
}
