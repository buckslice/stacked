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

    IAbilityStatus ability;
    GameObject spawnedUIPrefab;
    IAbilityDisplay[] displays;


	void Start () {
        ability = GetComponent<IAbilityStatus>();
        RectTransform parent = GetComponentInParent<IEntityUIGroupHolder>().EntityGroup.StatusHolder;
        spawnedUIPrefab = Instantiate(uiPrefab, parent) as GameObject;
        spawnedUIPrefab.GetComponent<RectTransform>().Reset();
        displays = spawnedUIPrefab.GetComponentsInChildren<IAbilityDisplay>();
        foreach (IAbilityDisplay display in displays) {
            display.Initialize(this);
        }
	}

    void Update() {
        foreach (IAbilityDisplay display in displays) {
            display.setAbilityReady(ability.Ready());
        }

        float cooldownProgress = ability.cooldownProgress();
        foreach (IAbilityDisplay display in displays) {
            display.setCooldownProgress(cooldownProgress);
        }
    }

    void OnDestroy() {
        if(spawnedUIPrefab != null) {
            Destroy(spawnedUIPrefab);
        }
    }
}