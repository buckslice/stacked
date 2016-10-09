using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class GetAbilitiesFromData : MonoBehaviour {
    [SerializeField]
    protected TextAsset abilityText;

    [SerializeField]
    protected GameObject abstractAbilityPrefab;

	// Use this for initialization
	void Start () {
        getAbilities();
	}
	
    public GameObject[] getAbilities() {
        AbilityData abilityData = JsonUtility.FromJson<AbilityData>(abilityText.text);
        print(JsonUtility.ToJson(abilityData));
        GameObject[] result = new GameObject[0/*abilityData.behaviors.Length*/];
        return result;
    }
}
