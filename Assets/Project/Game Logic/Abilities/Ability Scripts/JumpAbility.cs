using UnityEngine;
using System.Collections;

public class JumpAbility : AbstractAbilityAction {
    public static int MAX_JUMPS = 2;

    [SerializeField]
    protected float jumpStrength = 100f;

    private int jumps;

    Rigidbody rigid;

    protected override void Start() {
        base.Start();
        rigid = GetComponentInParent<Rigidbody>();

        jumps = MAX_JUMPS;
    }

    public override bool Activate(PhotonStream stream) {
        if (jumps > 0) {
            rigid.velocity = new Vector3(rigid.velocity.x, (Vector3.up * jumpStrength).y, rigid.velocity.z);
            jumps -= 1;
        }
        return true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Floor")) {
            jumps = MAX_JUMPS;
        }
    }
}
