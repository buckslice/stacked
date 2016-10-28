using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PlayerInputHolder))]
public class PlayerCursor : MonoBehaviour, ISelection, IPlayerID, IAbilityDisplayHolder {

    [SerializeField]
    protected GameObject abilityDisplayPrefab;
    [SerializeField]
    public int playerNumber = -1;
    public int PlayerID { get { return playerNumber; } }
    public float moveSpeed = 4.0f;
    public Image leftHalf;
    public Image rightHalf;

    PointerEventData pointer = new PointerEventData(EventSystem.current);
    List<RaycastResult> results = new List<RaycastResult>();
    PlayerInputHolder input;
    public IPlayerInputHolder Input { get { return input; } }

    EntityUIGroupHolder holder;

    PlayerSetupNetworkedData.AbilityId selection1;
    PlayerSetupNetworkedData.AbilityId selection2;
    GameObject selection1Display;
    GameObject selection2Display;

    GameObject playerSetupGO = null;
    ReadyChecker readyChecker;
    public bool Ready { get; set; }

    // Use this for initialization
    void Start () {
        input = GetComponent<PlayerInputHolder>();
        readyChecker = GameObject.Find("ReadyChecker").GetComponent<ReadyChecker>();
        readyChecker.AddPlayer(this);

        holder = GetComponentInParent<EntityUIGroupHolder>();

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
        //transform.Translate(input.movementDirection * moveSpeed *Time.deltaTime* Screen.width / 800.0f);

        if (input.getSubmitDown) {
            pointer.position = new Vector3(transform.position.x, transform.position.y);
            results.Clear();

            EventSystem.current.RaycastAll(pointer, results);

            for(int i = 0; i < results.Count; ++i) {
                GameObject rgo = results[i].gameObject;
                if (rgo.CompareTag("AbilityIcon")) {
                    CharacterSelectIcon selectIcon = rgo.GetComponent<CharacterSelectIcon>();

                    if (selection1Display == null) {

                        PlayerSetupNetworkedData.AbilityId newSelection = selectIcon.ability;

                        selection1 = newSelection;
                        leftHalf.color = rgo.GetComponent<Image>().color;

                        RectTransform parent = holder.EntityGroup.StatusHolder;
                        selection1Display = Instantiate(abilityDisplayPrefab, parent) as GameObject;
                        selection1Display.GetComponent<RectTransform>().Reset();
                        selection1Display.GetComponent<SelectedAbilityDisplay>().Initialize(input.ability1Name, selectIcon.abilityIcon);

                    } else if (selection2Display == null) {

                        PlayerSetupNetworkedData.AbilityId newSelection = selectIcon.ability;

                        if (selection1 == newSelection) {  // cant select two of same ability
                            break;
                        }
                        selection2 = newSelection;

                        rightHalf.color = rgo.GetComponent<Image>().color;

                        RectTransform parent = holder.EntityGroup.StatusHolder;
                        selection2Display = Instantiate(abilityDisplayPrefab, parent) as GameObject;
                        selection2Display.GetComponent<RectTransform>().Reset();
                        selection2Display.GetComponent<SelectedAbilityDisplay>().Initialize(input.ability2Name, selectIcon.abilityIcon);

                        //create/recreate setupGO
                        if (playerSetupGO) {
                            Destroy(playerSetupGO);
                        }
                        Assert.IsTrue(selection1Display != null && selection2Display != null);
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
        }
        if (input.getCancelDown) {

            if (playerSetupGO) {
                Destroy(playerSetupGO);
            }
            
            if (selection2Display != null) {
                Destroy(selection2Display);
                selection2Display = null;
                rightHalf.color = Color.white;
            } else if (selection1Display) {
                Destroy(selection1Display);
                selection1Display = null;
                leftHalf.color = Color.white;
            }

            Ready = false;
        }
	}
}
