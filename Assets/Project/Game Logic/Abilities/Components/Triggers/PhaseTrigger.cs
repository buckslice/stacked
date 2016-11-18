using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Trigger script specificly for phase transitions. Used by PhaseInvulnerability (and will be used by any other phase transitioning mechanics)
/// </summary>
public class PhaseTrigger : MonoBehaviour, IUntargetedAbilityTrigger {

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    public int currentPhase { get; private set; }

    public void Trigger(int currentPhase) {
        this.currentPhase = currentPhase;
        if (isActiveAndEnabled) {
            abilityTriggerEvent();
        }
    }
}
