using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Technically overlap wedge, but cone is more intuitive.
/// </summary>
public class OverlapCone : AbstractAreaCast {

    const float height = 50f;

    [SerializeField]
    protected MultiplierFloatStat radius = new MultiplierFloatStat(5f);

    /// <summary>
    /// The angle from the forward direction to an edge of the wedge, in degrees.
    /// </summary>
    [SerializeField]
    protected float angleDegHalved = 45;

    protected override Collider[] performCast() {

        List<Collider> results = new List<Collider>();
        Collider[] hits = Physics.OverlapCapsule(transform.position, transform.position + height * Vector3.up, radius, layermask);

        Vector3 wedgeFoward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

        foreach (Collider hit in hits) {
            if (WedgeCast(hit, wedgeFoward, Vector3.up, transform.position, angleDegHalved)) {
                results.Add(hit);
                //Debug.Log(hit);
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Returns true if the collider is hit by the wedge.
    /// </summary>
    private static bool WedgeCast(Collider col, Vector3 wedgeDirection, Vector3 wedgeUp, Vector3 wedgePos, float angleDegHalved) {
        Vector3 displacement = col.bounds.center - wedgePos;
        displacement = Vector3.ProjectOnPlane(displacement, wedgeUp);

        //if the collider point is inside the volume of the wedge
        if (Vector3.Angle(displacement, wedgeDirection) <= angleDegHalved) {
            return true;
        }

        //forward direction of the left edge of the wedge
        Vector3 leftPlane = Quaternion.AngleAxis(angleDegHalved, wedgeUp) * wedgeDirection;
        if (PlaneCast(col, leftPlane, wedgeUp, wedgePos)) {
            return true;
        }

        //forward direction of the right edge of the wedge
        Vector3 rightPlane = Quaternion.AngleAxis(-angleDegHalved, wedgeUp) * wedgeDirection;
        if (PlaneCast(col, rightPlane, wedgeUp, wedgePos)) {
            return true;
        }

        return false;
    }

    private static bool PlaneCast(Collider col, Vector3 planeForward, Vector3 planeUp, Vector3 planePos) {
        Vector3 planeNormal = Vector3.Cross(planeUp, planeForward);

        Vector3 displacement = col.bounds.center - planePos;

        //There is a point on the collider which is closest to the plane. This is the vector from the wedge center to that point
        Vector3 raycastDirection = Vector3.ProjectOnPlane(displacement, planeNormal);

        if (Vector3.Dot(raycastDirection, planeForward) < 0) {
            //raycast is backwards, outside of the plane
            return false;
        }

        //Decompose the vector along the plane coordinates
        Vector3 raycastForward = Vector3.Project(raycastDirection, planeForward);
        Vector3 raycastUp = raycastDirection - raycastForward;

        //check we did the decomposition correctly
        Assert.IsTrue(raycastUp == Vector3.Project(raycastDirection, planeUp), string.Format("{0} {1}", raycastUp, Vector3.Project(raycastDirection, planeUp)));

        //raycast along the forward direction of the plane, to better compensate for differences in elevation (remember, this is intended for a wedge)
        //should still be the same if the collider is convex.
        RaycastHit ignore;
        Ray ray = new Ray(planePos + raycastUp, raycastForward);
        bool result = col.Raycast(ray, out ignore, raycastForward.magnitude);
        return result;
    }
}
