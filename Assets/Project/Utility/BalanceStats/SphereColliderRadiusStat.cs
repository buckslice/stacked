using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class SphereColliderRadiusStat : MonoBehaviour, IBalanceStat {

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch (type) {
            case BalanceStat.StatType.RADIUS:
            default:
                GetComponent<SphereCollider>().radius = new MultiplierFloatStat(value);
                break;
        }
    }
}
