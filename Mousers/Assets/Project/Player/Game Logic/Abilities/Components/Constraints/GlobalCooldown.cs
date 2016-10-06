using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Tracking script for global ability cooldowns. Should exist in the root of a player. Created via AddComponent in GlobalCooldownConstraint
/// </summary>
public class GlobalCooldown : MonoBehaviour {

    protected float lastActivationTime = -9999;
    public virtual float LastActivationTime { get { return lastActivationTime; } }

    public void Activate() {
        lastActivationTime = Time.time;
    }
}

