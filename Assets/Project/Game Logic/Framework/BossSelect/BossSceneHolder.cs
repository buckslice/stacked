using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneHolder : MonoBehaviour {
    [SerializeField]
    public string bossToLoad;

    private static BossSceneHolder main;
    public static BossSceneHolder Main { get { return main; } }
    void Awake() {
        if (main == null) {
            main = this;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        if (main != this) {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    protected void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
        if (arg0.name != bossToLoad) {
            return;
        }
        Destroy(this.gameObject);
    }
}
