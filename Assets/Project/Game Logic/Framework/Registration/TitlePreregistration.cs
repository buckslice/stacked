using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TitleScreen))]
public class TitlePreregistration : MonoBehaviour {

    [SerializeField]
    protected GameObject preregistrationPrefab;

    [SerializeField]
    protected PlayerInputHolder[] possibleBindings;
	
	void Update () {
        for (int i = 0; i < possibleBindings.Length; i++) {
            if (possibleBindings[i].AnyKey()) {
                GameObject instantiatedPreregistration = Instantiate(preregistrationPrefab);
                instantiatedPreregistration.GetComponent<PreregisteredPlayer>().inputBindings = possibleBindings[i].HeldInput;
                GetComponent<TitleScreen>().GoToMainMenu();
                return;
            }
        }
	}
}
