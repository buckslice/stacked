using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script attached as a child gameobject to an object which is thrown.
/// </summary>
[RequireComponent(typeof(ProjectileDestruction))]
public class DashingObjectAbility : MonoBehaviour, IMovementOverride {

    ProjectileDestruction destruction;

    Vector3 startPosition;
    Vector3 destinationPosition;
    float startTime;
    float endTime;
    protected AbilityNetworking targetNetworking;
    IMovement targetMovement;
    Rigidbody targetRigid;
    bool active = true;
    float previousLerpValue = 0;

    void Awake() {
        destruction = GetComponent<ProjectileDestruction>();
    }

    public void Initialize(Vector3 startPosition, Vector3 destinationPosition, float startTime, float endTime, AbilityNetworking targetNetworking, IMovement targetMovement, Rigidbody targetRigid) {
        this.startPosition = startPosition;
        this.destinationPosition = destinationPosition;
        this.startTime = startTime;
        this.endTime = endTime;
        this.targetNetworking = targetNetworking;
        this.targetMovement = targetMovement;
        this.targetRigid = targetRigid;

        Assert.AreApproximatelyEqual((destinationPosition - startPosition).y, 0);

        targetMovement.HaltMovement();
        targetMovement.MovementInputEnabled.AddModifier(false);
        targetMovement.SetCurrentMovementOverride(this);

        targetRigid.rotation = Quaternion.LookRotation(destinationPosition - startPosition, Vector3.up);
        targetRigid.position = startPosition;
        targetMovement.MovePosition(startPosition);
        this.transform.position = targetNetworking.transform.position;
        this.transform.SetParent(targetNetworking.transform);
        this.transform.Reset();

        active = true;
        previousLerpValue = 0;

        StartCoroutine(UpdateRoutine());
    }

    IEnumerator UpdateRoutine() {
        while (Time.time <= endTime) {
            if (active) {
                float lerpProgress = Mathf.InverseLerp(startTime, endTime, Time.time);
                if (Time.time > endTime) {
                    lerpProgress = 1;
                }

                Vector3 movementDiff = Vector3.Lerp(startPosition, destinationPosition, lerpProgress) - Vector3.Lerp(startPosition, destinationPosition, previousLerpValue);
                previousLerpValue = lerpProgress;
                targetMovement.MovePosition(movementDiff + targetRigid.position);
            }
            yield return null;
        }

        Destroy();
    }

    public bool Disable() {
        if (active) {
            targetMovement.MovementInputEnabled.RemoveModifier(false);
            active = false;
            return true;
        }
        return false;
    }

    void Destroy() {
        if (active) {
            targetMovement.MovementInputEnabled.RemoveModifier(false);
            targetMovement.SetVelocity((destinationPosition - startPosition).normalized);
            active = false;
        }

        this.transform.SetParent(null);

        targetNetworking = null;
        targetMovement = null;
        targetRigid = null;

        destruction.StartDestroySequence();
    }
}
