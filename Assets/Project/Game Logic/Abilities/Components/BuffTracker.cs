using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script to track buffs.
/// </summary>
public class BuffTracker : MonoBehaviour, IEnumerable<Collider> {
    HashSet<Collider> appliedTargets = new HashSet<Collider>();

    public delegate void TargetAdded(Collider added);
    public delegate void TargetRemoved(Collider removed);

    public event TargetRemoved onTargetAdded = delegate { };
    public event TargetRemoved onTargetRemoved = delegate { };

    public virtual bool AddTarget(Collider col) {
        bool added = appliedTargets.Add(col);
        if (added) {
            onTargetAdded(col);
        }
        return added;
    }

    public virtual bool RemoveTarget(Collider col) {
        bool removed = appliedTargets.Remove(col);
        if (removed) {
            onTargetRemoved(col);
        }
        return removed;
    }

    public void Clear() {
        foreach (Collider col in appliedTargets) {
            onTargetRemoved(col);
        }
        appliedTargets.Clear();
    }

    public void UpdateTargets(HashSet<Collider> newTargets) {
        HashSet<Collider> duplicateAppliedTargets = new HashSet<Collider>(appliedTargets); //avoid modification during iteration
        foreach (Collider col in duplicateAppliedTargets) {
            if (!newTargets.Contains(col)) {
                RemoveTarget(col);
            }
        }

        foreach (Collider col in newTargets) {
            AddTarget(col);
        }
    }

    public IEnumerator<Collider> GetEnumerator() {
        return appliedTargets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}
