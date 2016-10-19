using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Technically overlap wedge, but cone is more intuitive.
/// </summary>
public class SphereShape : MonoBehaviour, IShape {

    [SerializeField]
    protected MultiplierFloatStat radius = new MultiplierFloatStat(5f);

    public Collider[] Cast(LayerMask layermask) {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layermask);

        return hits;
    }
}
