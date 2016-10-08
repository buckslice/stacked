using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Requires a minimum amount of time between activations.
/// </summary>
public class CooldownConstraint : UntargetedAbilityConstraint {

    [SerializeField]
    protected MultiplierFloatStat cooldownSecs = new MultiplierFloatStat(1);

    protected float lastActivationTime = -9999;
    public virtual float LastActivationTime { get { return lastActivationTime; } }

    public override bool isAbilityActivatible() {
        return Time.time >= lastActivationTime + cooldownSecs;
    }

    public override void Activate() {
        lastActivationTime = Time.time;
    }
}