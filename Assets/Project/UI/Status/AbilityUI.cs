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

    IAbilityUI ability;
    GameObject spawnedUIPrefab;
    AbilityDisplay display;
    //AbstractAbilityDisplay abilityDisplay;

	void Start () {
        ability = GetComponent<IAbilityUI>();
        RectTransform parent = GetComponentInParent<EntityUIGroupHolder>().EntityGroup.StatusHolder;
        spawnedUIPrefab = Instantiate(uiPrefab, parent) as GameObject;
        spawnedUIPrefab.GetComponent<RectTransform>().Reset();
        display = spawnedUIPrefab.GetComponent<AbilityDisplay>();
	}

    void Update() {
        display.setAbilityReady(ability.Ready());
        display.setCooldownProgress(ability.cooldownProgress());
    }
}