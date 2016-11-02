using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class UntargetedTriggerEnter : MonoBehaviour, IUntargetedAbilityTrigger {

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    void OnTriggerEnter(Collider col) {
        if (enabled) {
            abilityTriggerEvent();
        }
    }
}
