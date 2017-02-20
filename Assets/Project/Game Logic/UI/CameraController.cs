using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum CameraType {
    ROTATE,
    FIXED,
    TEST
}

// attach to the camera
public class CameraController : MonoBehaviour {

    public Transform boss { get; set; }
    public CameraType camType;
    public Vector3 centerOffset = Vector3.zero;
    public float camSmoothTime = 2.0f;
    private bool trackDeadPlayers = false;

    // fixed variables
    float padding = 0.2f;   // percentage of width / height
    float minHeight = 18.0f;
    Vector3 targetPos;

    // rotate variables
    float minFollowDistance = 20.0f;
    float startY = 5.0f;
    Vector3 camVel = Vector3.zero;
    Vector3 boundsCenter = Vector3.zero;

    // list of positions being tracked by camera this frame
    List<Vector3> trackingList = new List<Vector3>();

    Camera cam;

    // Use this for initialization
    void Start() {
        cam = Camera.main;
        PopulateTrackingList();
        boundsCenter = new BoundingSphere(trackingList).center;
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        PopulateTrackingList();

        if (targetPosOverride) {
            LerpTowardsTarget();
        } else {
            if (camType == CameraType.ROTATE) {
                UpdateCamTypeRotate();
            } else if (camType == CameraType.FIXED) {
                UpdateCamTypeFixed();
            } else if (camType == CameraType.TEST) {
                UpdateCamTypeTesting();
            }
        }
    }

    bool targetPosOverride = false;
    public void SetTargetOverride(Vector3 targ) {
        targetPos = targ;
        targetPosOverride = true;
    }
    public void RemoveTargetOverride() {
        targetPosOverride = false;
    }

    void UpdateCamTypeTesting() {

        Vector2 min, max;
        GetMinMax(out min, out max);

        Vector2 center = new Vector2(min.x + (max.x - min.x) / 2.0f, min.y + (max.y - min.y) / 2.0f);
        Vector2 dir = center - new Vector2(cam.pixelWidth / 2.0f, cam.pixelHeight / 2.0f);

        Vector3 move = Vector3.zero;

        if (max.x - min.x < cam.pixelWidth) {
            move += transform.forward;
        } else {
            move -= transform.forward;
        }

        if (dir.sqrMagnitude > 1.0f) {
            dir.Normalize();
            move += transform.right * dir.x + transform.up * dir.y;
        }

        transform.position = transform.position + move * Time.deltaTime * 10.0f;

    }

    void UpdateCamTypeRotate() {
        BoundingSphere bounds = new BoundingSphere(trackingList);
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
        if (dir.sqrMagnitude > 1.0f) {
            dir.Normalize();
        }

        // addded some magic variables here to also tweak camera height a bit
        // todo: make these public variables that make sense
        float camFollowDist = bounds.radius * 1.5f + 5.0f;
        float heightChange = bounds.radius * 0.6f;
        camFollowDist -= heightChange * 0.5f;

        if (camFollowDist < minFollowDistance) {
            camFollowDist = minFollowDistance;
        }

        // calculate target camera follow position
        Vector3 targetPos = transform.position + dir * (camFollowDist - xzDist);
        targetPos.y = startY + heightChange;

        // find best local rotation (hill climbing)
        targetPos = FindLocalBestRotation(targetPos, boundsCenter);

        // set camera position using smoothdamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref camVel, camSmoothTime);

        // lastly look at center of bounds
        transform.LookAt(boundsCenter);
    }

    void UpdateCamTypeFixed() {
        // return early if 1 or less tracked target
        if (trackingList.Count <= 1) {
            return;
        }
        // return early if all targets are really close to eachother
        float totalDist = 0.0f;
        for (int i = 0; i < trackingList.Count - 1; ++i) {
            totalDist += (trackingList[i] - trackingList[i + 1]).sqrMagnitude;
        }
        if (totalDist < 1.0f) {
            return;
        }
        // make sure camera is never looking up
        Vector3 rot = transform.eulerAngles;
        if (rot.x < 5.0f || rot.x > 90.0f) {
            transform.rotation = Quaternion.Euler(5.0f, rot.y, 0.0f);
        }

        Vector2 min, max;
        Vector3 startPos = transform.position;  // backup to restore to at the end
        transform.position = targetPos;        // start at target since it is usually close to solution
        // cache local directions since they wont change at all in this function
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;

        bool dir = true;
        int count = 0;  // to ensure accidental infinite loops wont freeze the game
        const float step = 1.0f;

        // move forward and backward in camera look direction
        while (count++ < 1000) {
            GetMinMax(out min, out max);
            bool shouldZoom = max.x - min.x < cam.pixelWidth;

            transform.position = transform.position + forward * (shouldZoom ? 1 : -1) * step;

            if ((shouldZoom && !dir) || (!shouldZoom && dir)) {
                break;
            }
        }

        // move towards direction of min max center (based on screen center)
        float lastSqrMag = float.PositiveInfinity;
        while (count++ < 1000) {
            GetMinMax(out min, out max);
            Vector2 center = new Vector2(min.x + (max.x - min.x) / 2.0f, min.y + (max.y - min.y) / 2.0f);
            Vector2 moveDir = center - new Vector2(cam.pixelWidth / 2.0f, cam.pixelHeight / 2.0f);
            float sqrMag = moveDir.sqrMagnitude;
            if (lastSqrMag <= sqrMag) {
                break;
            }
            lastSqrMag = sqrMag;

            moveDir.Normalize();
            transform.position = transform.position + (right * moveDir.x + up * moveDir.y).normalized * step;
        }

        if (count >= 1000) {
            Debug.LogWarning("Camera probably bugged");
        }
        //Debug.Log(count);

        // if y position is below height then move back along forward vector
        // place is it minimum height
        float ty = transform.position.y;
        if (ty < minHeight) {
            // will be moving camera backwards as long as camera is looking down
            transform.position += forward * (minHeight - ty) / forward.y;
        }

        // set final calculated position as the target and lerp towards it
        targetPos = transform.position;
        // reset transform.position back to start position
        transform.position = startPos;

        LerpTowardsTarget();
    }

    void LerpTowardsTarget() {
        // if target position is in front of us reduce smooth speed to slowly zoom in
        float dot = Vector3.Dot(transform.forward, (targetPos - transform.position).normalized);
        if (dot < 0.0f) {
            dot = 0.0f;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref camVel, camSmoothTime + dot);
        //transform.position = targetPos;   // for debugging

        transform.position = transform.position + centerOffset;
    }

    // returns min and max bounds of tracked things in viewport pixels
    // min and max will have same aspect ratio as camera
    void GetMinMax(out Vector2 min, out Vector2 max) {
        Vector3 s = cam.WorldToScreenPoint(trackingList[0]);
        if (s.z < 0) {  // if tracked position is behind camera then screen coordinates will be inverted
            s *= -1.0f;
        }
        min.x = max.x = s.x;
        min.y = max.y = s.y;
        for (int i = 1; i < trackingList.Count; ++i) {
            s = cam.WorldToScreenPoint(trackingList[i]);
            if (s.z < 0) {
                s *= -1.0f;
            }
            min.x = Mathf.Min(min.x, s.x);
            min.y = Mathf.Min(min.y, s.y);
            max.x = Mathf.Max(max.x, s.x);
            max.y = Mathf.Max(max.y, s.y);
        }

        float width = max.x - min.x;
        float height = max.y - min.y;
        if (width / height > cam.aspect) { // if current aspect ratio is > cam aspect, scale up height
            float halfDiff = (width / cam.aspect - height) / 2.0f;
            min.y -= halfDiff;
            max.y += halfDiff;
        } else {    // if less than scale up width
            float halfDiff = (height * cam.aspect - width) / 2.0f;
            min.x -= halfDiff;
            max.x += halfDiff;
        }

        min.x -= cam.pixelWidth * padding;
        max.x += cam.pixelWidth * padding;

        //min.y -= cmain.pixelHeight * padding;
        //max.y += cmain.pixelHeight * padding;
        min.y -= cam.pixelHeight * padding * 1.2f;   // favor players running down so they have more of warning
        max.y += cam.pixelHeight * padding * 0.8f;   // since players going towards top of screen can see more
    }

    Vector3 RotatePoint(Vector3 point, Vector3 pivot, Vector3 axis, float angle) {
        Vector3 dir = point - pivot;
        dir = Quaternion.AngleAxis(angle, axis) * dir;
        return dir + pivot;
    }

    // basic hill climbing algorithm to find best rotation
    // there should be exactly two solutions everytime, but should find the closer one
    Vector3 FindLocalBestRotation(Vector3 start, Vector3 center) {
        if (trackingList.Count <= 1) {
            return start;
        }

        float stepSize = 5.0f; // angle in degrees
        Vector3 curPos = start;
        bool dir = true;
        while (stepSize > 0.01f) {
            Vector3 prot = RotatePoint(curPos, center, Vector3.up, stepSize);
            Vector3 nrot = RotatePoint(curPos, center, Vector3.up, -stepSize);
            if (ScorePosition(prot) < ScorePosition(nrot)) {
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
        for (int i = 0; i < trackingList.Count; ++i) {
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
            if (sqrDist < min) {
                min = sqrDist;
            }
            if (sqrDist > max) {
                max = sqrDist;
            }
        }
        return max - min;
    }

    // gets list of positions camera should track
    void PopulateTrackingList() {
        trackingList.Clear();
        foreach (Player player in Player.Players) {
            if (trackDeadPlayers || !player.dead) {
                trackingList.Add(player.Holder.transform.position);
            }
        }
        if (boss != null) {
            trackingList.Add(boss.position);
        }
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
