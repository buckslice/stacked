using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectIcon : MonoBehaviour, ISelectable {
    //[SerializeField]
    //protected GameObject playerSetup;
    //public GameObject getPlayerSetup()
    //{
    //    return playerSetup;
    //}

    public PlayerSetupNetworkedData.AbilityId ability;

    public Sprite abilityIcon { get { return GetComponent<Image>().sprite; } }

    public GameObject visualsIcon;

    public Color color = Color.white;
}
