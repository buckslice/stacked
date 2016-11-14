using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An ability that requires the player to not move while it is active.
/// </summary>
public class DisableMovementDuration : DurationAbilityAction {

    IMovement targetMovement;

    protected override void Start() {
        base.Start();
        targetMovement = GetComponentInParent<IMovement>();
    }

    protected override void OnDurationBegin() {
        targetMovement.MovementInputEnabled.AddModifier(false);
    }

    protected override void OnDurationEnd() {
        targetMovement.MovementInputEnabled.RemoveModifier(false);
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
