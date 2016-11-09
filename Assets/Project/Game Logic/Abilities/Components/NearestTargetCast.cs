using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

/// <summary>
/// An action which triggers as a targeted action via raycasting against an area.
/// </summary>
[RequireComponent(typeof(IShape))]
public class NearestTargetCast : UntargetedAbilityConstraint, ITargetedAbilityTrigger {

    [SerializeField]
    protected LayerMask layermask;

    IShape shape;
    TargetedAbilityActivation targetedActivation;

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };
    public event TargetedTrigger targetedAbilityTriggerEvent = (target) => { };

    protected override void Start() {
        base.Start();
        Assert.IsTrue(GetComponentsInChildren<TargetedAbilityActivation>().Length == 1);
        targetedActivation = GetComponentInChildren<TargetedAbilityActivation>();
        shape = GetComponent<IShape>();
        Assert.IsTrue(GetComponents<IShape>().Length == 1);
    }

    public override bool isAbilityActivatible() {
        Collider[] possibleTargets = shape.Cast(layermask);
        foreach (Collider target in possibleTargets) {
            if(targetedActivation.Ready(target.gameObject)) {
                return true;
            }
        }
        return false;
    }

    public override void Activate() {
        Collider[] possibleTargets = shape.Cast(layermask);

        GameObject target = null;
        float targetDistance = float.MaxValue;

        foreach (Collider possibleTarget in possibleTargets) {
            float possibleDistance = (possibleTarget.ClosestPointOnBounds(transform.position) - transform.position).magnitude;

            if (possibleDistance < targetDistance) {
                if (targetedActivation.Ready(possibleTarget.gameObject)) {
                    target = possibleTarget.gameObject;
                    targetDistance = possibleDistance;
                }
            }
        }

        if (target != null) {
            targetedAbilityTriggerEvent(target);
        }
    }
}