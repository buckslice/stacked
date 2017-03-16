using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class TriggerWithSelf : AbstractAbilityAction, ITargetedAbilityTrigger {

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };
    public event TargetedTrigger targetedAbilityTriggerEvent = delegate { };

    public override bool Activate(PhotonStream stream) {
        targetedAbilityTriggerEvent(this.transform.root.gameObject);
        return false; //child activations handle the networking
    }
}
