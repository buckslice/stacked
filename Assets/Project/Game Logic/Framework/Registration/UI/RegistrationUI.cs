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

        CanvasHelper ch = canvasRoot.GetComponent<CanvasHelper>();

        GameObject registrationBar = (GameObject)Instantiate(registrationUIPrefab, GetComponent<EntityUIGroupHolder>().EntityGroup.GetComponent<RectTransform>());
        (registrationBar.transform as RectTransform).Reset();
        instantiatedRegistrationBar = registrationBar.GetComponent<RegistrationUIBar>();
        Player player = (Player)GetComponent<IDamageHolder>().GetRootDamageTracker();
        instantiatedRegistrationBar.PlayerID = player.PlayerID;
	}
}
