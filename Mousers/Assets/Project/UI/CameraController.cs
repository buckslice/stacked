using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

    private Camera mainCam;

    private List<Transform> playerLocs = new List<Transform>();

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;

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

        Bounds bounds = new Bounds();
        for (int i = 0; i < playerLocs.Count; ++i){
            Bounds b = new Bounds(playerLocs[i].position, new Vector3(5, 5, 5));
            bounds.Encapsulate(b);
        }
        mainCam.transform.parent.LookAt(bounds.center);

        Vector3 pos = mainCam.transform.position;
        Debug.DrawRay(pos, bounds.center - pos, Color.green, 5.0f);
        
	}
}
