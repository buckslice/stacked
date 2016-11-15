using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(OneHotAbilityConstraint))]
public class OneHotAbilityAction : DurationAbilityAction {

    OneHotAbilityConstraint constraint;

    protected override void Start() {
        base.Start();
        constraint = GetComponent<OneHotAbilityConstraint>();
    }

    protected override void OnDurationBegin() {
        constraint.OneHotReference.activationAvailable.AddModifier(false);
    }

    protected override void OnDurationEnd() {
        constraint.OneHotReference.activationAvailable.RemoveModifier(false);
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
