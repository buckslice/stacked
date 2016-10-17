using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Joint))]
public class ConfigureSpring : MonoBehaviour {

	void Start () {
        Rigidbody connect = transform.parent.GetComponentInParent<Rigidbody>();
        foreach (Joint joint in GetComponents<Joint>()) {
            joint.connectedBody = connect;
        }
        Destroy(this);
	}
}
