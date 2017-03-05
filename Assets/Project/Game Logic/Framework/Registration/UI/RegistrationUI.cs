using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IEntityUIGroupHolder))]
public class RegistrationUI : MonoBehaviour {

    [SerializeField]
    protected GameObject registrationUIPrefab;

    protected RegistrationUIBar instantiatedRegistrationBar;

    public bool ready { set { instantiatedRegistrationBar.readyText.enabled = value; } }

    protected virtual void Start() {
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        GameObject registrationBar = (GameObject)Instantiate(registrationUIPrefab, GetComponent<IEntityUIGroupHolder>().EntityGroup.HealthBarHolder);
        (registrationBar.transform as RectTransform).Reset();
        instantiatedRegistrationBar = registrationBar.GetComponent<RegistrationUIBar>();
        int playerID = GetComponent<IPlayerID>().PlayerID;
        instantiatedRegistrationBar.PlayerID = playerID;
    }

    void OnDestroy() {
        if (instantiatedRegistrationBar != null) {
            Destroy(instantiatedRegistrationBar.gameObject);
        }
    }
}
