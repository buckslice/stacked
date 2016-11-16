using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class TriggerStay : TargetedAbilityTrigger {

    void OnTriggerStay(Collider col) {
        if (enabled) {
            FireTrigger(col.gameObject);
        }
    }
}
