using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class TriggerWithSelfDamageable : AbstractAbilityAction, ITargetedAbilityTrigger {

    GameObject damageableSelf;

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };
    public event TargetedTrigger targetedAbilityTriggerEvent = delegate { };

    private void Start() {
        damageableSelf = transform.root.GetComponentInChildren<Damageable>().gameObject;
    }

    public override bool Activate(PhotonStream stream) {
        targetedAbilityTriggerEvent(damageableSelf);
        return false; //child activations handle the networking
    }
}
