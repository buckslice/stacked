using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class PressurePlateTrigger : MonoBehaviour, IUntargetedAbilityTrigger {

    [SerializeField]
    protected int requiredHeight = 4;

    void OnTriggerStay(Collider col) {
        Stackable stackable = col.GetComponentInParent<Stackable>();
        if (stackable == null) { return; }

        if(stackable.topmost.elevationInStack() >= requiredHeight - 1) {
            abilityTriggerEvent();
        }
    }

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

}
