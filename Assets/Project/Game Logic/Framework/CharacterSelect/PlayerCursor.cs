using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PlayerInputHolder))]
public class PlayerCursor : MonoBehaviour, ISelection {

    [SerializeField]
    public int playerNumber = -1;
    public float moveSpeed = 4.0f;
    public Image leftHalf;
    public Image rightHalf;

    PointerEventData pointer = new PointerEventData(EventSystem.current);
    List<RaycastResult> results = new List<RaycastResult>();
    PlayerInputHolder input;
    public IPlayerInputHolder Input { get { return input; } }
    
    bool selected1 = false;
    bool selected2 = false;
    PlayerSetupNetworkedData.AbilityId selection1;
    PlayerSetupNetworkedData.AbilityId selection2;

    GameObject playerSetupGO = null;
    ReadyChecker readyChecker;
    public bool Ready { get; set; }

    // Use this for initialization
    void Start () {
        input = GetComponent<PlayerInputHolder>();
        readyChecker = GameObject.Find("ReadyChecker").GetComponent<ReadyChecker>();
        readyChecker.AddPlayer(this);

        Assert.IsNotNull(BossSetup.Main);
        readyChecker.LevelToLoad = BossSetup.Main.BossData.sceneName;

        Ready = false;
	}

    public void Initialize(int playerNumber) {
        this.playerNumber = playerNumber;
    }

    //TODO: migrate to use ability system for selection and deselection
    public bool CanSelect() { return false; }
    public bool Select(ISelectable data) { return false; }

    public bool CanDeselect() { return false; }
    public bool Deselect() { return false; }

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
                    if (selected1) {
                        selection2 = rgo.GetComponent<CharacterSelectIcon>().ability;
                        if(selection1 == selection2) {  // cant select two of same ability
                            break;
                        }
                        rightHalf.color = rgo.GetComponent<Image>().color;
                        selected2 = true;
                    } else {
                        selection1 = rgo.GetComponent<CharacterSelectIcon>().ability;
                        leftHalf.color = rgo.GetComponent<Image>().color;
                        selected1 = true;
                    }
                }
            }
        }
        if (input.getCancelDown) {
            if (playerSetupGO) {
                Destroy(playerSetupGO);
            }else if (selected2) {
                selected2 = false;
                rightHalf.color = Color.white;
            } else if (selected1) {
                leftHalf.color = Color.white;
                selected1 = false;
            }
            Ready = false;
        }

        if (input.getStartDown && selected1 && selected2 && playerSetupGO == null) {
            playerSetupGO = new GameObject();
            PlayerSetup playerSetup = playerSetupGO.AddComponent<PlayerSetup>();
            playerSetup.Initalize(input.HeldInput, playerNumber);
            PlayerSetup.PlayerSetupData pd = new PlayerSetup.PlayerSetupData();
            pd.firstAbilities = new PlayerSetupNetworkedData.AbilityId[] { selection1 };
            pd.secondAbilities = new PlayerSetupNetworkedData.AbilityId[] { selection2 };
            playerSetup.playerData = pd;
            Ready = true;
        }

	}
}
