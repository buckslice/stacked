using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class TouchedGroundConstraint : UntargetedAbilityConstraint {

    const float groundCheckDistance = 0.1f;

    [SerializeField]
    protected int numJumps = 2;
    
    [SerializeField]
    protected int currentJumps;

    //int layermask;

    protected override void Start() {
        base.Start();

        currentJumps = numJumps;
        //layermask = LayerMask.GetMask(Tags.Layers.StaticGeometryFloor);
    }

    public override bool isAbilityActivatible() {
        return currentJumps > 0;
        /*
        if (currentJumps > 0) {
            return true;
        } 
        //else {
        //    RaycastHit info;
        //    bool result = Physics.Raycast(this.transform.position, Vector3.down, out info, groundCheckDistance, layermask);
        //    if(result && info.collider.CompareTag(Tags.Floor)) {
        //        currentJumps = numJumps;
        //        return true;
        //    }
        //}

        return false;
         * */
    }

    public override void Activate() { currentJumps--; }

    void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag(Tags.Floor)) {
            currentJumps = numJumps;
        }
    }
}
