using UnityEngine;
using System.Collections;

public class SetEnabledMovement : AbstractAbilityAction {
    IMovement targetMovement;

    [SerializeField]
    protected bool outcomeState;

    protected override void Start() {
        base.Start();
        targetMovement = GetComponentInParent<IMovement>();
    }

    public override bool Activate(PhotonStream stream) {
        if (!outcomeState) {
            targetMovement.MovementInputEnabled.AddModifier(false);
            return true;
        }
        else if (outcomeState) {
            targetMovement.MovementInputEnabled.RemoveModifier(false);
            return true;
        }
        return false;
    }
}
