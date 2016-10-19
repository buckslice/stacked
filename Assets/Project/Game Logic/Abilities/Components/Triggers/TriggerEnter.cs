using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class TriggerEnter : TargetedAbilityTrigger {

    void OnTriggerEnter(Collider col) {
        if (enabled) {
            FireTrigger(col.gameObject);
        }
    }
}
