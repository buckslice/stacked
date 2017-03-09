using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour {

    public Transform segment1;
    public Transform segment2;

    Transform root;

    // Use this for initialization
    void Start () {
        root = transform.root;
	}
	
	// Update is called once per frame
	void Update () {
        segment2.rotation = Quaternion.LookRotation(root.forward);
	}
}
