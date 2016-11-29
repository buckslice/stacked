using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class TouchedGroundConstraint : UntargetedAbilityConstraint {

    const float groundCheckDistance = 1f;

    [SerializeField]
    protected int numJumps = 2;
    
    [SerializeField]
    protected int currentJumps;

    int layermask;

    protected override void Start() {
        base.Start();

        currentJumps = numJumps;
        layermask = LayerMask.GetMask(Tags.Layers.StaticGeometryFloor);
    }

    public override bool isAbilityActivatible() {
        if (currentJumps > 0) {
            return true;
        }
        else {
            RaycastHit info;
            bool result = Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y+1, this.transform.position.z), Vector3.down, out info, groundCheckDistance, layermask);
            if (result && info.collider.CompareTag(Tags.Floor)) {
                currentJumps = numJumps;
                return true;
            }
        }

        return false;
    }

    public override void Activate() { currentJumps--; }
}
