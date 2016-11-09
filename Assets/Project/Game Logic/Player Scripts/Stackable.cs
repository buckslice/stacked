﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IMovement))] //not strictly required, but exists in all use cases I can think of
[RequireComponent(typeof(Rigidbody))]
public class Stackable : MonoBehaviour {

    [SerializeField]
    protected Rigidbody targetHolder;

    [SerializeField]
    FixedJoint targetConnectingJoint;

    [SerializeField]
    FixedJoint holderConnectingJoint;

    Vector3 connectedOffset;
    Rigidbody selfRigidbody;
    IMovement selfMovement;

    Stackable above = null;
    public Stackable Above { get { return above; } }

    Stackable below = null;
    public Stackable Below { get { return below; } }

    protected void Start() {
        holderConnectingJoint.connectedBody = selfRigidbody = GetComponent<Rigidbody>();
        selfMovement = GetComponent<IMovement>();
        Assert.IsTrue(holderConnectingJoint.connectedBody != targetHolder);
        targetConnectingJoint.autoConfigureConnectedAnchor = false;
        connectedOffset = transform.localPosition;
    }

    public void Grab(Stackable target) {
        Assert.IsTrue(target.below == null);
        
        if(above != null) {
            Stackable detached = above;
            DisconnectGrabbed();
            detached.PlaceAtop(target.topmost);
        }

        targetHolder.gameObject.SetActive(true);
        Rigidbody targetRigid = target.selfRigidbody;

        IMovement targetMovement = target.selfMovement;
        targetMovement.MovementInputEnabled.AddModifier(false);
        targetMovement.HaltMovement();

        targetRigid.transform.position = targetHolder.position;
        targetRigid.transform.rotation = targetRigid.transform.rotation = targetHolder.rotation;
        targetConnectingJoint.connectedBody = targetRigid;

        holderConnectingJoint.connectedAnchor = connectedOffset; //prevent drift from physics shenannigans

        target.below = this;
        above = target;
    }

    void DisconnectGrabbed() {
        Assert.IsTrue(above != null);

        IMovement targetMovement = above.selfMovement;

        targetConnectingJoint.connectedBody = null;

        targetMovement.MovementInputEnabled.RemoveModifier(false);

        targetHolder.gameObject.SetActive(false);
        above.below = null;
        above = null;
    }

    public void PlaceAtop(Stackable target) {
        Assert.IsTrue(below == null);
        target = target.topmost;
        target.Grab(this);
    }

    public bool RemoveSelf() {

        if (below == null && above == null) { return false; }

        Stackable targetBelow = below;
        Stackable targetAbove = above;

        if (targetBelow != null) {
            targetBelow.DisconnectGrabbed();
        }

        if (targetAbove != null) {
            DisconnectGrabbed();
        }

        if (targetAbove != null && targetBelow != null) {
            targetBelow.Grab(targetAbove);
        }

        return true;
    }

    public Stackable topmost {
        get {
            Stackable result = this;
            while (result.above != null) {
                result = result.above;
            }
            return result;
        }
    }

    public Stackable bottommost {
        get {
            Stackable result = this;
            while (result.below != null) {
                result = result.below;
            }
            return result;
        }
    }
}
