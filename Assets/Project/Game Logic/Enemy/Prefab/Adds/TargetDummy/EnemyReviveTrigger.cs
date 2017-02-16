using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReviveTrigger : MonoBehaviour, IUntargetedAbilityTrigger {

    Health health;
    Player player;

    void Start() {
        health = GetComponentInParent<Health>();
        health.onHeal += health_onHeal;
        player = GetComponentInParent<IDamageHolder>().GetRootDamageTracker() as Player;
    }

    void health_onHeal(float amount, int playerID) {
        if (enabled && health.healthPercent == 1) {
            abilityTriggerEvent();
        }
    }

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };
}
