using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneSelector : MonoBehaviour {
    [SerializeField]
    protected ReadyChecker readyChecker;

    private BossSceneHolder sceneHolder;

	// Use this for initialization
	void Start () {
        sceneHolder = GameObject.FindObjectOfType<BossSceneHolder>();
        if (sceneHolder!= null) {
            readyChecker.LevelToLoad = sceneHolder.bossToLoad;
        }
    }
}
