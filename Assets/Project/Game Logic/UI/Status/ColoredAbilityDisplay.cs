using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IAbilityDisplay {
    void Initialize(IAbilityDisplayHolder abilityUI);
    IAbilityDisplayHolder AbilityUI { get; }
    void setCooldownProgress(float progress);
    void setAbilityReady(bool ready);
}

public class ColoredAbilityDisplay : MonoBehaviour, IAbilityDisplay {

    [SerializeField]
    protected Image mask;

    [SerializeField]
    protected Image[] tintedImages;

    [SerializeField]
    protected Color readyColor = Color.yellow;

    [SerializeField]
    protected Color notReadyColor = Color.grey;

    IAbilityDisplayHolder abilityUI;
    public IAbilityDisplayHolder AbilityUI { get { return abilityUI; } }

    public void Initialize(IAbilityDisplayHolder abilityUI) {
        this.abilityUI = abilityUI;
    }

    /// <summary>
    /// TODO: maybe move this to a seprate script.
    /// </summary>
    /// <param name="progress"></param>
    public void setCooldownProgress(float progress) {
        mask.fillAmount = 1 - progress;
    }

    public void setAbilityReady(bool ready) {
        foreach (Image tintedImage in tintedImages) {
            tintedImage.color = ready ? readyColor : notReadyColor;
        }
    }
}
