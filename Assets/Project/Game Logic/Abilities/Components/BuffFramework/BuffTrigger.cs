using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// When activated, triggers a targeted activation for every buffed object.
/// </summary>
[RequireComponent(typeof(BuffTracker))]
public class BuffTrigger : AbstractAbilityAction, ITargetedAbilityTrigger {

    BuffTracker tracker;

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };
    private void FireUntargetedEvent(GameObject target) { abilityTriggerEvent(); }

    public event TargetedTrigger targetedAbilityTriggerEvent = (target) => { };

    protected override void Awake() {
        base.Awake();
        tracker = GetComponent<BuffTracker>();
        targetedAbilityTriggerEvent += FireUntargetedEvent;
    }

    public override bool Activate(PhotonStream stream) {
        foreach (Collider target in tracker) {
            targetedAbilityTriggerEvent(target.gameObject);
        }
        return false; //let child ability activation handle it
    }
}
