using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DestroySelfAction : UntargetedAbilityConstraint, ISpawnable {

    ProjectileDestruction projectileRoot;
    bool activated = true;

    protected override void Start() {
        base.Start();
        projectileRoot = GetComponentInParent<ProjectileDestruction>();
    }

    public override bool isAbilityActivatible() {
        return activated;
    }

    public void Spawn() {
        activated = true;
    }

    public override void Activate() {
        activated = false;
        projectileRoot.StartDestroySequence();
    }
}
