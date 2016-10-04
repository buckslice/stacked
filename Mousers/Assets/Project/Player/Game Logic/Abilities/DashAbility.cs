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
        coll = GetComponentInParent<CapsuleCollider>();
        rigid = GetComponentInParent<Rigidbody>();
        movement = GetComponentInParent<PlayerMovement>();

        layermask = LayerMask.GetMask(Tags.Layers.StaticGeometry);
    }

    public override void Activate()
    {
        Vector3 direction = rigid.transform.forward;
        Assert.AreApproximatelyEqual(direction.magnitude, 1);
        Assert.AreApproximatelyEqual(direction.y, 0);
        RaycastHit hit;
        float distance;
        if (Physics.CapsuleCast(rigid.position + Vector3.up * coll.radius,
                                rigid.position + Vector3.up * (coll.height - coll.radius),
                                coll.radius - 0.05f, direction, out hit, dashDistance, layermask))
        {
            distance = hit.distance;
        }
        else
        {
            distance = dashDistance; //max distance
        }

        Vector3 endPosition = rigid.position + distance * direction;

        ActivateWithData(endPosition);

        //TODO: possibly use a vector2, since dash never has a vertical (y) component
        networkedActivation.ActivateRemoteWithData(endPosition);
    }

    public override void ActivateWithData(object data)
    {
        Vector3 startPosition = rigid.position;
        Vector3 endPosition = (Vector3)data;
        Vector3 direction = endPosition - startPosition;
        float startTime = Time.time;
        float distance = direction.magnitude;
        float endTime = startTime + (distance / dashDistance) * dashDuration;

        if (activeRoutine != null)
        {
            StopCoroutine(activeRoutine);
        }
        activeRoutine = StartCoroutine(DurationRoutine(startPosition, endPosition, startTime, endTime));
    }

    public override void ActivateRemote()
    {
    }

    protected IEnumerator DurationRoutine(Vector3 startPosition, Vector3 endPosition, float startTime, float endTime)
    {
        movement.controlEnabled = false;
        movement.haltMovement();
        rigid.rotation = Quaternion.LookRotation(endPosition - startPosition);

        while (Time.time <= endTime)
        {
            float lerpProgress = Mathf.InverseLerp(startTime, endTime, Time.time);
            rigid.MovePosition(Vector3.Lerp(startPosition, endPosition, lerpProgress));
            yield return null;
            movement.controlEnabled = false;
        }

        rigid.MovePosition(endPosition);
        movement.controlEnabled = true;
        movement.setVelocity((endPosition - startPosition).normalized);
    }
}
