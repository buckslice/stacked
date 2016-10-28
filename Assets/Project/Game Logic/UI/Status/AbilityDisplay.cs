using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IAbilityDisplay {
    void Initialize(IAbilityDisplayHolder abilityUI, Sprite imageTex);
    IAbilityDisplayHolder AbilityUI { get; }
    void setCooldownProgress(float progress);
    void setAbilityReady(bool ready);
}

public class AbilityDisplay : MonoBehaviour, IAbilityDisplay {

    [SerializeField]
    protected Image mask;

    [SerializeField]
    protected Image background;

    [SerializeField]
    protected Image greyscaleBackground;

    [SerializeField]
    protected Color readyColor = Color.yellow;

    [SerializeField]
    protected Color notReadyColor = Color.grey;

    IAbilityDisplayHolder abilityUI;
    public IAbilityDisplayHolder AbilityUI { get { return abilityUI; } }

    public void Initialize(IAbilityDisplayHolder abilityUI, Sprite imageTex) {
        this.abilityUI = abilityUI;
        if (imageTex != null) {
            background.overrideSprite = imageTex;
            greyscaleBackground.overrideSprite = imageTex;
        }
    }

    public void setCooldownProgress(float progress) {
        mask.fillAmount = 1 - progress;
    }

    public void setAbilityReady(bool ready) {
        background.color = ready ? readyColor : notReadyColor;
    }
}
