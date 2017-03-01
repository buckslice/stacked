using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class PressurePlateTrigger : MonoBehaviour, IUntargetedAbilityTrigger {

    public bool requiresAllPlayers = true; // by default require every player to stack, else just one

    void OnTriggerStay(Collider col) {
        Stackable stackable = col.GetComponentInParent<Stackable>();
        if (stackable == null) { return; }

        if(!requiresAllPlayers || stackable.topmost.elevationInStack() >= Player.Players.Count - 1) {
            abilityTriggerEvent();
        }
    }

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

}
