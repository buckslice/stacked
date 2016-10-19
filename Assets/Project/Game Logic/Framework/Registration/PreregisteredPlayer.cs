using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A player pre-registered by the action of pressing start on the title screen.
/// </summary>
public class PreregisteredPlayer : MonoBehaviour {

	private IPlayerInput input;
    public IPlayerInput inputBindings { get { return input; } set { input = value; } }

    protected virtual void Awake() {
        DontDestroyOnLoad(this.transform.root.gameObject);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
        if (arg0.name == Tags.Scenes.PlayerRegistration) {
            PlayerRegistration.Main.PreregisterPlayer(input);
            Destroy(this);
        }
    }
}
