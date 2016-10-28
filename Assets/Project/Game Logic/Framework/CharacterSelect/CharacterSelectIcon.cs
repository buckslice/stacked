using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectIcon : MonoBehaviour {
    //[SerializeField]
    //protected GameObject playerSetup;
    //public GameObject getPlayerSetup()
    //{
    //    return playerSetup;
    //}

    public PlayerSetupNetworkedData.AbilityId ability;

    public Sprite abilityIcon { get { return GetComponent<Image>().sprite; } }
}
