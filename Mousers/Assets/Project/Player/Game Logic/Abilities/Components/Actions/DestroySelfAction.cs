using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DestroySelfAction : UntargetedAbilityConstraint, ISpawnable {

    bool activated = true;

    public override bool isAbilityActivatible() {
        return activated;
    }

    public void Spawn() {
        activated = true;
    }

    public override void Activate() {
        activated = false;
        ProjectileLifetimeAction.DestroyProjectile(transform.root);
    }
}
