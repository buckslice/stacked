﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CheckGameEnd : MonoBehaviour {

    [SerializeField]
    protected string gameDefeatPopupSceneName = Tags.Scenes.DefeatPopup;

    [SerializeField]
    protected string gameVictoryPopupSceneName = Tags.Scenes.VictoryPopup;

    [SerializeField]
    protected GameObject[] trackedObjects;

    public void Update() {
        // for testing
        //if (Input.GetKeyDown(KeyCode.U)) {
        //    SceneManager.LoadScene(gameVictoryPopupSceneName, LoadSceneMode.Additive);
        //    Destroy(this);
        //}


        if (Player.AllPlayersDead()) {
            SceneManager.LoadScene(gameDefeatPopupSceneName, LoadSceneMode.Additive);
            Destroy(this);
        } else if ((trackedObjects==null || trackedObjects.Length==0) && Boss.Bosses.Count == 0) {
            SceneManager.LoadScene(gameVictoryPopupSceneName, LoadSceneMode.Additive);
            Destroy(this);
        }
        else if (trackedObjects != null && trackedObjects.Length>0) {
            foreach (GameObject trackedObject in trackedObjects){
                if (trackedObject!=null && trackedObject.activeSelf) {
                    return;
                }
            }
            SceneManager.LoadScene(gameVictoryPopupSceneName, LoadSceneMode.Additive);
            Destroy(this);
        }
    }
}
