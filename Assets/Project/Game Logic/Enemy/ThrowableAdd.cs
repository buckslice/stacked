using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Stackable))]
public class ThrowableAdd : MonoBehaviour {
    [SerializeField]
    protected float horizontalThrowVelocity = 20;

    [SerializeField]
    protected float verticalThrowVelocity = 5;

    /// <summary>
    /// The collider on the add so that players can detect and pick them up. Should be disabled when grabbed
    /// </summary>
    [SerializeField]
    protected Collider stackableCollider;

    Stackable stackable;
    Rigidbody rigid;
    Stackable previousBelow;

	void Awake () {
        stackable = GetComponent<Stackable>();
        stackable.changeEvent += ThrowableAdd_changeEvent;
        rigid = GetComponent<Rigidbody>();
    }

    private void ThrowableAdd_changeEvent() {

        stackableCollider.enabled = stackable.Below == null; //enabled if we are not stacked

        if (previousBelow != null && stackable.Below == null) {
            //we were thrown from our stack
            if(previousBelow.isActiveAndEnabled) {
                //if player didn't die or something

                //throw self

                Vector3 throwDirection = previousBelow.GetComponent<PlayerInputHolder>().rotationDirection;
                Vector3.ProjectOnPlane(throwDirection, Vector3.up);
                throwDirection.Normalize();

                Vector3 newVelocity = horizontalThrowVelocity * throwDirection + verticalThrowVelocity * Vector3.up;
                rigid.velocity = newVelocity;
            }
        }

        previousBelow = stackable.Below;
    }
}
