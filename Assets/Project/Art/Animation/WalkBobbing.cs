using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class WalkBobbing : MonoBehaviour {

    [SerializeField]
    protected float distancePerCycle = 5;

    [SerializeField]
    protected AnimationCurve height;

    Transform target;
    float animationProgress = 0;
    Vector3 previousPosition;
    

	void Start () {
        target = transform.GetChild(0);
        previousPosition = transform.position;
	}
	
	void Update () {
        animationProgress += (transform.position - previousPosition).magnitude / distancePerCycle;
        animationProgress %= 1;
        previousPosition = transform.position;

        target.localPosition = new Vector3(0, height.Evaluate(animationProgress));
	}
}
