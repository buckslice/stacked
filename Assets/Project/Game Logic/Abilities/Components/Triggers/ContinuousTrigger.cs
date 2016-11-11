using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class ContinuousTrigger : MonoBehaviour, IUntargetedAbilityTrigger {

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    [SerializeField]
    float timeBetweenActivations = 1;

    float triggerTime = -1;

    void OnEnable() {
        triggerTime = Mathf.Max(Time.time, triggerTime);
    }

    void Update() {
        while(Time.time > triggerTime) {
            abilityTriggerEvent();
            triggerTime += timeBetweenActivations;
        }
    }

}
