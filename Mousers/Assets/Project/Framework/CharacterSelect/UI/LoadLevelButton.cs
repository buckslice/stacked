using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadLevelButton : MonoBehaviour {
    MouserNetworking networkingScript;
	// Use this for initialization
	void Start () {
        networkingScript = GameObject.Find("DontDestroyOnLoadScripts").GetComponent<MouserNetworking>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(activate);
	}
	
	public void activate()
    {
        //TODO: change hardcoded level
        networkingScript.LoadLevel("Derek");
    }
}
