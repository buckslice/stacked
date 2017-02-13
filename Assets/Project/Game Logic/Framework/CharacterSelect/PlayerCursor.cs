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

    IEntityUIGroupHolder holder;
    
    CharacterSelectIcon selectedCharacter;
    GameObject characterSelectedDisplay;

    GameObject playerSetupGO = null;
    ReadyChecker readyChecker;
    public bool Ready { get; set; }

    // Use this for initialization
    void Start() {
        view = GetComponent<PhotonView>();
        input = GetComponent<PlayerInputHolder>();
        readyChecker = GameObject.Find("ReadyChecker").GetComponent<ReadyChecker>();
        readyChecker.AddPlayer(this);
        holder = GetComponentInParent<IEntityUIGroupHolder>();

        SetReady(false);
    }

    public void Initialize(int playerNumber) {
        this.playerNumber = playerNumber;
    }

    //TODO: migrate to use ability system for selection and deselection
    public bool CanSelect() { return true; }
    public bool Select(ISelectable selectable) {

        if (Ready) {    // should have to deselect before you can select again?
            return false;
        }

        CharacterSelectIcon selectIcon = selectable as CharacterSelectIcon;
        if (selectIcon == null) {
            return false;
        }

        if (selectIcon.Claimed) {
            return false;
            //error/warning message?
        }

        if (selectedCharacter != null) {
            Deselect();
        }

        selectedCharacter = selectIcon;
        selectedCharacter.Claimed = true;

        leftHalf.color = rightHalf.color = selectIcon.color;

        RectTransform parent = holder.EntityGroup.StatusHolder;
        characterSelectedDisplay = Instantiate(abilityDisplayPrefab, parent) as GameObject;
        characterSelectedDisplay.GetComponent<RectTransform>().Reset();
        characterSelectedDisplay.GetComponent<SelectedAbilityDisplay>().Initialize(input.ability1Name, selectIcon.visualsIcon);

        //create/recreate setupGO
        if (playerSetupGO != null) {
            Destroy(playerSetupGO);
        }

        if (view.isMine) {
            //only the owning player needs to set up their player info for the next scene
            playerSetupGO = new GameObject("PlayerSetupHolder");
            PlayerSetup playerSetup = playerSetupGO.AddComponent<PlayerSetup>();
            playerSetup.Initalize(input.HeldInput, playerNumber);
            PlayerSetup.PlayerSetupData pd = new PlayerSetup.PlayerSetupData();
            pd.firstAbilities = new PlayerSetupNetworkedData.AbilityId[] { selectedCharacter.ability1 };
            pd.secondAbilities = new PlayerSetupNetworkedData.AbilityId[] { selectedCharacter.ability2 };
            pd.playerClass = selectedCharacter.playerClass; // so we can switch models and stuff
            playerSetup.playerData = pd;
        }

        SetReady(true);
        return true;

    }

    public bool CanDeselect() { return selectedCharacter != null; }
    public bool Deselect() {
        if (playerSetupGO != null) {
            Destroy(playerSetupGO);
        }

        if (selectedCharacter == null) {
            return false;
        } else {
            selectedCharacter.Claimed = false;
            selectedCharacter = null;
            Destroy(characterSelectedDisplay);
            characterSelectedDisplay = null;
            rightHalf.color = leftHalf.color = Color.white;

            SetReady(false);
            return true;
        }
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
