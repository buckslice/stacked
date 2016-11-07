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

    /// <summary>
    /// vectors in the range [0..1], which map to positions on the screen for spawn points
    /// </summary>
    [SerializeField]
    protected Vector2[] screenSpaceSpawnPoints;

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
        R41DNetworking.RaiseEvent((byte)Tags.EventCodes.CREATEREMOTECHARACTERSELECTCURSOR, data, true, options);
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
        if (playerNumber < screenSpaceSpawnPoints.Length) {
            //we have a spawn point, use it
            Vector2 screenSpaceSpawnPoint = screenSpaceSpawnPoints[playerNumber];
            Vector2 worldSpawnPoint = new Vector2(screenSpaceSpawnPoint.x * Screen.width, screenSpaceSpawnPoint.y * Screen.height);
            cursor = (GameObject)Instantiate(cursorPrefab, worldSpawnPoint, Quaternion.identity, GameObject.Find("Canvas").transform);
        } else {
            // random spawn
            Vector3 screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);
            screenCenter += Random.onUnitSphere * 200.0f;
            screenCenter.z = 0.0f;
            cursor = (GameObject)Instantiate(cursorPrefab, screenCenter, Quaternion.identity, GameObject.Find("Canvas").transform);
        }

        // place cursor directly before the tooltips
        // so they are drawn over everything except tooltips
        Transform toolTips = GameObject.Find("Tooltips").transform;
        cursor.transform.SetSiblingIndex(toolTips.GetSiblingIndex());

        //assign view ID
        PhotonView toInitialize = cursor.GetComponent<PhotonView>();
        toInitialize.viewID = allocatedViewId;
        cursor.GetComponent<PlayerInputHolder>().HeldInput = input;
        cursor.GetComponent<PlayerCursor>().Initialize(playerNumber);
    }
}
