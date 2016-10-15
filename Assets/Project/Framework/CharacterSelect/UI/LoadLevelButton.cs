using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelButton : MonoBehaviour {

    [SerializeField]
    protected string levelName = "Derek";

	// Use this for initialization
	void Start () {
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName != Tags.Scenes.PlayerRegistration && sceneName != Tags.Scenes.CharacterSelect) {
            Destroy(gameObject);
            return;
        }
        Button button = GetComponent<Button>();
        button.onClick.AddListener(activate);
	}
	
	public void activate()
    {
        R41DNetworking.Main.LoadLevel(levelName);
    }
}
