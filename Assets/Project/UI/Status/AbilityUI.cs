using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class responsible for the ability's UI
/// </summary>
[RequireComponent(typeof(IAbilityUI))]
public class AbilityUI : MonoBehaviour {

    [SerializeField]
    protected GameObject uiPrefab;

    [SerializeField]
    protected float vibrationDuration = 0.25f;

    [SerializeField]
    protected float vibrationStrength = 1;

    IAbilityUI ability;
    GameObject spawnedUIPrefab;
    AbilityDisplay display;
    ControllerPlayerInput controllerInput = null;

    float cooldownProgress;


	void Start () {
        ability = GetComponent<IAbilityUI>();
        RectTransform parent = GetComponentInParent<EntityUIGroupHolder>().EntityGroup.StatusHolder;
        spawnedUIPrefab = Instantiate(uiPrefab, parent) as GameObject;
        spawnedUIPrefab.GetComponent<RectTransform>().Reset();
        display = spawnedUIPrefab.GetComponent<AbilityDisplay>();

        PlayerInputHolder holder = GetComponentInParent<PlayerInputHolder>();
        if (holder.heldInput is ControllerPlayerInput) {
            controllerInput = (ControllerPlayerInput)holder.heldInput;
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