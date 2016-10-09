using UnityEngine;
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

    void Start()
    {
        if (health == null)
        {
            health = GetComponentInParent<Health>();
        }
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

    public float Damage(Damage incoming)
    {
        float actualDamageAmount = CalculateActualDamage(incoming);
        health.Damage(actualDamageAmount);
        return actualDamageAmount;
    }

    public float Damage(Damage incoming, IDamageTracker trackerReference) {
        float actualDamageAmount = CalculateActualDamage(incoming);
        health.Damage(actualDamageAmount, trackerReference);
        trackerReference.AddDamageDealt(actualDamageAmount);
        return actualDamageAmount;
    }

    public float Damage(Damage incoming, Player playerReference) {
        float actualDamageAmount = CalculateActualDamage(incoming);
        health.Damage(actualDamageAmount, playerReference);
        playerReference.AddDamageDealt(actualDamageAmount);
        return actualDamageAmount;
    }
}
