using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IDisabledStart { void DisabledStart(); }

public static class DisabledStartExtension {
    public static void Start(this IDisabledStart target) {
        if (!(target as MonoBehaviour).enabled || !(target as MonoBehaviour).gameObject.activeInHierarchy) {
            target.DisabledStart();
        }
    }
}

public class DisabledStart : MonoBehaviour {

    [SerializeField]
    protected MonoBehaviour[] targets;

    void Start() {
        foreach (IDisabledStart target in targets) {
            target.Start();
        }
        this.enabled = false;
    }
}