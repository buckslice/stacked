using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class SilenceTarget : TypedTargetedAbilityAction {

    [SerializeField]
    protected bool modifier = false; //if false, silences an enemy. If true, unsilences them.

    public override bool isAbilityActivatible(GameObject target) {
        GameObject targetRoot = target.transform.root.gameObject;
        if (targetRoot.GetComponentInChildren<IAbilities>() == null) {
            return false;
        }
        return true;
    }
    public override bool Activate(GameObject context, PhotonStream stream) {
        GameObject targetRoot = context.transform.root.gameObject;
        IAbilities targetAbilities = targetRoot.GetComponentInChildren<IAbilities>();
        targetAbilities.ActivationEnabled.AddModifier(modifier);
        return true;
    }
}
