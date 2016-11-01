using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Requires that the target be a locally owned gameObject.
/// </summary>
public class RequireLocal : TargetedAbilityConstraint {

    public override bool isAbilityActivatible(GameObject target) {
        PhotonView view = target.GetComponentInParent<PhotonView>();
        return view != null && view.isMine;
    }

    public override void Activate(GameObject context) { }
}
