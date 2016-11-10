using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IDisabledAwake { void DisabledAwake(); }

public static class DisabledAwakeExtension {
    public static void Awake(this IDisabledAwake target) {
        if (!(target as MonoBehaviour).enabled || !(target as MonoBehaviour).gameObject.activeInHierarchy) {
            target.DisabledAwake();
        }
    }
}

public class DisabledAwake : MonoBehaviour {

    [SerializeField]
    protected MonoBehaviour[] targets;

    void Awake() {
        foreach (IDisabledAwake target in targets) {
            target.Awake();
        }
        this.enabled = false;
    }
}