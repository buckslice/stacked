using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

/// <summary>
/// An ability that requires the player to not move while it is active.
/// </summary>
public class HaltAbility : DurationAbilityAction {

    IMovement movement;
    IAbilities targetAbilities;
    Coroutine activeRoutine;

    protected override void Start() {
        Debug.LogWarning("I (Derek) am depreciating this in favor of combinations of disable-duration scripts");
        base.Start();
        movement = GetComponentInParent<IMovement>();
        targetAbilities = GetComponentInParent<IAbilities>();
    }

    protected override void OnDurationBegin() {
        movement.ControlEnabled.AddModifier(false);
        targetAbilities.ActivationEnabled.AddModifier(false);
        movement.HaltMovement();
    }

    protected override void OnDurationEnd() {
        movement.ControlEnabled.RemoveModifier(false);
        targetAbilities.ActivationEnabled.RemoveModifier(false);
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
