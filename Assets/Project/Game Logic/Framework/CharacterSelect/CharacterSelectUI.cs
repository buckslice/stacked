using UnityEngine;
using System.Collections;

[RequireComponent(typeof(IPlayerInputHolder))]
public class CharacterSelectUI : RegistrationUI {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        CharacterSelectUIBar characterSelectBar = instantiatedRegistrationBar.GetComponent<CharacterSelectUIBar>();

        IPlayerInputHolder input = GetComponent<IPlayerInputHolder>();

        characterSelectBar.setTooltip(input.submitName);
    }
}
