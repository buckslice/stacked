using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class GetAbilitiesFromData : MonoBehaviour {
    [SerializeField]
    protected TextAsset abilityText;

	// Use this for initialization
	void Start () {
	}
	
    public GameObject[] getAbilities() {
        AbilitiesList abilitiesList = JsonUtility.FromJson<AbilitiesList>(abilityText.text);
        GameObject[] result = new GameObject[abilitiesList.abilities.Length];
        for (int i = 0; i < abilitiesList.abilities.Length; i++) {
            GameObject ability = new GameObject();
            ability.transform.SetParent(transform.root);
            AbilityFactory.createAbility(ability, abilitiesList.abilities[i]);
            result[i] = ability;
        }
        return result;
    }
}
