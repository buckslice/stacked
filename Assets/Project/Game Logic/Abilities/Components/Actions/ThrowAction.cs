using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class ThrowAction : TypedTargetedAbilityAction {

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

    public override bool isAbilityActivatible(GameObject target) {
        if (target.GetComponentInParent<Rigidbody>() == null) {
            return false;
        }

        if (target.GetComponentInParent<IMovement>() == null) {
            return false;
        }

        if (target.GetComponentInParent<AbilityNetworking>() == null) {
            return false;
        }

        IDamageTracker targetReference = target.GetComponentInParent<IDamageHolder>().GetRootDamageTracker();
        return targetReference != trackerReference;
    }

    public override bool Activate(GameObject context, PhotonStream stream) {
        Vector3 destinationPosition = transform.position + throwDistance * Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        AbilityNetworking targetNetworking = context.GetComponentInParent<AbilityNetworking>();
        IMovement targetMovement = context.GetComponentInParent<IMovement>();
        Rigidbody targetRigid = context.GetComponentInParent<Rigidbody>();

        GameObject instantiatedThrownObjectAbility = SimplePool.Spawn(thrownObjectAbilityPrefab);
        ThrownObjectAbility thrownObjectAbility = instantiatedThrownObjectAbility.GetComponent<ThrownObjectAbility>();
        thrownObjectAbility.Initialize(transform.position, destinationPosition, Time.time + throwDuration, targetNetworking, targetMovement, targetRigid);
        return true;
    }
}
