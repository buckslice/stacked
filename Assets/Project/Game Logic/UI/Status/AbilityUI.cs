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

    [SerializeField]
    protected float vibrationDuration = 0.25f;

    [SerializeField]
    protected float vibrationStrength = 1;

    IAbilityStatus ability;
    GameObject spawnedUIPrefab;
    IAbilityDisplay[] displays;
    ControllerPlayerInput controllerInput = null;

    float cooldownProgress;


	void Start () {
        ability = GetComponent<IAbilityStatus>();
        RectTransform parent = GetComponentInParent<EntityUIGroupHolder>().EntityGroup.StatusHolder;
        spawnedUIPrefab = Instantiate(uiPrefab, parent) as GameObject;
        spawnedUIPrefab.GetComponent<RectTransform>().Reset();
        displays = spawnedUIPrefab.GetComponentsInChildren<IAbilityDisplay>();
        foreach (IAbilityDisplay display in displays) {
            display.Initialize(this);
        }

        PlayerInputHolder holder = GetComponentInParent<PlayerInputHolder>();
        if (holder.HeldInput is ControllerPlayerInput) {
            controllerInput = (ControllerPlayerInput)holder.HeldInput;
        }
	}

    void Update() {
        foreach (IAbilityDisplay display in displays) {
            display.setAbilityReady(ability.Ready());
        }

        float newCooldownProgress = ability.cooldownProgress();
        foreach (IAbilityDisplay display in displays) {
            display.setCooldownProgress(newCooldownProgress);
        }
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