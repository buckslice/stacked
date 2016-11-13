using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class TriggerWithTarget : AbstractAbilityAction, ITargetedAbilityTrigger {

    [SerializeField]
    protected TargetHolder holder;

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };
    public event TargetedTrigger targetedAbilityTriggerEvent = delegate{ };

    public override bool Activate(PhotonStream stream) {
        targetedAbilityTriggerEvent(holder.target);
        return false; //child activations handle the networking
    }
}
