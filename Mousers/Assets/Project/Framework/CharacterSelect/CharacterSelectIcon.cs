using UnityEngine;
using System.Collections;

public class CharacterSelectIcon : MonoBehaviour {
    [SerializeField]
    protected GameObject playerSetup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public GameObject getPlayerSetup()
    {
        return playerSetup;
    }
}
