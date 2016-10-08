using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface for AbilityActivation and TargetedAbilityActivation, so that untargeted constraints can add themselves to both
/// </summary>
public interface IAbilityConstrained {
    void AddConstraint(UntargetedAbilityConstraint toAdd);
    bool RemoveConstraint(UntargetedAbilityConstraint toRemove);
}

/// <summary>
/// Abstract class to contain functionality that almost all ability constraints will use
/// </summary>
public abstract class TargetedAbilityConstraint : AbstractAbilityAction {
    protected IAbilityConstrained activation = null;
    protected override void Start() {
        base.Start();
        linkActivation();
    }

    protected virtual void OnDestroy() {
        removeActivation();
    }

    protected virtual void linkActivation() {
        activation = GetComponent<TargetedAbilityActivation>();

        ((TargetedAbilityActivation)activation).AddConstraint(this);
    }

    protected virtual void removeActivation() {
        if (activation != null) {
            ((TargetedAbilityActivation)activation).RemoveConstraint(this);
        }
    }

    public abstract bool isAbilityActivatible(GameObject target);

    public override bool Activate(PhotonStream stream) {
        Activate();
        return false;
    }

    public abstract void Activate();
}

/// <summary>
/// Ability constraint which does not need a reference target.
/// </summary>
public abstract class UntargetedAbilityConstraint : TargetedAbilityConstraint {

    protected sealed override void linkActivation() {
        activation = GetComponent<TargetedAbilityActivation>();

        if (activation == null) {
            activation = GetComponent<AbilityActivation>();
        } else {
            Assert.IsNull(GetComponent<AbilityActivation>());
        }

        Assert.IsNotNull(activation);

        activation.AddConstraint(this);
    }

    protected sealed override void removeActivation() {
        if (activation != null) {
            activation.RemoveConstraint(this);
        }
    }

    public sealed override bool isAbilityActivatible(GameObject target) {
        return isAbilityActivatible();
    }

    public abstract bool isAbilityActivatible();
}