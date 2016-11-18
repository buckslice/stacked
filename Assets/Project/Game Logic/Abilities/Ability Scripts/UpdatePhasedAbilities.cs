using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PhaseTrigger))]
public class UpdatePhasedAbilities : AbstractAbilityAction {

    PhaseTrigger phaseTrigger;

    PhasedAbilityConstraint[] phasedAbilities;

    protected override void Start() {
        base.Start();
        phaseTrigger = GetComponent<PhaseTrigger>();

        phasedAbilities = transform.root.GetComponentsInChildren<PhasedAbilityConstraint>();
    }

    public override bool Activate(PhotonStream stream) {
        foreach (PhasedAbilityConstraint phasedAbility in phasedAbilities) {
            phasedAbility.currentPhase = phaseTrigger.currentPhase;
        }
        return phasedAbilities.Length > 0;
    }
}
