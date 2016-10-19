using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class RandomRotation : MonoBehaviour {

    [SerializeField]
    protected float rotationMagnitude = 0.25f;

	void Start () {
        GetComponent<Rigidbody>().angularVelocity = Random.rotationUniform.eulerAngles.normalized * rotationMagnitude;
	}
}
