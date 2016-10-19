﻿using UnityEngine;
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
    bool enabled = true;
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

        enabled = true;
        previousLerpValue = 0;
    }

    void Update() {
        if (Time.time > endTime) {
            Destroy();
            return;
        }

        if (enabled) {
            float lerpProgress = Mathf.InverseLerp(startTime, endTime, Time.time);
            Vector3 movementDiff = Vector3.Lerp(startPosition, destinationPosition, lerpProgress) - Vector3.Lerp(startPosition, destinationPosition, previousLerpValue);
            previousLerpValue = lerpProgress;
            targetRigid.MovePosition(movementDiff + targetRigid.position);
        }
    }

    public void Disable() {
        if (enabled) {
            targetMovement.MovementInputEnabled.RemoveModifier(false);
            enabled = false;
        }
    }

    void Destroy() {
        if (enabled) {
            targetMovement.MovementInputEnabled.RemoveModifier(false);
            targetMovement.setVelocity((destinationPosition - startPosition).normalized);
            enabled = false;
        }

        this.transform.SetParent(null);

        targetNetworking = null;
        targetMovement = null;
        targetRigid = null;
        SimplePool.Despawn(this.gameObject);
    }
}
