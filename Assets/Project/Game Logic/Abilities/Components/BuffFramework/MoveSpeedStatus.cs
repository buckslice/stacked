using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

public class MoveSpeedStatus : AbstractStatus {

    [SerializeField]
    protected float moveSpeedMultiplier = 1f;

    IMovementSpeed affectedTarget = null;

    protected override bool handleDuplicates(Transform target) {
        return AbstractStatus.refreshDuplicates<MoveSpeedStatus>(this, target);
    }

    protected override void OnProjectileCreated() { }

    protected override void OnProjectileAttached() {
        base.OnProjectileAttached();

        affectedTarget = GetComponentInParent<IMovementSpeed>();
        if (affectedTarget == null) {
            DeactivateProjectile();
            return;
        }
        Debug.Log(this.gameObject, this.gameObject);
        affectedTarget.Speed.AddModifier(moveSpeedMultiplier);
    }

    public override void OnProjectileDeactivated() {
        base.OnProjectileDeactivated();
        if (affectedTarget != null) {
            affectedTarget.Speed.RemoveModifier(moveSpeedMultiplier);
            affectedTarget = null;
        }
    }
}
