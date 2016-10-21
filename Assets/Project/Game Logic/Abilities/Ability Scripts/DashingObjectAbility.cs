using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script attached as a child gameobject to an object which is thrown.
/// </summary>
public class DashingObjectAbility : MonoBehaviour, IMovementOverride {

    Vector3 startPosition;
    Vector3 destinationPosition;
    float startTime;
    float endTime;
    AbilityNetworking targetNetworking;
    IMovement targetMovement;
    Rigidbody targetRigid;
    bool active = true;
    float previousLerpValue = 0;

    public void Initialize(Vector3 startPosition, Vector3 destinationPosition, float startTime, float endTime, AbilityNetworking targetNetworking, IMovement targetMovement, Rigidbody targetRigid) {
        this.startPosition = startPosition;
        this.destinationPosition = destinationPosition;
        this.startTime = startTime;
        this.endTime = endTime;
        this.targetNetworking = targetNetworking;
        this.targetMovement = targetMovement;
        this.targetRigid = targetRigid;

        targetMovement.haltMovement();
        targetMovement.MovementInputEnabled.AddModifier(false);

        foreach (IMovementOverride movementOverride in targetNetworking.transform.GetComponentsInChildren<IMovementOverride>()) {
            movementOverride.Disable();
        }

        targetRigid.rotation = Quaternion.LookRotation(destinationPosition - startPosition, Vector3.up);
        targetRigid.position = startPosition;
        this.transform.position = targetNetworking.transform.position;
        this.transform.SetParent(targetNetworking.transform);
        this.transform.Reset();

        active = true;
        previousLerpValue = 0;
    }

    void Update() {

        if (active) {
            float lerpProgress = Mathf.InverseLerp(startTime, endTime, Time.time);
            if (Time.time > endTime)
            { 
                lerpProgress = 1;
            }

            Vector3 movementDiff = Vector3.Lerp(startPosition, destinationPosition, lerpProgress) - Vector3.Lerp(startPosition, destinationPosition, previousLerpValue);
            previousLerpValue = lerpProgress;
            targetRigid.MovePosition(movementDiff + targetRigid.position);
        }

        if (Time.time > endTime) {
            Destroy();
            return;
        }
    }

    public void Disable() {
        if (active) {
            targetMovement.MovementInputEnabled.RemoveModifier(false);
            active = false;
        }
    }

    void Destroy() {
        if (active) {
            targetMovement.MovementInputEnabled.RemoveModifier(false);
            targetMovement.setVelocity((destinationPosition - startPosition).normalized);
            active = false;
        }

        this.transform.SetParent(null);

        targetNetworking = null;
        targetMovement = null;
        targetRigid = null;
        SimplePool.Despawn(this.gameObject);
    }
}
