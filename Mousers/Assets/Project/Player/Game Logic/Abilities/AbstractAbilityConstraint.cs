﻿using UnityEngine;
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

public interface ITargetedAbilityConstraint {
    bool isAbilityActivatible(GameObject target);
}

/// <summary>
/// A targeted ability action that imposes constraints on what it can be activated with.
/// </summary>
public abstract class TypedTargetedAbilityAction : TargetedAbilityAction, ITargetedAbilityConstraint {
    protected IAbilityConstrained activation = null;
    protected override void Start() {
        base.Start();
        linkActivation();
    }

    protected virtual void OnDestroy() {
        removeActivation();
    }

    protected void linkActivation() {
        activation = GetComponent<TargetedAbilityActivation>();

        ((TargetedAbilityActivation)activation).AddConstraint(this);
    }

    protected void removeActivation() {
        if (activation != null) {
            ((TargetedAbilityActivation)activation).RemoveConstraint(this);
        }
    }

    public abstract bool isAbilityActivatible(GameObject target);
}

/// <summary>
/// A targeted constraint which has no meaningful action.
/// </summary>
public abstract class TargetedAbilityConstraint : TypedTargetedAbilityAction {

    public sealed override bool Activate(GameObject context, PhotonStream stream) {
        Activate(context);
        return false;
    }

    public abstract void Activate(GameObject context);
}

/// <summary>
/// Ability constraint which does not need a reference target.
/// </summary>
public abstract class UntargetedAbilityConstraint : AbstractAbilityAction, ITargetedAbilityConstraint {

    protected IAbilityConstrained activation = null;
    protected override void Start() {
        base.Start();
        linkActivation();
    }

    protected virtual void OnDestroy() {
        removeActivation();
    }

    protected void linkActivation() {
        activation = GetComponent<TargetedAbilityActivation>();

        if (activation == null) {
            activation = GetComponent<AbilityActivation>();
        } else {
            Assert.IsNull(GetComponent<AbilityActivation>());
        }

        Assert.IsNotNull(activation);

        activation.AddConstraint(this);
    }

    protected void removeActivation() {
        if (activation != null) {
            activation.RemoveConstraint(this);
        }
    }

    public bool isAbilityActivatible(GameObject context) {
        return isAbilityActivatible();
    }

    public abstract bool isAbilityActivatible();

    public void Activate(GameObject context) {
        Activate();
    }

    public sealed override bool Activate(PhotonStream stream) {
        Activate();
        return false;
    }

    public abstract void Activate();
}