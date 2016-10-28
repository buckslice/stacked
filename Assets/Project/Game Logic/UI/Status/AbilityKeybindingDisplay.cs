using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IAbilityDisplay))]
public class AbilityKeybindingDisplay : MonoBehaviour {

    [SerializeField]
    protected Text text;

	void Start () {
        IAbilityDisplay display = GetComponent<IAbilityDisplay>();

        InputTrigger inputTrigger = ((MonoBehaviour)display.AbilityUI).GetComponent<InputTrigger>();
        if (inputTrigger == null) {
            return;
        }

        switch (inputTrigger.Binding) {
            case AbilityKeybinding.ABILITY1:
            case AbilityKeybinding.ABILITY1DOWN:
            case AbilityKeybinding.ABILITY1UP:
                text.text = inputTrigger.PlayerInput.ability1Name;
                break;
            case AbilityKeybinding.ABILITY2:
            case AbilityKeybinding.ABILITY2DOWN:
            case AbilityKeybinding.ABILITY2UP:
                text.text = inputTrigger.PlayerInput.ability2Name;
                break;
            case AbilityKeybinding.BASICATTACK:
            case AbilityKeybinding.BASICATTACKDOWN:
            case AbilityKeybinding.BASICATTACKUP:
                text.text = inputTrigger.PlayerInput.basicAttackName;
                break;
            case AbilityKeybinding.CANCEL:
            case AbilityKeybinding.CANCELDOWN:
                text.text = inputTrigger.PlayerInput.cancelName;
                break;
            case AbilityKeybinding.SUBMIT:
            case AbilityKeybinding.SUBMITDOWN:
                text.text = inputTrigger.PlayerInput.submitName;
                break;
        }
	}
}
