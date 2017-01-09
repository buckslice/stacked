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
        return AbstractStatus.refreshDuplicates<MoveSpeedStatus>(target);
    }

    protected override void OnProjectileCreated() { }

    protected override void OnProjectileAttached() {
        base.OnProjectileAttached();

        affectedTarget = GetComponentInParent<IMovementSpeed>();
        if (affectedTarget == null) {
            DeactivateProjectile();
            return;
        }
        affectedTarget.Speed.AddModifier(moveSpeedMultiplier);
    }

    public override void OnProjectileDeactivated() {
        base.OnProjectileDeactivated();
        affectedTarget.Speed.RemoveModifier(moveSpeedMultiplier);
        affectedTarget = null;
    }
}
