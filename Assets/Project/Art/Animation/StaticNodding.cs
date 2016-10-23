using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class StaticNodding : MonoBehaviour {

    [SerializeField]
    protected float timePerCycle = 5;

    [SerializeField]
    protected AnimationCurve rotation;

    Transform target;

    float timeOffset;

    void Start() {
        target = transform.GetChild(0);
        timeOffset = Random.value * timePerCycle;
    }

    void Update() {
        float animationProgress = (Time.time + timeOffset) / timePerCycle;
        animationProgress %= 1;
        target.localRotation = Quaternion.AngleAxis(45 * rotation.Evaluate(animationProgress), Vector3.right);
    }
}
