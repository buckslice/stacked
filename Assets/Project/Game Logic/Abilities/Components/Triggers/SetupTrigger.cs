using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Unique type so getComponentInChildren will only get these particular scripts.
/// </summary>
public class SetupTrigger : TargetedAbilityTrigger {
    public void Trigger(GameObject target) {
        FireTrigger(target);
    }
}
