using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerRegistration : MonoBehaviour {

    const int numPlayers = 4;

    static PlayerRegistration main;
    public static PlayerRegistration Main { get { return main; } }

    public class RegisteredPlayerGrouping {
        //TODO: add references to the containing RegisteredPlayerGrouping to its component objects
        public readonly int ownerActorID;
        public readonly int bindingID;
        public readonly RegisteredPlayer registeredPlayer;
        public bool ready = false;
        public RegisteredPlayerGrouping(int bindingID, RegisteredPlayer registeredPlayer) {
            this.bindingID = bindingID;
            this.registeredPlayer = registeredPlayer;
        }

        public void Destroy() {
            if (registeredPlayer != null) {
                MonoBehaviour.Destroy(registeredPlayer.transform.root.gameObject);
            }
        }
    }

    [SerializeField]
    protected bool requireMaxPlayerCount = true;

    [SerializeField]
    protected GameObject registeredPlayerPrefab;

    [SerializeField]
    protected string nextScene = Tags.Scenes.BossSelect;

    /// <summary>
    /// Collection of all the possible input bindings. The index of a binding in this list is its bindingID.
    /// </summary>
    [SerializeField]
    protected PlayerInputHolder[] possibleBindings;

    [SerializeField]
    protected PlayerInputHolder inputHolder;

    [SerializeField]
    protected PlayerInputHolder[] XInputBindings;

    private int numJoySticks;

    [SerializeField]
    protected RectTransform[] pressStartPrompts;

    [SerializeField]
    protected Text continuePrompt;
    [SerializeField]
    protected Text skipPrompt;

    [SerializeField]
    protected float vibrationDuration = 0.15f;

    [SerializeField]
    protected float vibrationStrength = 1f;

    /// <summary>
    /// Collection of all the players who have registered. The index of a registeredPlayer is its binding's bindingID, the index of its binding in possibleBindings. Used to ensure a binding is registered at most once.
    /// </summary>
    /// 
    private bool[] registeredBindings;

    private RegisteredPlayerGrouping[] registeredPlayers;
    public RegisteredPlayerGrouping[] RegisteredPlayers { get { return registeredPlayers; } }

    void Awake() {
        registeredPlayers = new RegisteredPlayerGrouping[numPlayers];
        registeredBindings = new bool[possibleBindings.Length + 4];
        XInputBindings = new PlayerInputHolder[4];
        for (int i = 0; i < 4; i++) {
            XInputBindings[i] = GameObject.Instantiate<PlayerInputHolder>(inputHolder);
            XInputBindings[i].HeldInput = new XinputPlayerInput((XInputDotNetPure.PlayerIndex)i);
            XInputBindings[i].HeldInput.Initialize(XInputBindings[i]);
            XInputBindings[i].HeldInput.Player = XInputBindings[i].transform;
        }
        numJoySticks = Input.GetJoystickNames().Length;
        RepopulateJoystickList();
        inputHolder.HeldInput = new KeyboardMousePlayerInput();
        inputHolder.HeldInput.Initialize(inputHolder);
        inputHolder.HeldInput.Player = inputHolder.transform;
        Assert.IsNull(main);
        main = this;
        Assert.IsTrue(pressStartPrompts.Length == numPlayers);

        // clear out all registered players on scene start (incase moved back to here with back button)
        RegisteredPlayer[] playerObjects = FindObjectsOfType<RegisteredPlayer>();
        for (int i = 0; i < playerObjects.Length; ++i) {
            Destroy(playerObjects[i].gameObject);
        }
    }

    void OnDestroy() {
        main = null;
    }

    int getFirstAvailablePlayerID() {
        for (int i = 0; i < numPlayers; i++) {
            if (registeredPlayers[i] == null) {
                return i;
            }
        }
        return -1;
    }

    void CreateRegisteredPlayer(byte bindingID, bool XInput) {
        for (int i = 0; i < numPlayers; i++) {
            if (registeredPlayers[i] != null) {
                if (registeredPlayers[i].bindingID == bindingID) {
                    //already registered
                    return;
                }
            }
        }

        int playerID = getFirstAvailablePlayerID();
        if (playerID < 0) {
            //no valid ID
            return;
        }
        GameObject instantiatedRegisteredPlayer = (GameObject)Instantiate(registeredPlayerPrefab, Vector3.zero, Quaternion.identity);
        RegisteredPlayer registeredPlayer = instantiatedRegisteredPlayer.GetComponent<RegisteredPlayer>();

        //locally controlled player
        if (XInput) {
            registeredPlayer.Initalize(XInputBindings[bindingID - possibleBindings.Length].HeldInput, playerID, locallyControlled: true);
        } else {
            registeredPlayer.Initalize(possibleBindings[bindingID].HeldInput, playerID, locallyControlled: true);
        }

        //ControllerPlayerInput controllerInput = possibleBindings[bindingID].HeldInput as ControllerPlayerInput;

        //Commented out until controller stuff is finalized
        //if (controllerInput != null) {
        //    controllerInput.Vibrate(vibrationStrength, vibrationDuration);
        //}

        registeredBindings[bindingID] = true;

        registeredPlayers[playerID] = new RegisteredPlayerGrouping(bindingID, registeredPlayer);


        pressStartPrompts[playerID].gameObject.SetActive(false);
    }



    public void removePlayer(byte playerID) {
        Destroy(registeredPlayers[playerID].registeredPlayer.gameObject);

        registeredBindings[registeredPlayers[playerID].bindingID] = false;
        registeredPlayers[playerID] = null;
        pressStartPrompts[playerID].gameObject.SetActive(true);

    }

    public void setPlayerReady(byte playerID, bool ready) {
        if (registeredPlayers[playerID] != null) {
            registeredPlayers[playerID].ready = ready;

            RegistrationUI ui = registeredPlayers[playerID].registeredPlayer.GetComponent<RegistrationUI>();
            ui.ready = ready;
        }
    }

    void Update() {
        if (skipping) {
            return;
        }

        if (numJoySticks != Input.GetJoystickNames().Length) {
            RepopulateJoystickList();
        }

        for (int i = 0; i < possibleBindings.Length; i++) {
            if (possibleBindings[i] != null && possibleBindings[i].getAnyKeyDown && !registeredBindings[i]) {
                int openPlayerID = getFirstAvailablePlayerID();
                if (openPlayerID >= 0) {
                    //if there exists an open ID
                    CreateRegisteredPlayer((byte)i, false);
                }
            }
        }


        for (int i = 0; i < XInputBindings.Length; i++) {
            if (XInputBindings[i] != null && XInputBindings[i].getAnyKeyDown && !registeredBindings[i + possibleBindings.Length]) {
                int openPlayerID = getFirstAvailablePlayerID();
                if (openPlayerID >= 0) {
                    //if there exists an open ID
                    CreateRegisteredPlayer((byte)(i + possibleBindings.Length), true);
                }
            }
        }

        //check for moving to the next scene
        int ready = 0;
        int currentPlayers = 0;
        for (int i = 0; i < numPlayers; i++) {
            if (registeredPlayers[i] != null) {
                currentPlayers++;
                if (registeredPlayers[i].ready) {
                    ready++;
                }
            }
        }

        // set up menu text
        continuePrompt.enabled = ready != 0 && ready == currentPlayers;
        skipPrompt.enabled = ready > 1;
        if (requireMaxPlayerCount && ready != numPlayers) {
            continuePrompt.text = "Waiting for " + numPlayers + " players";
        } else {
            continuePrompt.text = "Press A to continue";
            skipPrompt.enabled = false;
        }

        // check if skipping
        if (skipPrompt.enabled) {
            if (Input.GetKey(KeyCode.DownArrow)
            && Input.GetKey(KeyCode.LeftArrow)
            && Input.GetKey(KeyCode.RightArrow)) {
                skipping = true;
                StartCoroutine(SkipRoutine());
            }
        }

        if (ready != 0 && ready == currentPlayers) {
            if (!requireMaxPlayerCount || ready == numPlayers) {
                for (int i = 0; i < possibleBindings.Length; ++i) {
                    if (possibleBindings[i] != null && possibleBindings[i].getSubmitDown) {
                        SceneManager.LoadScene(nextScene);
                        break;
                    }
                }
                for (int i = 0; i < XInputBindings.Length; ++i) {
                    if (XInputBindings[i] != null && XInputBindings[i].getSubmitDown) {
                        SceneManager.LoadScene(nextScene);
                        break;
                    }
                }
            }
        }
    }

    bool skipping = false;
    IEnumerator SkipRoutine() {
        float t = 0.0f;
        continuePrompt.enabled = false;
        skipPrompt.text = "HARD MODE\nENGAGED";
        Vector2 off = skipPrompt.rectTransform.offsetMax;
        while (t < 1.0f) {
            skipPrompt.color = (int)(t * 10.0f) % 2 == 0 ? Color.yellow : Color.white;
            float newT = Mathf.Lerp(off.y, 0.0f, t);
            skipPrompt.rectTransform.offsetMax = new Vector2(off.x, newT);
            skipPrompt.fontSize = (int)Mathf.Lerp(15.0f, 80.0f, t);
            t += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(nextScene);
    }

    private void RepopulateJoystickList() {
        string[] names = Input.GetJoystickNames();
        possibleBindings = new PlayerInputHolder[names.Length + 1];
        bool[] oldRegisteredBindings = registeredBindings;
        registeredBindings = new bool[possibleBindings.Length + 4];
        for (int i = 0; i < oldRegisteredBindings.Length; i++) {
            if (i < registeredBindings.Length) {
                registeredBindings[i] = oldRegisteredBindings[i];
            }
        }

        for (int i = 0; i < possibleBindings.Length; i++) {
            if (i >= names.Length) {
                possibleBindings[i] = GameObject.Instantiate<PlayerInputHolder>(inputHolder);
                possibleBindings[i].HeldInput = new KeyboardMousePlayerInput();
                possibleBindings[i].HeldInput.Initialize(possibleBindings[i]);
                possibleBindings[i].HeldInput.Player = possibleBindings[i].transform;
            } else {
                if (names[i] != "Controller (XBOX 360 For Windows)" && names[i] != "Controller (Xbox One For Windows)") {
                    possibleBindings[i] = GameObject.Instantiate<PlayerInputHolder>(inputHolder);
                    possibleBindings[i].HeldInput = new ControllerPlayerInput((XInputDotNetPure.PlayerIndex)i);
                    possibleBindings[i].HeldInput.Initialize(possibleBindings[i]);
                    possibleBindings[i].HeldInput.Player = possibleBindings[i].transform;
                }
            }
        }
    }

    public void PreregisterPlayer(IPlayerInput input) {
        for (int i = 0; i < possibleBindings.Length; i++) {
            if (possibleBindings[i].HeldInput.Equals(input)) {
                int openPlayerID = getFirstAvailablePlayerID();
                if (openPlayerID >= 0) {
                    //if there exists an open ID
                    CreateRegisteredPlayer((byte)i, false);
                } else {
                    Debug.LogError("There is no openPlayerID for a pre-registered player");
                }
                return;
            }
        }

        Debug.LogWarning("There is no matching player input for the pre-registered player");
        //TODO: add support for a -1 bindingID, for custom bindings?
    }
}

