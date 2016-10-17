using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class PlayerRegistration : MonoBehaviour {

    const int numPlayers = 4;

    class RegisteredPlayerGrouping
    {
        //TODO: add references to the containing RegisteredPlayerGrouping to its component objects
        public readonly int ownerActorID;
        public readonly int bindingID;
        public readonly RegisteredPlayer registeredPlayer;
        public readonly GameObject registrationUI;
        public RegisteredPlayerGrouping(int ownerActorID, int bindingID, RegisteredPlayer registeredPlayer, GameObject registrationUI)
        {
            this.ownerActorID = ownerActorID;
            this.bindingID = bindingID;
            this.registeredPlayer = registeredPlayer;
            this.registrationUI = registrationUI;
        }

        public void Destroy()
        {
            if (registeredPlayer != null)
            {
                MonoBehaviour.Destroy(registeredPlayer.transform.root.gameObject);
            }

            if (registrationUI != null)
            {
                PhotonNetwork.Destroy(registrationUI);
            }
        }
    }

    [SerializeField]
    protected GameObject registeredPlayerPrefab;

    [SerializeField]
    protected string playerRegistrationUIPrefabName = Tags.Resources.RegistrationUI;

    /// <summary>
    /// Collection of all the possible input bindings. The index of a binding in this list is its bindingID.
    /// </summary>
    [SerializeField]
    protected PlayerInputHolder[] possibleBindings;

    /// <summary>
    /// Collection of all the players who have registered. The index of a registeredPlayer is its binding's bindingID, the index of its binding in possibleBindings. Used to ensure a binding is registered at most once.
    /// </summary>
    /// 
    private bool[] registeredBindings;

    private RegisteredPlayerGrouping[] registeredPlayers;

    void Awake()
    {
        PhotonNetwork.OnEventCall += OnEvent;
    }

    void Start()
    {
        registeredPlayers = new RegisteredPlayerGrouping[numPlayers];
        registeredBindings = new bool[possibleBindings.Length];
    }

    void OnDestroy()
    {
        PhotonNetwork.OnEventCall -= OnEvent;
    }

    int getFirstAvailablePlayerID()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            if (registeredPlayers[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    void sendRegistrationRequest(byte bindingID)
    {
        //Send request to master client
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.MasterClient;

        //Send the event to create the remote copies
        R41DNetworking.RaiseEvent((byte)Tags.EventCodes.REQUESTREGISTRATION, bindingID, true, options);
    }

    void sendRegistrationResponse(byte bindingID, byte playerID, byte owningClientActorID)
    {
        byte[] payloadData = new byte[3];
        payloadData[0] = bindingID;
        payloadData[1] = playerID;
        payloadData[2] = owningClientActorID;

        //Send request to others
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.Others;

        //Send the event to create the remote copies
        R41DNetworking.RaiseEvent((byte)Tags.EventCodes.CREATEREGISTRATION, payloadData, true, options);
    }

    void receiveRegistrationRequest(byte bindingID, byte owningClientActorID)
    {
        if (!PhotonNetwork.isMasterClient)
        {
            Debug.LogError("Only the master client should receive requestRegistration events");
            return;
        }

        for (int i = 0; i < numPlayers; i++)
        {
            if (registeredPlayers[i] != null)
            {
                if (registeredPlayers[i].bindingID == bindingID && registeredPlayers[i].ownerActorID == owningClientActorID)
                {
                    //already registered
                    return;
                }
            }
        }

        int playerID = getFirstAvailablePlayerID();
        if (playerID < 0)
        {
            //no valid ID
            return;
        }

        //Theoretically, we could use an event sent to every client. However, I don't trust this to execute on the local client immediately, which could cause problems with duplicate requests.
        CreateRegisteredPlayer(bindingID, (byte)playerID, owningClientActorID);
        sendRegistrationResponse(bindingID, (byte)playerID, owningClientActorID);
    }

    public void OnEvent(byte eventcode, object content, int senderid)
    {
        switch (eventcode)
        {
            case (byte)Tags.EventCodes.REQUESTREGISTRATION:
                receiveRegistrationRequest(bindingID: (byte)content, owningClientActorID: (byte)senderid);
                break;
            case (byte)Tags.EventCodes.CREATEREGISTRATION:
                byte[] payloadData = (byte[])content;
                Assert.IsTrue(payloadData.Length == 3);
                CreateRegisteredPlayer(bindingID: payloadData[0], playerId: payloadData[1], owningClientActorID: payloadData[2]);
                break;
            default:
                break;
        }
    }

    void CreateRegisteredPlayer(byte bindingID, byte playerId, byte owningClientActorID)
    {
        Assert.IsNull(registeredPlayers[playerId]);
        if (owningClientActorID == PhotonNetwork.player.ID)
        {
            //if we own it, we need to create it

            GameObject instantiatedRegisteredPlayer = (GameObject)Instantiate(registeredPlayerPrefab, Vector3.zero, Quaternion.identity);
            RegisteredPlayer registeredPlayer = instantiatedRegisteredPlayer.GetComponent<RegisteredPlayer>();
            registeredPlayer.Initalize(possibleBindings[bindingID].HeldInput, playerId);

            GameObject instantiatedRegistrationUI = PhotonNetwork.Instantiate(playerRegistrationUIPrefabName, new Vector3(0, 0, 0), Quaternion.identity, 0);

            registeredPlayers[playerId] = new RegisteredPlayerGrouping(owningClientActorID, bindingID, registeredPlayer, instantiatedRegistrationUI);

            registeredBindings[bindingID] = true;
        }
        else
        {
            //otherwise, we just need to track it
            registeredPlayers[playerId] = new RegisteredPlayerGrouping(owningClientActorID, bindingID, null, null);
        }
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        for (int i = 0; i < registeredPlayers.Length; i++)
        {
            if (registeredPlayers[i].ownerActorID == player.ID)
            {
                registeredPlayers[i] = null;
            }
        }
    }

    void Update()
    {
        for (int i=0; i<possibleBindings.Length; i++)
        {
            if (possibleBindings[i].getSubmit && !registeredBindings[i])
            {
                int openPlayerID = getFirstAvailablePlayerID();
                if (openPlayerID >= 0)
                {
                    //if there exists an open ID
                    sendRegistrationRequest((byte)i);
                }
            }
        }
    }
}
