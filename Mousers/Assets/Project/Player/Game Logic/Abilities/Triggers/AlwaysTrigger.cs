using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An ability that is always triggering. Useful for passive or always-on abilities
/// </summary>
public class AlwaysTrigger : MonoBehaviour, IAbilityTrigger {

    public event AbilityTrigger abilityTriggerEvent = delegate { };

    void Update()
    {
        abilityTriggerEvent();
    }
}
