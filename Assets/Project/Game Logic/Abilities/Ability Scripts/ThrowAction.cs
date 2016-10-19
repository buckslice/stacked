using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class ThrowAction : AbstractAbilityAction {

    [SerializeField]
    protected Rigidbody targetHolder;

    [SerializeField]
    FixedJoint targetConnectingJoint;

    [SerializeField]
    protected float throwDistance = 10;

    [SerializeField]
    protected float throwDuration = 1;

    [SerializeField]
    protected GameObject thrownObjectAbilityPrefab;

    IDamageTracker trackerReference;

    protected override void Start() {
        base.Start();
        trackerReference = GetComponentInParent<IDamageHolder>().GetRootDamageTracker();
    }

    public override bool Activate(PhotonStream stream) {
        Vector3 destinationPosition = transform.position + throwDistance * Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        Rigidbody targetRigid = targetConnectingJoint.connectedBody;
        AbilityNetworking targetNetworking = targetRigid.transform.root.GetComponentInParent<AbilityNetworking>();
        IAbilities targetAbilities = targetRigid.transform.root.GetComponentInParent<IAbilities>();
        IMovement targetMovement = targetRigid.transform.root.GetComponentInParent<IMovement>();

        targetConnectingJoint.connectedBody = null;

        targetAbilities.ActivationEnabled.RemoveModifier(false);

        GameObject instantiatedThrownObjectAbility = SimplePool.Spawn(thrownObjectAbilityPrefab);
        DashingObjectAbility dashingObjectAbility = instantiatedThrownObjectAbility.GetComponent<DashingObjectAbility>();
        dashingObjectAbility.Initialize(transform.position, destinationPosition, Time.time, Time.time + throwDuration, targetNetworking, targetMovement, targetRigid);
        ThrownObjectAbility thrownObjectAbility = instantiatedThrownObjectAbility.GetComponent<ThrownObjectAbility>();
        thrownObjectAbility.Initialize(targetNetworking, trackerReference);

        targetHolder.gameObject.SetActive(false);
        return true;
    }

    void OnDestroy() {
        Rigidbody targetRigid = targetConnectingJoint.connectedBody;
        if (targetRigid == null) { return; }
        IAbilities targetAbilities = targetRigid.transform.root.GetComponentInParent<IAbilities>();
        IMovement targetMovement = targetRigid.transform.root.GetComponentInParent<IMovement>();

        targetConnectingJoint.connectedBody = null;
        targetHolder.gameObject.SetActive(false);

        targetAbilities.ActivationEnabled.RemoveModifier(false);
        targetMovement.ControlEnabled.RemoveModifier(false);
    }
}
