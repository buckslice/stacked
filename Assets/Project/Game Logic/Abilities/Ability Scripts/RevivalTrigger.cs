using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RevivalTrigger : MonoBehaviour, IUntargetedAbilityTrigger {

    Health health;
    Player player;

    void Start() {
        health = GetComponentInParent<Health>();
        health.onHeal += health_onHeal;
        player = GetComponentInParent<IDamageHolder>().GetRootDamageTracker() as Player;
    }

    void health_onHeal(float amount, int playerID) {
        if (enabled && player.dead && health.healthPercent == 1) {
            abilityTriggerEvent();
        }
    }

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate{ };
}
