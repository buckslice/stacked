using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DelayTriggerPublisher : MonoBehaviour, IUntargetedAbilityTrigger {

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    public void Trigger() {
        abilityTriggerEvent();
    }

}
