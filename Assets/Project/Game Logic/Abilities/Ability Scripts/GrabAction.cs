using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class GrabAction : TypedTargetedAbilityAction {
    IDamageTracker trackerReference;

    [SerializeField]
    protected Rigidbody targetHolder;

    [SerializeField]
    FixedJoint targetConnectingJoint;

    [SerializeField]
    FixedJoint holderConnectingJoint;

    Vector3 connectedOffset;

    protected override void Start() {
        base.Start();
        trackerReference = GetComponentInParent<IDamageHolder>().GetRootDamageTracker();
        holderConnectingJoint.connectedBody = GetComponentInParent<Rigidbody>();
        targetConnectingJoint.autoConfigureConnectedAnchor = false;
        connectedOffset = transform.localPosition;
    }

    public override bool isAbilityActivatible(GameObject target) {
        GameObject targetRoot = target.transform.root.gameObject;
        if (targetRoot.GetComponentInChildren<Rigidbody>() == null) {
            return false;
        }

        if (targetRoot.GetComponentInChildren<IMovement>() == null) {
            return false;
        }

        if (targetRoot.GetComponentInChildren<AbilityNetworking>() == null) {
            return false;
        }

        IDamageTracker targetReference = targetRoot.GetComponentInChildren<IDamageHolder>().GetRootDamageTracker();
        return targetReference != trackerReference;
    }

    public override bool Activate(GameObject context, PhotonStream stream) {
        targetHolder.gameObject.SetActive(true);
        Rigidbody targetRigid = context.transform.root.GetComponentInChildren<Rigidbody>();

        IMovement targetMovement = context.transform.root.GetComponentInChildren<IMovement>();
        targetMovement.ControlEnabled.AddModifier(false);
        targetMovement.haltMovement();

        targetRigid.transform.position = targetHolder.position;
        targetRigid.transform.rotation = targetRigid.transform.rotation = targetHolder.rotation;
        targetConnectingJoint.connectedBody = targetRigid;

        holderConnectingJoint.connectedAnchor = connectedOffset; //prevent drift from physics shenannigans
        return true;
    }
}
