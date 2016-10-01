using UnityEngine;
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
        transform.root.GetComponent<Rigidbody>().position = (transform.root.position + transform.root.forward * dashDistance);
    }

    protected IEnumerator DurationRoutine()
    {
        Vector3 startPosition = rigid.position;
        float startTime = Time.time;
        RaycastHit hit;
        float distance;
        if (Physics.CapsuleCast(rigid.position + Vector3.up * coll.radius, rigid.position + Vector3.up * (coll.height - coll.radius), coll.radius, rigid.transform.forward, out hit, dashDistance, layermask))
        {
            distance = hit.distance;
        }
        else
        {
            distance = dashDistance; //max distance
        }

        Vector3 endPosition = startPosition + distance * rigid.transform.forward;
        float endTime = startTime + (distance / dashDistance) * dashDuration;

        movement.controlEnabled = false;

        while (Time.time <= endTime)
        {
            float lerpProgress = Mathf.InverseLerp(startTime, endTime, Time.time);
            rigid.MovePosition(Vector3.Lerp(startPosition, endPosition, lerpProgress));
            yield return null;
            movement.controlEnabled = false;
        }

        rigid.MovePosition(endPosition);
        movement.controlEnabled = true;
    }
}
