using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEveryFrame : MonoBehaviour, IUntargetedAbilityTrigger {

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    void Update() {
        abilityTriggerEvent();
    }
}
