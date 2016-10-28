using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class which holds an AbilityDisplay
/// </summary>
public interface IAbilityDisplayHolder {
}

/// <summary>
/// Class responsible for the ability's UI
/// </summary>
[RequireComponent(typeof(IAbilityStatus))]
public class AbilityUI : MonoBehaviour, IAbilityDisplayHolder {

    [SerializeField]
    protected GameObject uiPrefab;

    /// <summary>
    /// Icon can be null.
    /// </summary>
    [SerializeField]
    protected Sprite uiIcon;

    [SerializeField]
    protected float vibrationDuration = 0.25f;

    [SerializeField]
    protected float vibrationStrength = 1;

    IAbilityStatus ability;
    GameObject spawnedUIPrefab;
    IAbilityDisplay display;
    ControllerPlayerInput controllerInput = null;

    float cooldownProgress;


	void Start () {
        ability = GetComponent<IAbilityStatus>();
        RectTransform parent = GetComponentInParent<EntityUIGroupHolder>().EntityGroup.StatusHolder;
        spawnedUIPrefab = Instantiate(uiPrefab, parent) as GameObject;
        spawnedUIPrefab.GetComponent<RectTransform>().Reset();
        display = spawnedUIPrefab.GetComponent<IAbilityDisplay>();
        display.Initialize(this, uiIcon);

        PlayerInputHolder holder = GetComponentInParent<PlayerInputHolder>();
        if (holder.HeldInput is ControllerPlayerInput) {
            controllerInput = (ControllerPlayerInput)holder.HeldInput;
        }
	}

    void Update() {
        display.setAbilityReady(ability.Ready());

        float newCooldownProgress = ability.cooldownProgress();
        display.setCooldownProgress(newCooldownProgress);
        trackVibration(cooldownProgress, newCooldownProgress);
        cooldownProgress = newCooldownProgress;
    }

    void OnDestroy() {
        if(spawnedUIPrefab != null) {
            Destroy(spawnedUIPrefab);
        }
    }

    void trackVibration(float oldEdge, float newEdge) {
        if (controllerInput == null) {
            return;
        }

        if (oldEdge > 0 && newEdge == 0) {
            controllerInput.Vibrate(vibrationStrength, vibrationDuration, this);
        }
    }
}