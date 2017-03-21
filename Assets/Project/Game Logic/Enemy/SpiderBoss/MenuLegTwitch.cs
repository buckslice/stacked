using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// randomly twitch legs (used in start menu)
public class MenuLegTwitch : MonoBehaviour {


    float offset;
	// Use this for initialization
	void Start () {
        offset = Random.value * 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = Quaternion.Euler(Mathf.PerlinNoise(Time.time*3.0f+offset, 100.0f) * 60.0f, 0.0f, 0.0f);
	}
}
