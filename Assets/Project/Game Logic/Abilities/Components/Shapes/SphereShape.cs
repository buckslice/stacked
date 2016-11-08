using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class SphereShape : MonoBehaviour, IShape, IGenericStat {

    [SerializeField]
    protected MultiplierFloatStat radius = new MultiplierFloatStat(5f);

    public Collider[] Cast(LayerMask layermask) {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layermask);

        return hits;
    }

    float IBalanceStat.Value {
        set { radius = new MultiplierFloatStat(value); }
    }
}
