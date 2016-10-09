using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

/// <summary>
/// An ability that requires the player to not move while it is active.
/// </summary>
public class HaltAbility : DurationAbilityAction {

    PlayerMovement movement;

    Coroutine activeRoutine;

    protected override void Start() {
        base.Start();
        movement = GetComponentInParent<PlayerMovement>();
    }

    public override bool Activate(PhotonStream stream) {
        if (activeRoutine != null) {
            StopCoroutine(activeRoutine);
        }
        activeRoutine = StartCoroutine(DurationRoutine());
        return true;
    }
    protected override void OnDurationBegin() {
        movement.ControlEnabled.AddModifier(false);
        movement.haltMovement();
    }

    protected override void OnDurationEnd() {
        movement.ControlEnabled.RemoveModifier(false);
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
