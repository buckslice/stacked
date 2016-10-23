using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class StaticBobbing : MonoBehaviour {
    [SerializeField]
    protected float timePerCycle = 5;

    [SerializeField]
    protected AnimationCurve height;

    Transform target;

    float timeOffset;

    void Start() {
        target = transform.GetChild(0);
        timeOffset = Random.value * timePerCycle;
    }

    void Update() {
        float animationProgress = (Time.time + timeOffset) / timePerCycle;
        animationProgress %= 1;
        target.localPosition = new Vector3(0, height.Evaluate(animationProgress));
    }
}
