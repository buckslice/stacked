﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Damage {
    public enum DamageType { PHYSICAL, MAGICAL };

    [SerializeField]
    protected float amount = 10;
    public float Amount { get { return amount; } }

    [SerializeField]
    protected DamageType type;
    public DamageType Type { get { return type; } }

    public Damage(float amount) {
        this.amount = amount;
    }

    public Damage(float amount, DamageType type)
        : this(amount) {
        this.amount = amount;
        this.type = type;
    }

    
    public static implicit operator Damage(float amount)
    {
        return new Damage(amount);
    }
    

    public static implicit operator float(Damage damage) {
        return damage.amount;
    }
}

/// <summary>
/// Represents a section of an object that can take damage.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Damageable : MonoBehaviour {
    [SerializeField]
    [Tooltip("Can be null. If null, grabs a Health script via GetComponentInParent.")]
    protected Health health;

    [SerializeField]
    protected MultiplierFloatStat physicalVulnerabilityMultiplier = new MultiplierFloatStat(1);
    public MultiplierFloatStat PhysicalVulnerabilityMultiplier { get { return physicalVulnerabilityMultiplier; } }

    [SerializeField]
    protected MultiplierFloatStat magicalVulnerabilityMultiplier = new MultiplierFloatStat(1);
    public MultiplierFloatStat MagicalVulnerabilityMultiplier { get { return magicalVulnerabilityMultiplier; } }

    IDamageTracker selfTracker;

    void Start()
    {
        if (health == null)
        {
            health = GetComponentInParent<Health>();
        }

        selfTracker = GetComponentInParent<DamageHolder>().GetRootDamageTracker();
    }

    float CalculateActualDamage(Damage incoming) {
        switch (incoming.Type) {
            case global::Damage.DamageType.MAGICAL:
                return incoming.Amount * magicalVulnerabilityMultiplier;
            case global::Damage.DamageType.PHYSICAL:
                return incoming.Amount * physicalVulnerabilityMultiplier;
            default:
                return incoming.Amount;
        }
    }

    public float Damage(Damage incoming, bool trueDamage = false)
    {
        float actualDamageAmount = trueDamage ? incoming.Amount : CalculateActualDamage(incoming);
        return health.Damage(actualDamageAmount);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="incoming"></param>
    /// <param name="holderReference"></param>
    /// <param name="trueDamage">TrueDamage ignores damage reduction</param>
    /// <returns></returns>
    public float Damage(Damage incoming, IDamageHolder holderReference, bool trueDamage = false) {
        IDamageTracker rootDamageReference = holderReference.GetRootDamageTracker();
        float dealtDamage = Damage(incoming, rootDamageReference, trueDamage);
        //EventLog.Log(this, "{0} did {1} {2} damage to {3} using {4}", rootDamageReference, dealtDamage, incoming.Type, selfTracker, holderReference);
        return dealtDamage;
    }

    public float Damage(Damage incoming, IDamageTracker trackerReference, bool trueDamage = false) {
        float actualDamageAmount = trueDamage ? incoming.Amount : CalculateActualDamage(incoming);
        float result = health.Damage(actualDamageAmount, trackerReference);
        trackerReference.AddDamageDealt(result);
        return result;
    }

    public float Damage(Damage incoming, Player playerReference, bool trueDamage = false) {
        float actualDamageAmount = trueDamage ? incoming.Amount : CalculateActualDamage(incoming);
        float result = health.Damage(actualDamageAmount, playerReference);
        playerReference.AddDamageDealt(result);
        return result;
    }

    public float Heal(float incoming, IDamageHolder holderReference) {
        IDamageTracker rootDamageReference = holderReference.GetRootDamageTracker();
        float healingDone = Heal(incoming, rootDamageReference);
        //EventLog.Log(this, "{0} healed {1} damage on {2} using {3}", rootDamageReference, healingDone, selfTracker, holderReference);
        return healingDone;
    }

    public float Heal(float incoming, Player playerReference) {
        float actualHealingAmount = health.Heal(incoming, playerReference);
        playerReference.AddHealingDone(actualHealingAmount);
        return actualHealingAmount;
    }

    public float Heal(float incoming, IDamageTracker trackerReference) {
        float actualHealingAmount = health.Heal(incoming, trackerReference);
        trackerReference.AddHealingDone(actualHealingAmount);
        return actualHealingAmount;
    }
}
