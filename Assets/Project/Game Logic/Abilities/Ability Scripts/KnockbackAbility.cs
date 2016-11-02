using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class KnockbackAbility : TypedTargetedAbilityAction {

    [SerializeField]
    protected float knockbackDistance = 10f;

    [SerializeField]
    protected float knockbackDuration = 0.15f;

    [SerializeField]
    protected GameObject knockbackedObjectPrefab;

    int layermask;

    protected override void Awake() {
        base.Awake();
        layermask = LayerMask.GetMask(Tags.Layers.StaticGeometry);
    }

    public override bool isAbilityActivatible(GameObject target) {
        CapsuleCollider coll = target.GetComponentInParent<CapsuleCollider>();
        if (coll == null) { return false; }

        Rigidbody rigid = target.GetComponentInParent<Rigidbody>();
        if (rigid == null) { return false; }

        IMovement movement = target.GetComponentInParent<IMovement>();
        if (movement == null) { return false; }

        AbilityNetworking networking = rigid.GetComponent<AbilityNetworking>();
        if (networking == null) { return false; }

        return true;
    }

    public override bool Activate(GameObject context, PhotonStream stream) {
        CapsuleCollider coll = context.GetComponent<CapsuleCollider>();
        Rigidbody rigid = context.GetComponentInParent<Rigidbody>();
        IMovement movement = context.GetComponentInParent<IMovement>();
        AbilityNetworking networking = rigid.GetComponent<AbilityNetworking>();

        Vector3 endPosition;

        if (stream.isWriting) {
            //calculate end point of the dash from our current position and rotation
            Vector3 direction = Vector3.ProjectOnPlane(context.transform.position - this.transform.position, Vector3.up);
            direction.Normalize();

            RaycastHit hit;
            float distance;

            if (Physics.CapsuleCast(rigid.position + Vector3.up * coll.radius,
                                    rigid.position + Vector3.up * (coll.height - coll.radius),
                                    coll.radius - 0.05f, direction, out hit, knockbackDistance, layermask)) {
                distance = hit.distance;
            } else {
                distance = knockbackDistance; //max distance
            }

            endPosition = rigid.position + distance * direction;
            stream.SendNext(endPosition);
            //TODO: possibly use a vector2, since dash never has a vertical (y) component

        } else {
            endPosition = (Vector3)stream.ReceiveNext();
        }

        Vector3 startPosition = rigid.position;
        Vector3 knockbackDirection = endPosition - startPosition;
        float startTime = Time.time;
        float knockbackMagnitude = knockbackDirection.magnitude;
        float endTime = startTime + (knockbackMagnitude / knockbackDistance) * knockbackDuration;

        GameObject instantiatedKnockbackedObjectAbility = SimplePool.Spawn(knockbackedObjectPrefab);
        DashingObjectAbility dashingObjectAbility = instantiatedKnockbackedObjectAbility.GetComponent<DashingObjectAbility>();
        dashingObjectAbility.Initialize(startPosition, endPosition, startTime, endTime, networking, movement, rigid);

        foreach (ProjectileProxy thrownObjectAbility in dashingObjectAbility.GetComponents<ThrownObjectAbility>()) {
            thrownObjectAbility.Initialize(networking, GetComponentInParent<IDamageHolder>());
        }

        //TODO: branch off into its own action?
        foreach (SmearSetup smear in instantiatedKnockbackedObjectAbility.GetComponentsInChildren<SmearSetup>()) {
            smear.Initialize(context.transform.root.GetComponentInChildren<Collider>());
        }

        return true;
    }
}
