using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedObjectActiveConstraint : UntargetedAbilityConstraint {
    [SerializeField]
    protected GameObject[] objectsTracked;

    public override void Activate() { }

    public override bool isAbilityActivatible() {
        foreach (GameObject trackedObject in objectsTracked) {
            if (trackedObject == null || !trackedObject.activeSelf) {
                return false;
            }
        }
        return true;
    }
}
