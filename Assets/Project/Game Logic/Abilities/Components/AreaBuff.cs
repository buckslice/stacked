using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Applies a buff/debuff to targets in a shape
/// </summary>
[RequireComponent(typeof(IShape))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(BuffTracker))]
public class AreaBuff : MonoBehaviour {

    [SerializeField]
    protected LayerMask layermask;

    IShape shape;
    BuffTracker tracker;

    void Awake() {
        shape = GetComponent<IShape>();
        tracker = GetComponent<BuffTracker>();
    }

    void OnEnable() {
        foreach (Collider target in shape.Cast(layermask)) {
            tracker.AddTarget(target);
        }
    }

    void OnTriggerEnter(Collider target) {
        if (!enabled) { return; }

        if (((1 << target.gameObject.layer) & layermask) == 0) {
            //not on layermask
            return;
        }

        tracker.AddTarget(target);
    }

    void OnDisable() {
        tracker.Clear();
    }

    void OnTriggerExit(Collider target) {

        if (!enabled) { return; }

        tracker.RemoveTarget(target);
    }
}