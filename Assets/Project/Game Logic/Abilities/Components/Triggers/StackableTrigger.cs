using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Triggers when the stackable's stack changes.
/// </summary>
public class StackableTrigger : MonoBehaviour, IUntargetedAbilityTrigger {

    Stackable stackable;

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    void Start() {
        stackable = GetComponentInParent<Stackable>();
        Assert.IsNotNull(stackable);
        stackable.changeEvent += stackable_changeEvent;
    }

    void stackable_changeEvent() {
        if (!enabled) { return; }
        abilityTriggerEvent();
    }
}
