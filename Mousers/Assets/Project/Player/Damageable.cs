using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents a section of an object that can take damage
/// </summary>
[RequireComponent(typeof(Collider))]
public class Damageable : MonoBehaviour {
    [SerializeField]
    Health health;

    [SerializeField]
    protected MultiplierFloatStat vulnerabilityMultiplier = new MultiplierFloatStat(1);

    void Start()
    {
        if (health == null)
        {
            health = GetComponentInParent<Health>();
        }
    }

    public float Damage(float incomingAmount)
    {
        float actualDamageAmount = incomingAmount * vulnerabilityMultiplier;
        health.Damage(actualDamageAmount);
        return actualDamageAmount;
    }

    public float Damage(float incomingAmount, Player playerReference) {
        float actualDamageAmount = incomingAmount * vulnerabilityMultiplier;
        health.Damage(actualDamageAmount, playerReference);
        playerReference.AddDamageDealt(actualDamageAmount);
        return actualDamageAmount;
    }

    public MultiplierFloatStat getVulnerabilityMultiplier() {
        return vulnerabilityMultiplier;
    }
}
