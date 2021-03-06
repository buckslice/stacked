﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// If an instance of this class exists, event logging will be enabled.
/// </summary>
public class EventLog : MonoBehaviour {

    [SerializeField]
    protected GameObject debugSphere;

    static EventLog main;

	void Awake () {
        main = this;
	}

    void OnDestroy() {
        if (main == this) { main = null; }
    }

    public static void Log(Object context, string format, params object[] args) {
        if (main == null) { return; }
        if (!main.enabled) { return; }
        Debug.LogFormat(context, format, args);
    }

    public static void LogPosition(Vector3 position) {
        SimplePool.Spawn(main.debugSphere, position);
    }

    void Update() { } //required for the enable/disable checkbox in the inspector
}
