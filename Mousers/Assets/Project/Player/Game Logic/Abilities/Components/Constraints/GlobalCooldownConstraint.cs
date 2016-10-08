using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class GlobalCooldownConstraint : UntargetedAbilityConstraint {

    [SerializeField]
    protected MultiplierFloatStat cooldownSecs = new MultiplierFloatStat(1);

    GlobalCooldown referenceCooldown;

    protected override void Start() {
        base.Start();
        referenceCooldown = GetComponentInParent<GlobalCooldown>();

        if (referenceCooldown == null) {
            referenceCooldown = transform.root.AddComponent<GlobalCooldown>();
        }
    }

    public override bool isAbilityActivatible() {
        return Time.time >= referenceCooldown.LastActivationTime + cooldownSecs;
    }

    public override void Activate() {
        referenceCooldown.Activate();
    }
}
