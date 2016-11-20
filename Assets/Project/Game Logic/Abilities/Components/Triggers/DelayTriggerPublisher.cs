using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DelayTriggerPublisher : MonoBehaviour, IUntargetedAbilityTrigger, IRemoteTrigger {

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    public void Trigger() {
        if (isActiveAndEnabled) {
            abilityTriggerEvent();
        }
    }
}
