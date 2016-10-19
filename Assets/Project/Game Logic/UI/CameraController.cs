using UnityEngine;
using System.Collections.Generic;

using System.Linq;

public class CameraController : MonoBehaviour {

    public Transform boss { get; set; }

    private Camera mainCam;
    private Transform camTransform;

    private readonly Vector3 padding = Vector3.one * 4.0f;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
        camTransform = mainCam.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.Players.Count == 0) {
            return;
        }

        // done like this to avoid including the origin
        Player p = Player.Players.First();
        Bounds bounds = new Bounds(p.Holder.transform.position, padding);

        foreach(Player player in Player.Players) {
            bounds.Encapsulate(new Bounds(player.Holder.transform.position, padding));
        }

        if (boss != null) {
            bounds.Encapsulate(new Bounds(boss.position, padding));
        }

        // find XZ distance from camera to bounds center
        Vector3 xzCam = camTransform.position;
        xzCam.y = 0.0f;
        Vector3 xzBounds = bounds.center;
        xzBounds.y = 0.0f;
        float xzDist = Vector3.Distance(xzCam, xzBounds);

        // get normalized direction to center of all players bounds
        Vector3 dir = camTransform.position - bounds.center;
        dir.y = 0.0f;
        if(dir.sqrMagnitude > 1.0f) {
            dir.Normalize();
        }

        float maxBoundsDim = Mathf.Max(bounds.size.x, Mathf.Max(bounds.size.y, bounds.size.z));
        float camFollowDist = maxBoundsDim;

        // set target to follow center without getting too close and then look at center
        Vector3 targetPos = camTransform.position + dir * (camFollowDist - xzDist);
        camTransform.position = Vector3.Lerp(camTransform.position, targetPos, Time.deltaTime * 0.5f);

        camTransform.LookAt(bounds.center);

	}
}
