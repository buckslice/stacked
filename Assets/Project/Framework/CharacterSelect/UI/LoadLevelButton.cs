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
#if UNITY_EDITOR
        if(sceneName != Tags.Scenes.PlayerRegistration && sceneName != Tags.Scenes.CharacterSelect && Time.timeSinceLevelLoad < 10) {
            Debug.LogError("the player shouldn't be able to change scenes right now");
            Destroy(this);
            return;
        }
#endif
        Button button = GetComponent<Button>();
        button.onClick.AddListener(activate);
	}
	
	public void activate()
    {
        R41DNetworking.Main.LoadLevel(levelName);
    }
}
