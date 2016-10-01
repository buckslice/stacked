using UnityEngine;
using System.Collections;

public class DerpAbility : MonoBehaviour {
    public string buttonName = "Fire1";
    private CameraShakeScript cameraShakeScript;

	// Use this for initialization
	void Start () {
        cameraShakeScript = Camera.main.GetComponent<CameraShakeScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(buttonName)){
            cameraShakeScript.screenShake(.5f);
        }
	}
}
