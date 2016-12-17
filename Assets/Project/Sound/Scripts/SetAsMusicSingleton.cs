using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Used to create or change the current MusicSingleton outside of scripting calls.
/// </summary>
public class SetAsMusicSingleton : MusicSingleton {

    /// <summary>
    /// If true, replace the current MusicSingleton. If false, replace only if there is no current MusicSingleton.
    /// </summary>
    [SerializeField]
    protected bool overrideSingleton = false;

	// Use this for initialization
	void Awake () {
        Assert.IsTrue(this != MusicSingleton.Main);
		if(overrideSingleton) {
            MusicSingleton.ChangeMain(this);
        } else {
            MusicSingleton.SetMain(this);
        }
	}
}
