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

    public LayerMask groundLayer;
    public bool trackDeadPlayers = true;

    float minFollowDistance = 20.0f;
    float startY = 5.0f;

    Vector3 camVel = Vector3.zero;
    Vector3 boundsCenter = Vector3.zero;

    public Image topLeft;
    public Image botLeft;
    public Image topRight;
    public Image botRight;

    // list of positions being tracked by camera this frame
    List<Vector3> trackingList = new List<Vector3>();

    Plane[] frustum;
    BoundingSphere bounds;

    // Use this for initialization
    void Start() {
        PopulateTrackingList();
        boundsCenter = new BoundingSphere(trackingList).center;
    }

    // Update is called once per frame
    void Update() {
        PopulateTrackingList();
        bounds = new BoundingSphere(trackingList);
        boundsCenter = Vector3.Lerp(boundsCenter, bounds.center, Time.deltaTime);

        if (camType == CameraType.ROTATE) {
            UpdateCamTypeRotate();
        } else if (camType == CameraType.FIXED) {
            UpdateCamTypeFixed();
        }
    }

    void UpdateCamTypeRotate() {
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
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref camVel, 2.0f);

        // lastly look at center of bounds
        transform.LookAt(boundsCenter);
    }

    void UpdateCamTypeFixed() {
        Camera cmain = Camera.main;
        Vector2 min, max;

        // at this point same aspect ratio
        // zoom in first then left right forward backward
        Vector3 startPos = transform.position;  // backup to restore to at the end

        int counter = 1000;

        float step = 10.0f;
        float stepThresh = 0.01f;
        bool dir = true;
        Vector3 forward = transform.forward;
        while (step > stepThresh && --counter > 0) {
            Vector3 posf = transform.position + forward * step;
            Vector3 posb = transform.position - forward * step;

            transform.position = posf;
            GetMinMax(out min, out max);
            float scoref = Mathf.Abs(1.0f - (max.x - min.x) / cmain.pixelWidth);
            transform.position = posb;
            GetMinMax(out min, out max);
            float scoreb = Mathf.Abs(1.0f - (max.x - min.x) / cmain.pixelWidth);
            //Debug.Log(scoref + " " + scoreb);

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
        }

        if(counter <= 0) {
            Debug.Break();
        }

        step = 10.0f;
        Vector3 right = transform.right;
        while(step > stepThresh) {
            
        }

        //if(min.x + (max.x - min.x)/2.0f < (cmain.pixelWidth / 2.0f)) {
        //    transform.position -= transform.right * 0.1f;
        //} else {
        //    transform.position += transform.right * 0.1f;
        //}

        //Vector3 f = transform.forward;
        //f.y = 0.0f;
        //f.Normalize();
        //if (min.y < 0) {
        //    transform.position -= f * 0.1f;
        //} else {
        //    transform.position += f * 0.1f; 
        //}


        //topLeft.rectTransform.position = new Vector2(min.x, max.y);
        //botLeft.rectTransform.position = new Vector2(min.x, min.y);
        //topRight.rectTransform.position = new Vector2(max.x, max.y);
        //botRight.rectTransform.position = new Vector2(max.x, min.y);


    }

    // returns min and max bounds of tracked things in viewport pixels
    void GetMinMax(out Vector2 min, out Vector2 max) {
        Camera cmain = Camera.main;
        Vector3 s = cmain.WorldToScreenPoint(trackingList[0]);
        min.x = max.x = s.x;
        min.y = max.y = s.y;
        for (int i = 1; i < trackingList.Count; ++i) {
            s = cmain.WorldToScreenPoint(trackingList[i]);
            min.x = Mathf.Min(min.x, s.x);
            min.y = Mathf.Min(min.y, s.y);
            max.x = Mathf.Max(max.x, s.x);
            max.y = Mathf.Max(max.y, s.y);
        }

        //Debug.Log(min.ToString() + " " + max.ToString());

        float width = max.x - min.x;
        float height = max.y - min.y;
        if (width / height > cmain.aspect) { // if current aspect ration is > cam aspect, scale up height
            float halfDiff = (width / cmain.aspect - height) / 2.0f;
            min.y -= halfDiff;
            max.y += halfDiff;
        } else {    // if less than scale up width
            float halfDiff = (height * cmain.aspect - width) / 2.0f;
            min.x -= halfDiff;
            max.x += halfDiff;
        }

        float pad = 0.2f;   // todo make this public variable
        min.x -= cmain.pixelWidth * pad;
        min.y -= cmain.pixelHeight * pad;
        max.x += cmain.pixelWidth * pad;
        max.y += cmain.pixelHeight * pad;
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
