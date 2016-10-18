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

    public void Update() {
        if (Player.Players.Count == 0) {
            SceneManager.LoadScene(gameDefeatPopupSceneName, LoadSceneMode.Additive);
            Destroy(this);
        } else if (Boss.Bosses.Count == 0) {
            SceneManager.LoadScene(gameVictoryPopupSceneName, LoadSceneMode.Additive);
            Destroy(this);
        }
    }
}