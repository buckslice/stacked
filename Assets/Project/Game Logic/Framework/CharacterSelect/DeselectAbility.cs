using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DeselectAbility : UntargetedAbilityConstraint {

    ISelection selection;

    protected override void Start() {
        base.Start();
        selection = GetComponentInParent<ISelection>();
    }

    public override void Activate() {
        throw new System.NotImplementedException();
    }

    public override bool isAbilityActivatible() {
        return selection.CanDeselect();
    }

    public override bool Activate(PhotonStream stream) {
        return selection.Deselect();
    }
}
