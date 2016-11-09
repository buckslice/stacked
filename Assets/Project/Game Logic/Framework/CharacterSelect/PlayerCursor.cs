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
    public Image leftHalf;
    public Image rightHalf;
    public Image readyImage;
    public Image background;

    PhotonView view;
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
    void Start() {
        view = GetComponent<PhotonView>();
        input = GetComponent<PlayerInputHolder>();
        readyChecker = GameObject.Find("ReadyChecker").GetComponent<ReadyChecker>();
        readyChecker.AddPlayer(this);
        holder = GetComponentInParent<EntityUIGroupHolder>();

        Assert.IsNotNull(BossSetup.Main);
        readyChecker.LevelToLoad = BossSetup.Main.BossData.sceneName;

        SetReady(false);
    }

    public void Initialize(int playerNumber) {
        this.playerNumber = playerNumber;
    }

    //TODO: migrate to use ability system for selection and deselection
    public bool CanSelect() { return true; }
    public bool Select(ISelectable selectable) {

        CharacterSelectIcon selectIcon = selectable as CharacterSelectIcon;
        if (selectIcon == null) {
            return false;
        }

        if (selection1Display == null) {

            PlayerSetupNetworkedData.AbilityId newSelection = selectIcon.ability;

            selection1 = newSelection;
            leftHalf.color = selectIcon.color;

            RectTransform parent = holder.EntityGroup.StatusHolder;
            selection1Display = Instantiate(abilityDisplayPrefab, parent) as GameObject;
            selection1Display.GetComponent<RectTransform>().Reset();
            selection1Display.GetComponent<SelectedAbilityDisplay>().Initialize(input.ability1Name, selectIcon.visualsIcon);

        } else if (selection2Display == null) {

            PlayerSetupNetworkedData.AbilityId newSelection = selectIcon.ability;

            if (selection1 == newSelection) {  // cant select two of same ability
                return false;
            }
            selection2 = newSelection;

            rightHalf.color = selectIcon.color;

            RectTransform parent = holder.EntityGroup.StatusHolder;
            selection2Display = Instantiate(abilityDisplayPrefab, parent) as GameObject;
            selection2Display.GetComponent<RectTransform>().Reset();
            selection2Display.GetComponent<SelectedAbilityDisplay>().Initialize(input.ability2Name, selectIcon.visualsIcon);

            //create/recreate setupGO
            if (playerSetupGO != null) {
                Destroy(playerSetupGO);
            }

            Assert.IsTrue(selection1Display != null && selection2Display != null);

            if (view.isMine) {
                //only the owning player needs to set up their player info for the next scene
                playerSetupGO = new GameObject();
                PlayerSetup playerSetup = playerSetupGO.AddComponent<PlayerSetup>();
                playerSetup.Initalize(input.HeldInput, playerNumber);
                PlayerSetup.PlayerSetupData pd = new PlayerSetup.PlayerSetupData();
                pd.firstAbilities = new PlayerSetupNetworkedData.AbilityId[] { selection1 };
                pd.secondAbilities = new PlayerSetupNetworkedData.AbilityId[] { selection2 };
                playerSetup.playerData = pd;
            }

            SetReady(true);
        }
        return true;
    }

    public bool CanDeselect() { return selection1Display != null; }
    public bool Deselect() {
        if (playerSetupGO != null) {
            Destroy(playerSetupGO);
        }

        if (selection2Display != null) {
            Destroy(selection2Display);
            selection2Display = null;
            rightHalf.color = Color.white;
        } else if (selection1Display != null) {
            Destroy(selection1Display);
            selection1Display = null;
            leftHalf.color = Color.white;
        } else {
            return false; //no action taken
        }

        SetReady(false);

        return true;
    }

    void SetReady(bool isReady) {
        Ready = isReady;
        readyImage.enabled = isReady;
        background.enabled = !isReady;
    }

    PointerEventData pointer = new PointerEventData(EventSystem.current);
    List<RaycastResult> results = new List<RaycastResult>();

    // TODO: port this to work with networked abilities
    // (or dont so players only see the tooltips they are hovering over)
    void Update() {
        pointer.position = transform.position;
        results.Clear();

        EventSystem.current.RaycastAll(pointer, results);

        for (int i = 0; i < results.Count; ++i) {
            // make sure icon parent has an attached image component for this to raycast against
            // did this to avoid calling GetComponentInParent every frame on many objects
            GameObject rgo = results[i].gameObject;
            if (rgo.CompareTag("AbilityIcon")) {
                CharacterSelectIcon selectIcon = rgo.GetComponent<CharacterSelectIcon>();
                selectIcon.SetHover();
            }
        }
    }
}
