using UnityEngine;
using System.Collections.Generic;


// attach to the camera
public class CameraController : MonoBehaviour {

    public Transform boss { get; set; }

    public bool shouldRotate = true;
    
    private readonly Vector3 padding = Vector3.one * 4.0f;

    private float minFollowDistance = 20.0f;
    private float startY = 5.0f;

    private Vector3 camVel = Vector3.zero;
    private Vector3 boundsCenter = Vector3.zero;

    // list of positions being tracked by camera this frame
    List<Vector3> trackingList = new List<Vector3>();

	// Use this for initialization
	void Start () {
        PopulateTrackingList();
        boundsCenter = new BoundingSphere(trackingList).center;
    }
	
	// Update is called once per frame
	void Update () {
        PopulateTrackingList();
        BoundingSphere boundingSphere = new BoundingSphere(trackingList);
        boundsCenter = Vector3.Lerp(boundsCenter, boundingSphere.center, Time.deltaTime);

        // find XZ distance from camera to bounds center
        Vector3 xzCam = transform.position;
        xzCam.y = 0.0f;
        Vector3 xzBounds = boundsCenter;
        xzBounds.y = 0.0f;
        float xzDist = Vector3.Distance(xzCam, xzBounds);

        // get normalized direction to center of all players bounds
        Vector3 dir = transform.position - boundsCenter;
        dir.y = 0.0f;
        if(dir.sqrMagnitude > 1.0f) {
            dir.Normalize();
        }

        // addded some magic variables here to also tweak camera height a bit
        float camFollowDist = boundingSphere.radius * 1.5f + 5.0f;
        float heightChange = boundingSphere.radius * 0.6f;
        camFollowDist -= heightChange * 0.5f;
        
        if(camFollowDist < minFollowDistance) {
            camFollowDist = minFollowDistance;
        }

        // calculate target camera follow position
        Vector3 targetPos = transform.position + dir * (camFollowDist - xzDist);
        targetPos.y = startY + heightChange;

        if (shouldRotate) {
            // find best local rotation (hill climbing)
            targetPos = FindLocalBestRotation(targetPos, boundsCenter);
        }
        // if someone moves toward camera then scoot it back
        // if people running far on the sides then zoom out

        // set camera position using smoothdamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref camVel, 200.0f * Time.deltaTime);

        // lastly look at center of bounds
        transform.LookAt(boundsCenter);
    }

    // gets list of positions camera should track
    void PopulateTrackingList() {
        trackingList.Clear();
        foreach (Player player in Player.Players) {
            trackingList.Add(player.Holder.transform.position);
        }
        if (boss != null) {
            trackingList.Add(boss.position);
        }
    }

    Vector3 RotatePoint(Vector3 point, Vector3 pivot, Vector3 axis, float angle) {
        Vector3 dir = point - pivot;
        dir = Quaternion.AngleAxis(angle, axis) * dir;
        return dir + pivot;
    }

    // basic hill climbing algorithm to find best rotation
    // there should be exactly two solutions everytime, but should find the closer one
    Vector3 FindLocalBestRotation(Vector3 start, Vector3 center) {
        if(trackingList.Count <= 1) {
            return start;
        }

        float stepSize = 5.0f; // angle in degrees
        Vector3 curPos = start;
        bool dir = true;
        while (stepSize > 0.01f) {
            Vector3 prot = RotatePoint(curPos, center, Vector3.up, stepSize);
            Vector3 nrot = RotatePoint(curPos, center, Vector3.up, -stepSize);
            if(ScorePosition(prot) < ScorePosition(nrot)) {
                curPos = prot;
                if (!dir) {
                    dir = !dir;
                    stepSize *= 0.5f;
                }
            } else {
                curPos = nrot;
                if (dir) {
                    dir = !dir;
                    stepSize *= 0.5f;
                }
            }
        }
        return curPos;
    }

    float ScorePosition(Vector3 pos) {
        //return GetTotalSquaredDistance(pos);
        return GetMinMaxDiff(pos);
    }

    // gets the total squared distance from provided pos to each tracked position
    float GetTotalSquaredDistance(Vector3 pos) {
        float dist = 0.0f;
        for(int i = 0; i < trackingList.Count; ++i) {
            dist += (pos - trackingList[i]).sqrMagnitude;
        }
        return dist;
    }

    // returns the difference between the min and max distances of tracked positions from pos
    float GetMinMaxDiff(Vector3 pos) {
        float min = float.PositiveInfinity;
        float max = 0.0f;
        for (int i = 0; i < trackingList.Count; ++i) {
            float sqrDist = (pos - trackingList[i]).sqrMagnitude;
            if(sqrDist < min) {
                min = sqrDist;
            }
            if(sqrDist > max) {
                max = sqrDist;
            }
        }
        return max - min;
    }
}


public class BoundingSphere {
    public Vector3 center;
    public float radius;

    public BoundingSphere(Vector3 center, float radius) {
        this.center = center;
        this.radius = radius;
    }

    // bouncing bubble algorithm
    public BoundingSphere(List<Vector3> points) {
        if (points.Count == 0) {
            return;
        }
        Vector3 center = points[0];
        float radius = 0.0001f;
        Vector3 pos, d;
        float len, alpha, alphaSqr;

        for (int j = 0; j < 2; j++) {
            for (int i = 0; i < points.Count; i++) {
                pos = points[i];
                d = pos - center;
                if (d.sqrMagnitude > radius * radius) {
                    alpha = d.magnitude / radius;
                    alphaSqr = alpha * alpha;
                    radius = 0.5f * (alpha + 1 / alpha) * radius;
                    center = 0.5f * ((1 + 1 / alphaSqr) * center + (1 - 1 / alphaSqr) * pos);
                }
            }
        }

        for (int i = 0; i < points.Count; i++) {
            pos = points[i];
            d = pos - center;
            if (d.sqrMagnitude > radius * radius) {
                len = d.magnitude;
                radius = (radius + len) / 2.0f;
                center = center + ((len - radius) / len * d);
            }
        }

        this.center = center;
        this.radius = radius;
    }

}
