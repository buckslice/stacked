using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Technically overlap wedge, but cone is more intuitive.
/// </summary>
public class ArcShape : MonoBehaviour, IShape, IBalanceStat {

    [SerializeField]
    protected MultiplierFloatStat radius = new MultiplierFloatStat(5f);

    [SerializeField]
    protected float verticalOffset = 1f;

    /// <summary>
    /// The angle from the forward direction to an edge of the wedge, in degrees.
    /// </summary>
    [SerializeField]
    protected float angleDegHalved = 45;

    public Collider[] Cast(LayerMask layermask) {

        List<Collider> results = new List<Collider>();
        Collider[] hits = Physics.OverlapBox(transform.position + Vector3.up * verticalOffset, new Vector3(radius, 0.1f, radius), Quaternion.identity, layermask);

        Vector3 wedgeFoward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

        foreach (Collider hit in hits) {
            if (WedgeCast(hit, wedgeFoward, Vector3.up, transform.position, angleDegHalved, radius)) {
                results.Add(hit);
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Returns true if the collider is hit by the wedge.
    /// </summary>
    private static bool WedgeCast(Collider col, Vector3 wedgeDirection, Vector3 wedgeUp, Vector3 wedgePos, float angleDegHalved, float radius) {
        Vector3 displacement = col.bounds.center - wedgePos;
        displacement = Vector3.ProjectOnPlane(displacement, wedgeUp);

        //if the collider point is inside the volume of the wedge
        if (Vector3.Angle(displacement, wedgeDirection) <= angleDegHalved) {
            //if we are inside the radius
            if (displacement.magnitude < radius) {
                return true;
            } else {
                return DistanceCast(col, wedgePos, radius);
            }
        }

        //forward direction of the left edge of the wedge
        Vector3 leftPlane = Quaternion.AngleAxis(angleDegHalved, wedgeUp) * wedgeDirection;
        if (DistanceCast(col, wedgePos, leftPlane, radius)) {
            return true;
        }

        //forward direction of the right edge of the wedge
        Vector3 rightPlane = Quaternion.AngleAxis(-angleDegHalved, wedgeUp) * wedgeDirection;
        if (DistanceCast(col, wedgePos, rightPlane, radius)) {
            return true;
        }

        return false;
    }

    private static bool DistanceCast(Collider col, Vector3 pos, float radius) {
        return DistanceCast(col, pos, Vector3.ProjectOnPlane(col.bounds.center - pos, Vector3.up), radius);
    }

    private static bool DistanceCast(Collider col, Vector3 pos, Vector3 direction, float radius) {
        RaycastHit ignore;
        return col.Raycast(new Ray(pos, direction), out ignore, radius);
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch (type) {
            case BalanceStat.StatType.ANGLE:
                angleDegHalved = value;
                break;
            case BalanceStat.StatType.RADIUS:
            default:
                radius = new MultiplierFloatStat(value);
                break;
        }
    }
}
