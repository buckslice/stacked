using UnityEngine;
using System.Collections;

public class DashAbility : AbstractAbilityAction
{
    [SerializeField]
    protected float dashDistance = 10f;

    [SerializeField]
    protected float dashDuration = 0.15f;

    PlayerMovement movement;
    PhotonView view;
    Rigidbody rigid;
    Coroutine activeRoutine;

    protected override void Start()
    {
        base.Start();
        rigid = GetComponentInParent<Rigidbody>();
        movement = GetComponentInParent<PlayerMovement>();
        view = GetComponentInParent<PhotonView>();
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
        movement.controlEnabled = false;

        float startTime = Time.time;
        float endTime = Time.time + dashDuration;

        Vector3 startPosition = rigid.position;
        Vector3 endPosition = startPosition + dashDistance * rigid.transform.forward;

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
