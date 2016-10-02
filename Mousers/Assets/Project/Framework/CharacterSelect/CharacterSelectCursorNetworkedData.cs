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
    private GameObject cursorPrefab;


	void Awake () {
        if (main != null)
        {
            Destroy(main.transform.root.gameObject);
        }
        main = this;
        DontDestroyOnLoad(this.transform.root.gameObject);
        Debug.Log("Main Set!");
	}

    void OnDestroy()
    {
        if (main == this)
        {
            main = null;
            Debug.Log("Main Unset!");
        }
    }

    /// <summary>
    /// Creates a CharacterSelectCursor on all clients, owned by the local client. Assumes all clients have already connected.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="playerNumber"></param>
    public void CreateCharacterSelectCursor(IPlayerInput input, int playerNumber)
    {
        int allocatedViewId = PhotonNetwork.AllocateViewID();
        GameObject cursor = (GameObject)Instantiate(cursorPrefab, Vector3.zero, Quaternion.identity); //TODO: change spawn point based on player number
        PhotonView toInitialize = cursor.GetComponent<PhotonView>();
        toInitialize.viewID = allocatedViewId;
        cursor.GetComponent<PlayerInputHolder>().heldInput = input;
        cursor.GetComponent<CharacterSelectCursor>().Initialize(playerNumber);

        byte[] data = new byte[5];
        data[0] = (byte)playerNumber;
        System.BitConverter.GetBytes(allocatedViewId).CopyTo(data, 1);


        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.Others;
        PhotonNetwork.RaiseEvent((byte)Tags.EventCodes.CREATEREMOTECHARACTERSELECTCURSOR, data, true, options);
    }

    private void OnEvent(byte eventcode, object content, int senderid)
    {
        if (eventcode != (int)Tags.EventCodes.CREATEREMOTECHARACTERSELECTCURSOR)
        {
            return;
        }

        byte[] data = (byte[])content;
        Assert.AreEqual(data.Length, 5);
        CreateRemoteCharacterSelectCursor(data[0], System.BitConverter.ToInt32(data, 1));
    }

    /// <summary>
    /// Creates a CharacterSelectCursor that is not owned by the local client.
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <param name="allocatedViewId"></param>
    public void CreateRemoteCharacterSelectCursor(byte playerNumber, int allocatedViewId)
    {
        GameObject cursor = (GameObject)Instantiate(cursorPrefab, Vector3.zero, Quaternion.identity); //TODO: change spawn point based on player number
        PhotonView toInitialize = cursor.GetComponent<PhotonView>();
        toInitialize.viewID = allocatedViewId;
        cursor.GetComponent<CharacterSelectCursor>().Initialize(playerNumber);
    }
}
