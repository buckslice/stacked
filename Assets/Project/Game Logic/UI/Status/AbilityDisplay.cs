using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class AbilityDisplay : MonoBehaviour {

    [SerializeField]
    protected Image mask;

    [SerializeField]
    protected Image background;

    [SerializeField]
    protected Color readyColor = Color.yellow;

    [SerializeField]
    protected Color notReadyColor = Color.grey;

    AbilityUI abilityUI;
    public AbilityUI AbilityUI { get { return abilityUI; } }

    public void Initialize(AbilityUI abilityUI, Sprite imageTex) {
        this.abilityUI = abilityUI;
        if (imageTex != null) {
            background.overrideSprite = imageTex;
        }
    }

    public void setCooldownProgress(float progress) {
        mask.fillAmount = 1 - progress;
    }

    public void setAbilityReady(bool ready) {
        background.color = ready ? readyColor : notReadyColor;
    }
}
