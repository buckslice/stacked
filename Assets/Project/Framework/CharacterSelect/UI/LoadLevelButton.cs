using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadLevelButton : MonoBehaviour {

    [SerializeField]
    protected string levelName = "Derek";

	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(activate);
	}
	
	public void activate()
    {
        R41DNetworking.Main.LoadLevel(levelName);
    }
}
