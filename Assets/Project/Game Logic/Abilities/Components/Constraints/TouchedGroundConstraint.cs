using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class TouchedGroundConstraint : UntargetedAbilityConstraint {

    const float groundCheckDistance = 0.1f;

    [SerializeField]
    protected int numJumps = 2;

    int currentJumps;
    int layermask;

    protected override void Start() {
        base.Start();

        currentJumps = numJumps;
        layermask = LayerMask.GetMask(Tags.Layers.StaticGeometryFloor);
    }

    public override bool isAbilityActivatible() {
        if (currentJumps > 0) {
            return true;
        } else {
            bool result = Physics.Raycast(this.transform.position, Vector3.down, groundCheckDistance, layermask);
            if(result) {
                numJumps = currentJumps;
            }
            return true;
        }
    }

    public override void Activate() { currentJumps--; }

    void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Floor")) {
            currentJumps = numJumps;
        }
    }
}
