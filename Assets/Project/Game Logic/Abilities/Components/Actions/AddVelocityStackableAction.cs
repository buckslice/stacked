using UnityEngine;
using System.Collections;

public class AddVelocityStackableAction : AbstractAbilityAction {

    [SerializeField]
    protected Vector3 impulse = Vector3.up;

    Stackable stackable;

    protected override void Start() {
        base.Start();
        stackable = GetComponentInParent<Stackable>();
    }

    public override bool Activate(PhotonStream stream) {
        foreach (Rigidbody rigid in stackable.Rigidbodies()) {
            rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            rigid.velocity += impulse;
        }
        return true;
    }
}
