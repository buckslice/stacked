using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class SphereShape : MonoBehaviour, IShape, IBalanceStat {

    [SerializeField]
    protected MultiplierFloatStat radius = new MultiplierFloatStat(5f);

    public Collider[] Cast(LayerMask layermask) {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layermask);

        return hits;
    }

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch (type) {
            case BalanceStat.StatType.RADIUS:
            default:
                radius = new MultiplierFloatStat(value);
                break;
        }
    }
}
