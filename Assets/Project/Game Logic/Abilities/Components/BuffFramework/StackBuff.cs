using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Applies a buff/debuff to targets in the current stack.
/// </summary>
public class StackBuff : MonoBehaviour, IDisabledStart {

    BuffTracker tracker;
    Stackable stackable;

    void Awake() {
        tracker = GetComponent<BuffTracker>();
    }

    void IDisabledStart.DisabledStart() {
        stackable = GetComponentInParent<Stackable>();
        stackable.changeEvent += stackable_changeEvent;
    }

    void stackable_changeEvent() {
        if (!enabled) { return; }
        HashSet<Collider> newSet = new HashSet<Collider>();
        foreach (Stackable stackElement in stackable) {
            foreach (Collider col in stackElement.GetComponentsInChildren<Collider>()) {
                newSet.Add(col);
            }
        }

        //tracker can be null if the scene is ending, which throws a null pointer exception. Doesn't particularly matter.
        tracker.UpdateTargets(newSet);
    }

    void OnEnable() {
        foreach (Stackable stackElement in stackable) {
            foreach (Collider col in stackElement.GetComponentsInChildren<Collider>()) {
                tracker.AddTarget(col);
            }
        }
    }

    void OnDisable() {
        tracker.Clear();
    }
}
