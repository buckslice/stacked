using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script attached as a child gameobject to an object which is thrown.
/// </summary>
public class ThrownObjectAbility : MonoBehaviour {

    IAbilityActivation abilityActivation; //ability to do damage to things hit by the thrown object
    DamageAction damageAction;

    Vector3 startPosition;
    Vector3 destinationPosition;
    float startTime;
    float endTime;
    AbilityNetworking targetNetworking;
    IMovement targetMovement;
    Rigidbody targetRigid;
    Damageable[] targetDamageables;

    void Awake() {
        abilityActivation = GetComponent<IAbilityActivation>();
        damageAction = GetComponent<DamageAction>();
    }

    public void Initialize(Vector3 startPosition, Vector3 destinationPosition, float endTime, AbilityNetworking targetNetworking, IMovement targetMovement, Rigidbody targetRigid, IDamageHolder damageReference) {
        this.startPosition = startPosition;
        this.destinationPosition = destinationPosition;
        this.startTime = Time.time;
        this.endTime = endTime;
        this.targetNetworking = targetNetworking;
        this.targetMovement = targetMovement;
        this.targetRigid = targetRigid;
        this.targetDamageables = targetRigid.GetComponentsInChildren<Damageable>();
        if (damageAction != null) { damageAction.TrackerReference = damageReference; }

        targetMovement.haltMovement();
        targetNetworking.AddNetworkedAbility(abilityActivation);

        targetRigid.rotation = Quaternion.LookRotation(destinationPosition - startPosition, Vector3.up);
        targetRigid.position = startPosition;
        this.transform.position = targetNetworking.transform.position;
        this.transform.SetParent(targetNetworking.transform);
        this.transform.Reset();

        foreach (Damageable targetDamageable in targetDamageables) {
            targetDamageable.MagicalVulnerabilityMultiplier.AddModifier(0);
            targetDamageable.PhysicalVulnerabilityMultiplier.AddModifier(0);
        }
    }

    void Update() {
        if (Time.time > endTime) {
            Destroy();
            return;
        }

        float lerpProgress = Mathf.InverseLerp(startTime, endTime, Time.time);
        targetRigid.MovePosition(Vector3.Lerp(startPosition, destinationPosition, lerpProgress));
    }

    void Destroy() {
        targetMovement.ControlEnabled.RemoveModifier(false);
        targetMovement.setVelocity((destinationPosition - startPosition).normalized);
        targetNetworking.RemoveNetworkedAbility(abilityActivation);

        foreach (Damageable targetDamageable in targetDamageables) {
            targetDamageable.MagicalVulnerabilityMultiplier.RemoveModifier(0);
            targetDamageable.PhysicalVulnerabilityMultiplier.RemoveModifier(0);
        }

        this.transform.SetParent(null);

        targetNetworking = null;
        targetMovement = null;
        targetRigid = null;
        targetDamageables = null;
        SimplePool.Despawn(this.gameObject);
    }
}
