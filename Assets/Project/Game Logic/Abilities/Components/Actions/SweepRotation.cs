using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

public class SweepRotation : DurationAbilityAction {

    [SerializeField]
    protected float angleToTraverse = 180f; //should be negative for counterclockwise rotation

    [SerializeField]
    protected Vector3 rotationUp = Vector3.up; //axis for the rotation, following right-hand rule up conventions.

    Rigidbody rigid;

    bool active;
    Quaternion startRotation;

    protected override void Start() {
        base.Start();
        rigid = GetComponentInParent<Rigidbody>();
    }

    protected override void OnDurationBegin() {
        startRotation = rigid.rotation;
        active = true;
    }

    protected override void OnDurationTick(float lerpProgress) {
        base.OnDurationTick(lerpProgress);
        Quaternion currentRotation = startRotation * Quaternion.AngleAxis(lerpProgress * angleToTraverse, rotationUp);
        rigid.MoveRotation(currentRotation);
    }

    protected override void OnDurationEnd() {
        Disable();
    }

    protected override void OnDurationInterrupted() {
        OnDurationEnd();
        base.OnDurationInterrupted();
    }

    public override void setValue(float value, BalanceStat.StatType type) {
        switch(type) {
            case BalanceStat.StatType.ANGLE:
                angleToTraverse = value;
                break;
            default:
                base.setValue(value, type);
                break;
        }
    }

    public bool Disable() {
        if (active) {
            rigid.MoveRotation(startRotation);
            active = false;
            return true;
        }
        return false;
    }
}
