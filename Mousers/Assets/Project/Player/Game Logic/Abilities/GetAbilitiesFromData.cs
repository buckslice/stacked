using UnityEngine;
using System.Collections;
using System.IO;

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
        print(abilityData.abilities);
        GameObject[] result = new GameObject[abilityData.abilities.Count];
        return result;
    }
}
