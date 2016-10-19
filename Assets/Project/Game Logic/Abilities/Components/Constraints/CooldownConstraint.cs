using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface to communicate time remaining on cooldowns
/// </summary>
public interface ICooldownConstraint {
    /// <summary>
    /// Time until the ability is ready, normalized to the range [0, 1].
    /// </summary>
    /// <returns></returns>
    float cooldownProgress();
}

/// <summary>
/// Requires a minimum amount of time between activations.
/// </summary>
public class CooldownConstraint : UntargetedAbilityConstraint, ICooldownConstraint {

    [SerializeField]
    protected MultiplierFloatStat cooldownSecs = new MultiplierFloatStat(1);

    protected float lastActivationTime = -9999;
    public virtual float LastActivationTime { get { return lastActivationTime; } }

    public float cooldownProgress() {
        float timeRemaining = lastActivationTime + cooldownSecs - Time.time;
        if(timeRemaining < 0)
        {
            timeRemaining = 0;
        }
        return timeRemaining / cooldownSecs;
    }

    public override bool isAbilityActivatible() {
        return Time.time >= lastActivationTime + cooldownSecs;
    }

    public override void Activate() {
        lastActivationTime = Time.time;
    }
}