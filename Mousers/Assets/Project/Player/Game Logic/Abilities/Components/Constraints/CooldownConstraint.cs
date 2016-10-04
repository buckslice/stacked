using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Requires a minimum amount of time between activations.
/// </summary>
public class CooldownConstraint : AbstractAbilityConstraint {

    [SerializeField]
    protected MultiplierFloatStat cooldownSecs = new MultiplierFloatStat(1);

    float lastActivationTime = -9999;

    public override bool isAbilityActivatible()
    {
        return Time.time >= lastActivationTime + cooldownSecs;
    }

    public override void Activate()
    {
        lastActivationTime = Time.time;
    }
}
