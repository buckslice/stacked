using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class RandomRotation : MonoBehaviour {

    [SerializeField]
    protected float rotationMagnitude = 0.25f;

    Rigidbody rigid;

    Vector2 angularVelocity;
    Vector2 angularVelocityNormal;

	void Start () {
        rigid = GetComponent<Rigidbody>();
        angularVelocity = rigid.angularVelocity = Random.insideUnitCircle.normalized * rotationMagnitude;
        angularVelocityNormal = Vector3.Cross(angularVelocity, Vector3.forward);
	}

    void Update() {
        rigid.angularVelocity = angularVelocity + Mathf.Sin(Time.time / 10) * angularVelocityNormal;
    }
}
