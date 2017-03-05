using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelButton : MonoBehaviour {

    [SerializeField]
    protected string levelName = "Derek";

    void FindAndStopAutoActivator() {
        ActivateButtonAfterTime activator = FindObjectOfType<ActivateButtonAfterTime>();
        activator.gameObject.SetActive(false);
    }
	
	public void activate() {
        FindAndStopAutoActivator();
        Time.timeScale = 1.0f;
        R41DNetworking.Main.LoadLevel(levelName);
    }

    public void reloadLevel() {
        FindAndStopAutoActivator();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
