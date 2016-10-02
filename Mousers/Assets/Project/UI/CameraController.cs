using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

    private Camera mainCam;
    private Transform camform;

    private List<Transform> playerLocs = new List<Transform>();

    private readonly Vector3 padding = Vector3.one * 5.0f;

    // TODO make this change based on size of players bounds
    // that way if players are far apart camera will zoom out more to see them all
    private const float camFollowDist = 30.0f;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
        camform = mainCam.transform.parent;

        // find all players transforms
        GameObject[] playerGos = GameObject.FindGameObjectsWithTag(Tags.Player);
        for(int i = 0; i < playerGos.Length; ++i) {
            playerLocs.Add(playerGos[i].transform);
        }

	}
	
	// Update is called once per frame
	void Update () {
        if(playerLocs.Count == 0) {
            return;
        }

        // done like this to avoid including the origin
        Bounds bounds = new Bounds(playerLocs[0].position, padding);
        for (int i = 1; i < playerLocs.Count; ++i){
            bounds.Encapsulate(new Bounds(playerLocs[i].position, padding));
        }

        // find XZ distance from camera to bounds center
        Vector3 xzCam = camform.position;
        xzCam.y = 0.0f;
        Vector3 xzBounds = bounds.center;
        xzBounds.y = 0.0f;
        float xzDist = Vector3.Distance(xzCam, xzBounds);

        // get normalized direction to center of all players bounds
        Vector3 dir = camform.position - bounds.center;
        dir.y = 0.0f;
        if(dir.sqrMagnitude > 1.0f) {
            dir.Normalize();
        }

        // set target to follow center without getting too close and then look at center
        Vector3 targetPos = camform.position + dir * (camFollowDist - xzDist);
        camform.position = Vector3.Lerp(camform.position, targetPos, Time.deltaTime * 0.5f);

        camform.LookAt(bounds.center);

	}
}
