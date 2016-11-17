using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class StackableJoint : MonoBehaviour {

    Rigidbody target;
    public Rigidbody Target { get { return target; } 
        set {
            RemoveTarget(); AddTarget(value); 
        } }

    void AddTarget(Rigidbody target) {
        if (target) {
            target.isKinematic = true;
            target.MovePosition(transform.position);
        }
        this.target = target;
    }

    void RemoveTarget() {
        if (target) {
            target.isKinematic = false;
        }
        this.target = null;
    }

	void LateUpdate () {
        if (target != null) {
            target.MovePosition(this.transform.position);
        }
	}
}
