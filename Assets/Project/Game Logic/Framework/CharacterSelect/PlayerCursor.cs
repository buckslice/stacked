using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(IPlayerInputHolder))]
public class PlayerCursor : MonoBehaviour {

    [SerializeField]
    public int playerNumber = -1;
    public float moveSpeed = 4.0f;
    public Image leftHalf;
    public Image rightHalf;

    PointerEventData pointer = new PointerEventData(EventSystem.current);
    List<RaycastResult> results = new List<RaycastResult>();
    PlayerInputHolder input;
    public PlayerInputHolder Input { get { return input; } }
    
    bool selectionOne = false;
    bool selectionTwo = false;
    PlayerSetupNetworkedData.AbilityId selection1;
    PlayerSetupNetworkedData.AbilityId selection2;

    GameObject playerSetupGO = null;
    ReadyChecker readyChecker;
    public bool ready { get; set; }

    // Use this for initialization
    void Start () {
        input = GetComponent<PlayerInputHolder>();
        readyChecker = GameObject.Find("ReadyChecker").GetComponent<ReadyChecker>();
        readyChecker.AddPlayer(this);
        ready = false;
	}

    public void Initialize(int playerNumber) {
        this.playerNumber = playerNumber;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(input.movementDirection * moveSpeed * Screen.width / 800.0f);

        if (input.getSubmitDown) {
            pointer.position = new Vector3(transform.position.x, transform.position.y);
            results.Clear();

            EventSystem.current.RaycastAll(pointer, results);

            for(int i = 0; i < results.Count; ++i) {
                GameObject rgo = results[i].gameObject;
                if (rgo.CompareTag("AbilityIcon")) {
                    if (selectionOne) {
                        selection2 = rgo.GetComponent<CharacterSelectIcon>().ability;
                        rightHalf.color = rgo.GetComponent<Image>().color;
                        selectionTwo = true;
                    } else {
                        selection1 = rgo.GetComponent<CharacterSelectIcon>().ability;
                        leftHalf.color = rgo.GetComponent<Image>().color;
                        selectionOne = true;
                    }
                }
            }
        }
        if (input.getCancelDown) {
            if (playerSetupGO) {
                Destroy(playerSetupGO);
            }else if (selectionTwo) {
                selectionTwo = false;
                rightHalf.color = Color.white;
            } else if (selectionOne) {
                leftHalf.color = Color.white;
                selectionOne = false;
            }
            ready = false;
        }

        if (input.getStartDown && selectionOne && selectionTwo && playerSetupGO == null) {
            playerSetupGO = new GameObject();
            PlayerSetup playerSetup = playerSetupGO.AddComponent<PlayerSetup>();
            playerSetup.Initalize(input.HeldInput, playerNumber);
            PlayerSetup.PlayerSetupData pd = new PlayerSetup.PlayerSetupData();
            pd.firstAbilities = new PlayerSetupNetworkedData.AbilityId[] { selection1 };
            pd.secondAbilities = new PlayerSetupNetworkedData.AbilityId[] { selection2 };
            playerSetup.playerData = pd;
            ready = true;
        }

	}
}
