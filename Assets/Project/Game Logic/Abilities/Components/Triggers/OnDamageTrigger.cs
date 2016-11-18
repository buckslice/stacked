using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class OnDamageTrigger : MonoBehaviour, IUntargetedAbilityTrigger {

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    Health targetHealth;

    void Start() {
        targetHealth = GetComponentInParent<Health>();
        targetHealth.onDamage += TargetHealth_onDamage;
    }

    private void TargetHealth_onDamage(float amount, int playerID) {
        abilityTriggerEvent();
    }
}
