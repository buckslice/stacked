using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CheckGameEnd : MonoBehaviour {

    [SerializeField]
    protected string gameEndPopupSceneName = Tags.Scenes.DefeatPopup;

    public void Update() {
        if (Player.Players.Count == 0) {
            SceneManager.LoadScene(gameEndPopupSceneName, LoadSceneMode.Additive);
            Destroy(this);
        }
    }
}
