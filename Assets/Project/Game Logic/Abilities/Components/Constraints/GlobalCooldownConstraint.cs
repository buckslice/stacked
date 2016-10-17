using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class GlobalCooldownConstraint : UntargetedAbilityConstraint, ICooldownConstraint {

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

    public float cooldownProgress() {
        float timeRemaining = referenceCooldown.LastActivationTime + cooldownSecs - Time.time;
        if (timeRemaining < 0) {
            timeRemaining = 0;
        }
        return timeRemaining / cooldownSecs;
    }

    public override bool isAbilityActivatible() {
        return Time.time >= referenceCooldown.LastActivationTime + cooldownSecs;
    }

    public override void Activate() {
        referenceCooldown.Activate();
    }
}
