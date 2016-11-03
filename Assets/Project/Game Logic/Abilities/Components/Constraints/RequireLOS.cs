using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Requires that the target have a collider in line-of-sight.
/// </summary>
public class RequireLOS : TargetedAbilityConstraint {

    [SerializeField]
    protected LayerMask layermask;

    public override bool isAbilityActivatible(GameObject target) {
        Collider col = target.GetComponent<Collider>();
        if (col == null) { return false; }

        RaycastHit hit;
        Vector3 direction = col.bounds.center - transform.position;


        if (!Physics.Raycast(transform.position, direction, out hit, direction.magnitude, layermask)) {
            //if don't hit it, it's just not on our layermask
            return true;
        }

        return hit.collider == col;
    }

    public override void Activate(GameObject context) { }

}
