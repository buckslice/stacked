using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

/// <summary>
/// Container class containing data to create CharacterSelectionCursors over the network.
/// </summary>
public class CharacterSelectCursorNetworkedData : MonoBehaviour {
    const string CreateRemoteCharacterSelectCursorRPCName = "CreateRemoteCharacterSelectCursor";

    static CharacterSelectCursorNetworkedData main;
    public static CharacterSelectCursorNetworkedData Main { get { return main; } }

    [SerializeField]
    protected GameObject cursorPrefab;

    [SerializeField]
    protected Transform[] spawnPoints;

	void Awake () {
        if (main != null)
        {
            PhotonNetwork.OnEventCall -= Main.OnEvent;
            Destroy(Main.transform.root.gameObject);
        }
        main = this;
        DontDestroyOnLoad(this.transform.root.gameObject);
        PhotonNetwork.OnEventCall += OnEvent;
	}

    void OnDestroy()
    {
        if (Main == this)
        {
            main = null;
        }
        PhotonNetwork.OnEventCall -= OnEvent;
    }

    /// <summary>
    /// Creates a CharacterSelectCursor on all clients, owned by the local client. Assumes all clients have already connected.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="playerNumber"></param>
    public void CreateCharacterSelectCursor(IPlayerInput input, byte playerNumber)
    {
        int allocatedViewId = PhotonNetwork.AllocateViewID();

        //Create our local copy
        InstantiateCharacterSelectCursor(playerNumber, allocatedViewId, input);

        //Create the network payload to send for remote copies
        byte[] data = new byte[5];
        data[0] = playerNumber;
        System.BitConverter.GetBytes(allocatedViewId).CopyTo(data, 1);

        //We already created our copy, so don't send to self
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.Others;

        //Send the event to create the remote copies
        PhotonNetwork.RaiseEvent((byte)Tags.EventCodes.CREATEREMOTECHARACTERSELECTCURSOR, data, true, options);
    }

    public void OnEvent(byte eventcode, object content, int senderid)
    {
        if (eventcode != (byte)Tags.EventCodes.CREATEREMOTECHARACTERSELECTCURSOR)
        {
            return;
        }

        byte[] data = (byte[])content;
        Assert.AreEqual(data.Length, 5);
        InstantiateCharacterSelectCursor(data[0], System.BitConverter.ToInt32(data, 1), new NullInput());
    }

    /// <summary>
    /// Creates and initializes a CharacterSelectCursor.
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <param name="allocatedViewId"></param>
    public void InstantiateCharacterSelectCursor(byte playerNumber, int allocatedViewId, IPlayerInput input)
    {
        GameObject cursor;

        Assert.IsTrue(playerNumber >= 0);
        if (playerNumber < spawnPoints.Length)
        {
            //we have a spawn point, use it
            Transform toCopy = spawnPoints[playerNumber];
            cursor = (GameObject)Instantiate(cursorPrefab, toCopy.localPosition, toCopy.localRotation);
        }
        else
        {
            cursor = (GameObject)Instantiate(cursorPrefab, Vector3.zero, Quaternion.identity);
        }
        PhotonView toInitialize = cursor.GetComponent<PhotonView>();
        toInitialize.viewID = allocatedViewId;
        cursor.GetComponent<PlayerInputHolder>().heldInput = input;
        cursor.GetComponent<CharacterSelectCursor>().Initialize(playerNumber);
    }
}
