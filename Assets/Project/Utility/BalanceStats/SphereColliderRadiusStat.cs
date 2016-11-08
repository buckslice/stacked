using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class SphereColliderRadiusStat : MonoBehaviour, IGenericStat {

    float IBalanceStat.Value {
        set { GetComponent<SphereCollider>().radius = value; }
    }
}
