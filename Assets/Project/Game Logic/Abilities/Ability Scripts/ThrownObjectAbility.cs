using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface for abilities on projectiles which must be initialized
/// </summary>
public interface ProjectileProxy {
    void Initialize(AbilityNetworking targetNetworking, IDamageHolder damageReference);
}

/// <summary>
/// Script attached as a child gameobject to an object which is thrown.
/// </summary>
public class ThrownObjectAbility : MonoBehaviour, IDespawnable, ProjectileProxy {

    IAbilityActivation abilityActivation; //ability to do damage to things hit by the thrown object
    DamageAction damageAction;

    AbilityNetworking targetNetworking;
    Damageable[] targetDamageables;

    void Awake() {
        abilityActivation = GetComponent<IAbilityActivation>();
        damageAction = GetComponent<DamageAction>();
    }

    public void Initialize(AbilityNetworking targetNetworking, IDamageHolder damageReference) {
        this.targetNetworking = targetNetworking;
        this.targetDamageables = targetNetworking.transform.root.GetComponentsInChildren<Damageable>();
        if (damageAction != null) { damageAction.TrackerReference = damageReference; }

        targetNetworking.AddNetworkedAbility(abilityActivation);

        foreach (Damageable targetDamageable in targetDamageables) {
            targetDamageable.MagicalVulnerabilityMultiplier.AddModifier(0);
            targetDamageable.PhysicalVulnerabilityMultiplier.AddModifier(0);
        }
    }

    public void Despawn() {
        targetNetworking.RemoveNetworkedAbility(abilityActivation);

        foreach (Damageable targetDamageable in targetDamageables) {
            targetDamageable.MagicalVulnerabilityMultiplier.RemoveModifier(0);
            targetDamageable.PhysicalVulnerabilityMultiplier.RemoveModifier(0);
        }

        targetNetworking = null;
        targetDamageables = null;
    }
}
