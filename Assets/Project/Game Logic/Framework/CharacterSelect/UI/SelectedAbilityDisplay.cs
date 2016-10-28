using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class SelectedAbilityDisplay : MonoBehaviour {

    [SerializeField]
    protected Text bindingLabel;

    [SerializeField]
    protected Image icon;

    public void Initialize(string bindingText, Sprite imageTex) {
        this.bindingLabel.text = bindingText;
        if (imageTex != null) {
            icon.overrideSprite = imageTex;
        }
    }
}
