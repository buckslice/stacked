using UnityEngine;
using System.Collections;

public class ButtonCheckUI : MonoBehaviour {

    [SerializeField]
    protected GameObject buttonCheckPrefab;

    // Use this for initialization
    void Start () {
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        GameObject buttonCheckMenu = (GameObject)Instantiate(buttonCheckPrefab, GetComponent<EntityUIGroupHolder>().EntityGroup.HealthBarHolder);
        (buttonCheckMenu.transform as RectTransform).Reset();
        //instantiatedRegistrationBar = registrationBar.GetComponent<RegistrationUIBar>();
        //int playerID = GetComponent<RegisteredPlayer>().PlayerID;
        //instantiatedRegistrationBar.PlayerID = playerID;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
