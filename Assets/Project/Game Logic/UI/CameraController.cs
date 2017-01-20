using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum CameraType {
    ROTATE,
    FIXED
}

// attach to the camera
public class CameraController : MonoBehaviour {

    public Transform boss { get; set; }
    public CameraType camType;
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

    Camera mainCam;

    // Use this for initialization
    void Start() {
        mainCam = Camera.main;
        PopulateTrackingList();
        boundsCenter = new BoundingSphere(trackingList).center;
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        PopulateTrackingList();

        if (camType == CameraType.ROTATE) {
            UpdateCamTypeRotate();
        } else if (camType == CameraType.FIXED) {
            UpdateCamTypeFixed();
        }
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

        Vector2 min, max;
        Vector3 startPos = transform.position;  // backup to restore to at the end
        transform.position = targetPos; // start at target since it is usually close to solution

        // make sure camera is never looking up
        Vector3 rot = transform.eulerAngles;
        if (rot.x < 5.0f || rot.x > 90.0f) {
            transform.rotation = Quaternion.Euler(5.0f, rot.y, 0.0f);
        }

        float startStep = 2.0f;
        const float stepThresh = 1.0f;  // larger steps cause gridlike movement
        float step = startStep;
        bool dir = true;
        int count = 0;
        // move forward and backward in camera look direction
        Vector3 forward = transform.forward;
        while (step > stepThresh) {
            Vector3 posi = transform.position + forward * step;
            Vector3 poso = transform.position - forward * step;

            transform.position = posi;
            GetMinMax(out min, out max);
            float scoref = Mathf.Abs(1.0f - (max.x - min.x) / mainCam.pixelWidth);
            transform.position = poso;
            GetMinMax(out min, out max);
            float scoreb = Mathf.Abs(1.0f - (max.x - min.x) / mainCam.pixelWidth);

            if (scoref < scoreb) {
                if (!dir) {
                    dir = !dir;
                    step *= 0.5f;
                }
                transform.position = posi;
            } else {
                if (dir) {
                    dir = !dir;
                    step *= 0.5f;
                }
                transform.position = poso;
            }
            count++;
        }

        // if y position is below height then move back along forward vector
        // place is it minimum height
        float ty = transform.position.y;
        if (ty < minHeight) {
            // will be moving camera backwards as long as camera is looking down
            transform.position += forward * (minHeight - ty) / forward.y;
        }

        // move left right on xz plane
        step = startStep;
        Vector3 right = transform.right;
        while (step > stepThresh) {
            Vector3 posl = transform.position + right * step;
            Vector3 posr = transform.position - right * step;

            transform.position = posl;
            GetMinMax(out min, out max);
            float scorel = Mathf.Abs((min.x + (max.x - min.x) / 2.0f) - mainCam.pixelWidth / 2.0f);
            transform.position = posr;
            GetMinMax(out min, out max);
            float scorer = Mathf.Abs((min.x + (max.x - min.x) / 2.0f) - mainCam.pixelWidth / 2.0f);

            if (scorel < scorer) {
                if (!dir) {
                    dir = !dir;
                    step *= 0.5f;
                }
                transform.position = posl;
            } else {
                if (dir) {
                    dir = !dir;
                    step *= 0.5f;
                }
                transform.position = posr;
            }
            count++;
        }

        step = startStep;
        // move forward and backward on xz plane (check for when looking straight down)
        if (Mathf.Abs(forward.x) < 0.01f && Mathf.Abs(forward.z) < 0.01f) {
            forward = transform.up;
        }
        forward.y = 0.0f;
        forward.Normalize();
        while (step > stepThresh) {
            Vector3 posf = transform.position + forward * step;
            Vector3 posb = transform.position - forward * step;

            transform.position = posf;
            GetMinMax(out min, out max);
            float scoref = Mathf.Abs((min.y + (max.y - min.y) / 2.0f) - mainCam.pixelHeight / 2.0f);
            transform.position = posb;
            GetMinMax(out min, out max);
            float scoreb = Mathf.Abs((min.y + (max.y - min.y) / 2.0f) - mainCam.pixelHeight / 2.0f);

            if (scoref < scoreb) {
                if (!dir) {
                    dir = !dir;
                    step *= 0.5f;
                }
                transform.position = posf;
            } else {
                if (dir) {
                    dir = !dir;
                    step *= 0.5f;
                }
                transform.position = posb;
            }
            count++;
        }
        //Debug.Log(count);

        // set final calculated position as the target and lerp towards it
        targetPos = transform.position;

        // if target position is in front of us reduce smooth speed to slowly zoom in
        float dot = Vector3.Dot(transform.forward, (targetPos - startPos).normalized);
        if (dot < 0.0f) {
            dot = 0.0f;
        }

        transform.position = Vector3.SmoothDamp(startPos, targetPos, ref camVel, camSmoothTime + dot);
        //transform.position = targetPos;   // for debugging

    }

    // returns min and max bounds of tracked things in viewport pixels
    void GetMinMax(out Vector2 min, out Vector2 max) {
        Vector3 s = mainCam.WorldToScreenPoint(trackingList[0]);
        min.x = max.x = s.x;
        min.y = max.y = s.y;
        for (int i = 1; i < trackingList.Count; ++i) {
            s = mainCam.WorldToScreenPoint(trackingList[i]);
            min.x = Mathf.Min(min.x, s.x);
            min.y = Mathf.Min(min.y, s.y);
            max.x = Mathf.Max(max.x, s.x);
            max.y = Mathf.Max(max.y, s.y);
        }

        float width = max.x - min.x;
        float height = max.y - min.y;
        if (width / height > mainCam.aspect) { // if current aspect ration is > cam aspect, scale up height
            float halfDiff = (width / mainCam.aspect - height) / 2.0f;
            min.y -= halfDiff;
            max.y += halfDiff;
        } else {    // if less than scale up width
            float halfDiff = (height * mainCam.aspect - width) / 2.0f;
            min.x -= halfDiff;
            max.x += halfDiff;
        }

        min.x -= mainCam.pixelWidth * padding;
        max.x += mainCam.pixelWidth * padding;

        //min.y -= cmain.pixelHeight * padding;
        //max.y += cmain.pixelHeight * padding;
        min.y -= mainCam.pixelHeight * padding * 1.2f;   // favor players running down so they have more of warning
        max.y += mainCam.pixelHeight * padding * 0.8f;   // since players going towards top of screen can see more
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
