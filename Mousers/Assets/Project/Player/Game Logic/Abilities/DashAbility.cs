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
        if (view.isMine)
        {
            if (activeRoutine != null)
            {
                StopCoroutine(activeRoutine);
            }
            activeRoutine = StartCoroutine(DurationRoutine());
        }
    }

    public override void ActivateRemote()
    {
    }

    protected IEnumerator DurationRoutine()
    {
        Vector3 direction = rigid.transform.forward;
        Assert.AreApproximatelyEqual(direction.magnitude, 1);
        Assert.AreApproximatelyEqual(direction.y, 0);
        Vector3 startPosition = rigid.position;
        float startTime = Time.time;
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

        Vector3 endPosition = startPosition + distance * direction;
        float endTime = startTime + (distance / dashDistance) * dashDuration;

        movement.controlEnabled = false;
        movement.haltMovement();

        while (Time.time <= endTime)
        {
            float lerpProgress = Mathf.InverseLerp(startTime, endTime, Time.time);
            rigid.MovePosition(Vector3.Lerp(startPosition, endPosition, lerpProgress));
            yield return null;
            movement.controlEnabled = false;
        }

        rigid.MovePosition(endPosition);
        movement.controlEnabled = true;
        movement.setVelocity(direction);
    }
}
