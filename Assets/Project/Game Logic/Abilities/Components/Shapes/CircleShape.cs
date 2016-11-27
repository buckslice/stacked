using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class CircleShape : MonoBehaviour, IShape, IBalanceStat {

    [SerializeField]
    protected float radius = 1;

    [SerializeField]
    protected bool allowSelf = false;

    Collider[] IShape.Cast(LayerMask layermask) {

        List<Collider> results = new List<Collider>();
        Collider[] hits = Physics.OverlapBox(transform.position, new Vector3(radius, 0.05f, radius), Quaternion.identity, layermask);
        foreach (Collider hit in hits) {

            if (!allowSelf && hit.transform.root == this.transform.root) { continue; }

            Vector2 center = hit.bounds.center;
            Vector2 displacement = center - (Vector2)this.transform.position;
            if(displacement.magnitude < radius) {
                results.Add(hit);
            } else {
                Ray ray = new Ray(this.transform.position, displacement);
                RaycastHit ignore;
                bool result = hit.Raycast(ray, out ignore, radius);
                if (result) {
                    results.Add(hit);
                }
            }
        }

        return results.ToArray();
    }

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch (type) {
            case BalanceStat.StatType.RADIUS:
            default:
                radius = value;
                break;
        }
    }
}
