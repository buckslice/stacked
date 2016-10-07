using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class DashAbility : AbstractAbilityAction
{
    [SerializeField]
    protected float dashDistance = 10f;

    [SerializeField]
    protected float dashDuration = 0.15f;

    PlayerMovement movement;
    CapsuleCollider coll;
    Rigidbody rigid;

    int layermask;
    Coroutine activeRoutine;

    protected override void Start()
    {
        base.Start();
        coll = transform.root.GetComponentInChildren<CapsuleCollider>();
        rigid = GetComponentInParent<Rigidbody>();
        movement = GetComponentInParent<PlayerMovement>();

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

        if (activeRoutine != null) {
            StopCoroutine(activeRoutine);
        }
        activeRoutine = StartCoroutine(DurationRoutine(startPosition, endPosition, startTime, endTime));

        return true;
    }

    protected IEnumerator DurationRoutine(Vector3 startPosition, Vector3 endPosition, float startTime, float endTime)
    {
        movement.controlEnabled += false;
        movement.haltMovement();
        rigid.rotation = Quaternion.LookRotation(endPosition - startPosition);

        while (Time.time <= endTime)
        {
            float lerpProgress = Mathf.InverseLerp(startTime, endTime, Time.time);
            rigid.MovePosition(Vector3.Lerp(startPosition, endPosition, lerpProgress));
            yield return null;
        }

        rigid.MovePosition(endPosition);
        movement.controlEnabled -= false;
        movement.setVelocity((endPosition - startPosition).normalized);
    }
}
