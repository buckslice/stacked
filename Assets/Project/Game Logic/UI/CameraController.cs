using UnityEngine;
using System.Collections.Generic;

using System.Linq;

// attach to the camera
public class CameraController : MonoBehaviour {

    public Transform boss { get; set; }
    
    private readonly Vector3 padding = Vector3.one * 4.0f;

    private float minFollowDistance = 20.0f;

    private Vector3 camVel = Vector3.zero;
    private Vector3 boundsCenter = Vector3.zero;

    // list of positions being tracked by camera this frame
    List<Vector3> trackingList = new List<Vector3>();

	// Use this for initialization
	void Start () {
        PopulateTrackingList();
        boundsCenter = GetTrackBounds().center;
	}
	
	// Update is called once per frame
	void Update () {
        PopulateTrackingList();

        Bounds bounds = GetTrackBounds();
        boundsCenter = Vector3.Lerp(boundsCenter, bounds.center, Time.deltaTime);

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

        // calculate the follow distance for this frame
        float maxBoundsDim = Mathf.Max(bounds.size.x, Mathf.Max(bounds.size.y, bounds.size.z));
        float camFollowDist = maxBoundsDim * 0.75f;
        if(camFollowDist < minFollowDistance) {
            camFollowDist = minFollowDistance;
        }

        // calculate target camera follow position
        Vector3 targetPos = transform.position + dir * (camFollowDist - xzDist);

        // find best local rotation (hill climbing)
        targetPos = FindLocalBestRotation(targetPos, boundsCenter);

        // set camera position using smoothdamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref camVel, 1.0f);

        // lastly look at center of bounds
        transform.LookAt(boundsCenter);
    }

    // get bounding box around all relevant game entities that camera should track
    Bounds GetTrackBounds() {
        if (trackingList.Count == 0) {
            return new Bounds();
        }

        // done like this to avoid including the origin
        Bounds bounds = new Bounds(trackingList[0], padding);
        for (int i = 1; i < trackingList.Count; ++i) {
            bounds.Encapsulate(new Bounds(trackingList[i], padding));
        }
        return bounds;
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
