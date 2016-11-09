using UnityEngine;
using System.Collections;

public class AddVelocityAction : AbstractAbilityAction {

    [SerializeField]
    protected Vector3 impulse = Vector3.up;

    Rigidbody rigid;

    protected override void Start() {
        base.Start();
        rigid = GetComponentInParent<Rigidbody>();
    }

    public override bool Activate(PhotonStream stream) {
        rigid.velocity += impulse;
        return true;
    }
}
