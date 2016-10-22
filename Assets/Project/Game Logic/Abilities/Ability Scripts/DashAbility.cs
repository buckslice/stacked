using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class DashAbility : AbstractAbilityAction
{
    [SerializeField]
    protected float dashDistance = 10f;

    [SerializeField]
    protected float dashDuration = 0.15f;

    [SerializeField]
    protected GameObject dashingObjectPrefab;

    IMovement movement;
    CapsuleCollider coll;
    Rigidbody rigid;
    AbilityNetworking networking;

    int layermask;
    Coroutine activeRoutine;

    protected override void Start()
    {
        base.Start();
        coll = transform.root.GetComponentInChildren<CapsuleCollider>();
        rigid = GetComponentInParent<Rigidbody>();
        movement = GetComponentInParent<IMovement>();
        networking = rigid.GetComponent<AbilityNetworking>();

        layermask = LayerMask.GetMask(Tags.Layers.StaticGeometry);
    }

    public override bool Activate(PhotonStream stream) {
        Vector3 endPosition;

        if (stream.isWriting) {
            //calculate end point of the dash from our current position and rotation
            Vector3 playerDirection = rigid.transform.forward;
            Assert.AreApproximatelyEqual(playerDirection.magnitude, 1);
            Assert.AreApproximatelyEqual(playerDirection.y, 0);

            RaycastHit hit;
            float distance;
            if (Physics.CapsuleCast(rigid.position + Vector3.up * coll.radius,
                                    rigid.position + Vector3.up * (coll.height - coll.radius),
                                    coll.radius - 0.05f, playerDirection, out hit, dashDistance, layermask)) {
                distance = hit.distance;
            } else {
                distance = dashDistance; //max distance
            }

            endPosition = rigid.position + distance * playerDirection;
            stream.SendNext(endPosition);
            //TODO: possibly use a vector2, since dash never has a vertical (y) component

        } else {
            endPosition = (Vector3)stream.ReceiveNext();
        }

        Vector3 startPosition = rigid.position;
        Vector3 dashDirection = endPosition - startPosition;
        float startTime = Time.time;
        float dashMagnitude = dashDirection.magnitude;
        float endTime = startTime + (dashMagnitude / dashDistance) * dashDuration;

        GameObject instantiatedDashingObjectAbility = SimplePool.Spawn(dashingObjectPrefab);
        DashingObjectAbility dashingObjectAbility = instantiatedDashingObjectAbility.GetComponent<DashingObjectAbility>();
        dashingObjectAbility.Initialize(transform.position, endPosition, startTime, endTime, networking, movement, rigid);

        return true;
    }
}
