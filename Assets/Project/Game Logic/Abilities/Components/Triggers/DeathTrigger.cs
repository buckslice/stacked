using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DeathTrigger : MonoBehaviour, IUntargetedAbilityTrigger {

    Health health;

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    void Start() {
        health = GetComponentInParent<Health>();
        Assert.IsNotNull(health);
        health.onDeath += health_onDeath;
    }

    void health_onDeath() {
        if (!enabled) { return; }
        abilityTriggerEvent();
    }
}
