using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(EntityUIGroupHolder))]
public class RegistrationUI : MonoBehaviour {

    [SerializeField]
    protected GameObject registrationUIPrefab;

    RegistrationUIBar instantiatedRegistrationBar;

	void Start () {
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        GameObject registrationBar = (GameObject)Instantiate(registrationUIPrefab, GetComponent<EntityUIGroupHolder>().EntityGroup.HealthBarHolder);
        (registrationBar.transform as RectTransform).Reset();
        instantiatedRegistrationBar = registrationBar.GetComponent<RegistrationUIBar>();
        int playerID = GetComponent<IPlayerID>().PlayerID;
        instantiatedRegistrationBar.PlayerID = playerID;
	}
}
