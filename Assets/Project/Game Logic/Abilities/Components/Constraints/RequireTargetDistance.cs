using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RequireTargetDistance : TargetedAbilityConstraint, IBalanceStat {

    [SerializeField]
    protected float distance;

    public override void Activate(GameObject context) { }

    public override bool isAbilityActivatible(GameObject target) {
        return Vector3.Distance(target.transform.position, this.transform.position) <= distance;
    }

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch (type) {
            case BalanceStat.StatType.RANGE:
            case BalanceStat.StatType.RADIUS:
            default:
                distance = value;
                break;
        }

    }
}
