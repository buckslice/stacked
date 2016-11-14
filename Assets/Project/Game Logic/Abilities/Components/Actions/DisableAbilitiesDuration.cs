using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An ability that requires the player to not move while it is active.
/// </summary>
public class DisableAbilitiesDuration : DurationAbilityAction {
    
    IAbilities targetAbilities;

    protected override void Start() {
        base.Start();
        targetAbilities = GetComponentInParent<IAbilities>();
    }

    protected override void OnDurationBegin() {
        targetAbilities.ActivationEnabled.AddModifier(false);
    }

    protected override void OnDurationEnd() {
        targetAbilities.ActivationEnabled.RemoveModifier(false);
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
