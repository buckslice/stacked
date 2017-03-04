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
    Stackable stackable;

    int layermask;
    Coroutine activeRoutine;

    protected override void Start()
    {
        base.Start();
        coll = transform.root.GetComponentInChildren<CapsuleCollider>();
        rigid = GetComponentInParent<Rigidbody>();
        movement = GetComponentInParent<IMovement>();
        networking = rigid.GetComponent<AbilityNetworking>();
        stackable = transform.root.GetComponent<Stackable>();

        layermask = LayerMask.GetMask(Tags.Layers.StaticGeometry);
    }

    public override bool Activate(PhotonStream stream) {
        if (stackable.Below) {
            stackable.DisconnectBelow();

            // if we want to exit the stack same way as jumping out works (code untested)
            //if (stackable.Above) {
            //    Stackable above = stackable.Above;
            //    stackable.DisconnectAbove();
            //    stackable.Below.Grab(above);
            //}
        }

        Vector3 endPosition;

        if (stream.isWriting) {
            //calculate end point of the dash from our current position and rotation
            Vector3 direction = movement.CurrentMovement();
            // if player is not moving in any direction blink should just go forward
            if(!direction.magnitude.AlmostEquals(1, 0.01f)) {
                direction = rigid.transform.forward;
            }
            direction.y = 0.0f;
            //Assert.AreApproximatelyEqual(direction.y, 0);

            RaycastHit hit;
            float distance;
            if (Physics.CapsuleCast(rigid.position + Vector3.up * coll.radius,
                                    rigid.position + Vector3.up * (coll.height - coll.radius),
                                    coll.radius - 0.05f, direction, out hit, dashDistance, layermask)) {
                distance = hit.distance;
            } else {
                distance = dashDistance; //max distance
            }

            endPosition = rigid.position + distance * direction;
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
        dashingObjectAbility.Initialize(startPosition, endPosition, startTime, endTime, networking, movement, rigid);

        foreach (ProjectileProxy thrownObjectAbility in dashingObjectAbility.GetComponents<ThrownObjectAbility>()) {
            thrownObjectAbility.Initialize(networking, GetComponentInParent<IDamageHolder>());
        }

        //TODO: branch off into its own action?
        foreach (SmearSetup smear in instantiatedDashingObjectAbility.GetComponentsInChildren<SmearSetup>()) {
            smear.Initialize(transform.root.GetComponentInChildren<Collider>());
        }

        return true;
    }
}
