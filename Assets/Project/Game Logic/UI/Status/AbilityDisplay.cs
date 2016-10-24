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

    public void Initialize(Sprite imageTex) {
        background.overrideSprite = imageTex;
    }

    public void setCooldownProgress(float progress) {
        mask.fillAmount = 1 - progress;
    }

    public void setAbilityReady(bool ready) {
        background.color = ready ? readyColor : notReadyColor;
    }
}
