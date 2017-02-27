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
        public RegisteredPlayerGrouping(int ownerActorID, int bindingID, RegisteredPlayer registeredPlayer) {
            this.ownerActorID = ownerActorID;
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
    protected RectTransform[] pressStartPrompts;

    [SerializeField]
    protected Text continuePrompt;

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
        PhotonNetwork.OnEventCall += OnEvent;
        registeredPlayers = new RegisteredPlayerGrouping[numPlayers];
        registeredBindings = new bool[possibleBindings.Length];
        Assert.IsNull(main);
        main = this;
        Assert.IsTrue(pressStartPrompts.Length == numPlayers);

        // clear out all registered players on scene start (incase moved back to here with back button)
        RegisteredPlayer[] playerObjects = FindObjectsOfType<RegisteredPlayer>();
        for(int i = 0; i < playerObjects.Length; ++i) {
            Destroy(playerObjects[i].gameObject);
        }
    }

    void OnDestroy() {
        PhotonNetwork.OnEventCall -= OnEvent;
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

    void sendRegistrationRequest(byte bindingID) {
        //Send request to master client
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.MasterClient;

        //Send the event to create the remote copies
        R41DNetworking.RaiseEvent((byte)Tags.EventCodes.REQUESTREGISTRATION, bindingID, true, options);
    }

    void sendRegistrationResponse(byte bindingID, byte playerID, byte owningClientActorID) {
        byte[] payloadData = new byte[3];
        payloadData[0] = bindingID;
        payloadData[1] = playerID;
        payloadData[2] = owningClientActorID;

        //Send request to others
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.Others;
        //late-joins will need to see this, if we're still around
        options.CachingOption = EventCaching.AddToRoomCacheGlobal;

        //Send the event to create the remote copies
        R41DNetworking.RaiseEvent((byte)Tags.EventCodes.CREATEREGISTRATION, payloadData, true, options);
    }

    void receiveRegistrationRequest(byte bindingID, byte owningClientActorID) {
        if (!PhotonNetwork.isMasterClient) {
            Debug.LogError("Only the master client should receive requestRegistration events");
            return;
        }

        for (int i = 0; i < numPlayers; i++) {
            if (registeredPlayers[i] != null) {
                if (registeredPlayers[i].bindingID == bindingID && registeredPlayers[i].ownerActorID == owningClientActorID) {
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

        //Theoretically, we could use an event sent to every client. However, I don't trust this to execute on the local client immediately, which could cause problems with duplicate requests.
        CreateRegisteredPlayer(bindingID, (byte)playerID, owningClientActorID);
        sendRegistrationResponse(bindingID, (byte)playerID, owningClientActorID);
    }

    void sendRemoval(byte playerID, int owningClientActorID) {

        Packet payloadData = new Packet();

        payloadData.Write(playerID);
        payloadData.Write(owningClientActorID);

        //Send request to all clients
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.All;
        //late-joins will need to see this, if we're still around
        options.CachingOption = EventCaching.AddToRoomCacheGlobal;

        //Send the event to create the remote copies
        R41DNetworking.RaiseEvent((byte)Tags.EventCodes.REMOVEREGISTRATION, payloadData.getData(), true, options);
    }

    public void removePlayer(byte playerID) {
        Assert.IsTrue(registeredPlayers[playerID] != null && registeredPlayers[playerID].ownerActorID == PhotonNetwork.player.ID);
        sendRemoval(playerID, PhotonNetwork.player.ID);
    }

    void receiveRemoval(byte playerID, int owningClientActorID) {
        if (registeredPlayers[playerID] != null && registeredPlayers[playerID].ownerActorID == owningClientActorID) {
            Destroy(registeredPlayers[playerID].registeredPlayer.gameObject);

            if (owningClientActorID == PhotonNetwork.player.ID) {
                registeredBindings[registeredPlayers[playerID].bindingID] = false;
            }

            registeredPlayers[playerID] = null;
            pressStartPrompts[playerID].gameObject.SetActive(true);

        }
    }

    void sendReadyState(byte playerID, int owningClientActorID, bool state) {
        Packet payloadData = new Packet();

        payloadData.Write(playerID);
        payloadData.Write(owningClientActorID);
        payloadData.Write(state);

        //Send request to all clients
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.All;
        //late-joins will need to see this, if we're still around
        options.CachingOption = EventCaching.AddToRoomCacheGlobal;

        //Send the event to create the remote copies
        R41DNetworking.RaiseEvent((byte)Tags.EventCodes.READYPLAYER, payloadData.getData(), true, options);
    }

    public void setPlayerReady(byte playerID, bool ready) {
        Assert.IsTrue(registeredPlayers[playerID] != null && registeredPlayers[playerID].ownerActorID == PhotonNetwork.player.ID);
        if (registeredPlayers[playerID].ready != ready) {
            sendReadyState(playerID, PhotonNetwork.player.ID, ready);
        }
    }

    void receiveReadyState(byte playerID, int owningClientActorID, bool state) {
        if (registeredPlayers[playerID] != null && registeredPlayers[playerID].ownerActorID == owningClientActorID) {
            registeredPlayers[playerID].ready = state;

            RegistrationUI ui = registeredPlayers[playerID].registeredPlayer.GetComponent<RegistrationUI>();
            ui.ready = state;
        }
    }

    public void OnEvent(byte eventcode, object content, int senderid) {
        switch (eventcode) {
            case (byte)Tags.EventCodes.REQUESTREGISTRATION:
                receiveRegistrationRequest(bindingID: (byte)content, owningClientActorID: (byte)senderid);
                break;

            case (byte)Tags.EventCodes.CREATEREGISTRATION:
                byte[] payloadData = (byte[])content;
                Assert.IsTrue(payloadData.Length == 3);
                CreateRegisteredPlayer(bindingID: payloadData[0], playerId: payloadData[1], owningClientActorID: payloadData[2]);
                break;

            case (byte)Tags.EventCodes.REMOVEREGISTRATION: {
                    Packet p = new Packet(content);
                    byte playerID = p.ReadByte();
                    int owningClientActorID = p.ReadInt();
                    receiveRemoval(playerID, owningClientActorID);
                    break;
                }
            case (byte)Tags.EventCodes.READYPLAYER: {
                    Packet p = new Packet(content);
                    byte playerID = p.ReadByte();
                    int owningClientActorID = p.ReadInt();
                    bool state = p.ReadBool();
                    receiveReadyState(playerID, owningClientActorID, state);
                    break;
                }
            default:
                break;
        }
    }

    void CreateRegisteredPlayer(byte bindingID, byte playerId, byte owningClientActorID) {
        Assert.IsNull(registeredPlayers[playerId]);
        Assert.IsTrue(PhotonPlayer.Find(owningClientActorID) != null); //if this becomes a problem, ignore events for players who no longer exist
        GameObject instantiatedRegisteredPlayer = (GameObject)Instantiate(registeredPlayerPrefab, Vector3.zero, Quaternion.identity);
        RegisteredPlayer registeredPlayer = instantiatedRegisteredPlayer.GetComponent<RegisteredPlayer>();


        if (owningClientActorID == PhotonNetwork.player.ID) {
            //locally controlled player
            registeredPlayer.Initalize(possibleBindings[bindingID].HeldInput, playerId, locallyControlled: true);

            ControllerPlayerInput controllerInput = possibleBindings[bindingID].HeldInput as ControllerPlayerInput;
            if (controllerInput != null) {
                controllerInput.Vibrate(vibrationStrength, vibrationDuration);
            }

            registeredBindings[bindingID] = true;

        } else {
            //remotely controlled player
            registeredPlayer.Initalize(possibleBindings[bindingID].HeldInput, playerId, locallyControlled: false);
        }

        registeredPlayers[playerId] = new RegisteredPlayerGrouping(owningClientActorID, bindingID, registeredPlayer);

        pressStartPrompts[playerId].gameObject.SetActive(false);
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player) {
        if (PhotonNetwork.isMasterClient) {
            for (byte i = 0; i < registeredPlayers.Length; i++) {
                if (registeredPlayers[i] != null && registeredPlayers[i].ownerActorID == player.ID) {
                    sendRemoval(i, player.ID);
                }
            }
        }
    }

    void Update() {
        for (int i = 0; i < possibleBindings.Length; i++) {
            if (possibleBindings[i].AnyKey() && !possibleBindings[i].getCancel && !registeredBindings[i]) {
                int openPlayerID = getFirstAvailablePlayerID();
                if (openPlayerID >= 0) {
                    //if there exists an open ID
                    sendRegistrationRequest((byte)i);
                }
            }
        }

        //check for moving to the next scene
        int ready = 0;
        int currentPlayers = 0;
        bool hasMasterPlayer = false;
        for (int i = 0; i < numPlayers; i++) {
            if (registeredPlayers[i] != null) {
                currentPlayers++;
                if (registeredPlayers[i].ready) {
                    ready++;
                    if (registeredPlayers[i].ownerActorID == PhotonNetwork.player.ID) {
                        hasMasterPlayer = true;
                    }
                }
            }
        }

        continuePrompt.enabled = ready != 0 && ready == currentPlayers;
        if (requireMaxPlayerCount && ready != numPlayers) {
            continuePrompt.text = "Waiting for " + numPlayers + " players";
        } else {
            continuePrompt.text = "Press start to continue";
        }

        if (PhotonNetwork.isMasterClient && hasMasterPlayer && ready != 0 && ready == currentPlayers) {
            if (!requireMaxPlayerCount || ready == numPlayers) {
                // check if any player hit submit this frame
                for (int i = 0; i < possibleBindings.Length; ++i) {
                    if (possibleBindings[i].getSubmitDown) {
                        SceneManager.LoadScene(nextScene);
                        break;
                    }
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
                    sendRegistrationRequest((byte)i);
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
