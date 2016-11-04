using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Requires that the target be a locally owned gameObject.
/// </summary>
public class RequireLocal : TargetedAbilityConstraint {

    protected override void Start() {
        base.Start();
        Callback.FireForUpdate(() => {
            //enable all activations, after the abilitynetwork disables them, since we actually need them.
            //the owner of the activation is the target, not the owner of this object
            foreach (IUntargetedAbilityTrigger inputActivation in GetComponentsInParent<IUntargetedAbilityTrigger>()) {
                ((MonoBehaviour)inputActivation).enabled = true;
            }
        }, this);
    }

    public override bool isAbilityActivatible(GameObject target) {
        PhotonView view = target.GetComponentInParent<PhotonView>();
        return view != null && view.isMine;
    }

    public override void Activate(GameObject context) { }
}
