using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An ability that requires the player to not move while it is active.
/// </summary>
public class DisableRotationDuration : DurationAbilityAction {

    IRotation targetRotation;

    protected override void Start() {
        base.Start();
        targetRotation = GetComponentInParent<IRotation>();
    }

    protected override void OnDurationBegin() {
        targetRotation.RotationInputEnabled.AddModifier(false);
    }

    protected override void OnDurationEnd() {
        targetRotation.RotationInputEnabled.RemoveModifier(false);
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
