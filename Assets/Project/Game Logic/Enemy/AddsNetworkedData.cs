using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Container class containing data to create Adds over the network. The entire add (including abilities) is saved as one prefab.
/// </summary>
public class AddsNetworkedData : MonoBehaviour {
    /// <summary>
    /// When an add is created, it needs to be registered here. Do not remove legacy abilities. Order matters.
    /// </summary>
    public enum AddID : byte {
        EXPLOSIVE,
        RANGED
    }

    static AddsNetworkedData main;
    public static AddsNetworkedData Main { get { return main; } }

    /// <summary>
    /// When an add is created, its data needs to be added here. Do not remove legacy data. Order matters.
    /// </summary>
    [SerializeField]
    public GameObject[] addPrefabs;


    void Awake() {
        if (main != null) {
            PhotonNetwork.OnEventCall -= Main.OnEvent;
            Destroy(Main.transform.root.gameObject);
        }
        main = this;
        DontDestroyOnLoad(this.transform.root.gameObject);
        PhotonNetwork.OnEventCall += OnEvent;
    }

    void OnDestroy() {
        if (Main == this) {
            main = null;
        }
        PhotonNetwork.OnEventCall -= OnEvent;
    }

    /// <summary>
    /// Creates a Boss on all clients, owned by the local client. Assumes all clients have already connected.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="BossNumber"></param>
    public GameObject CreateAdd(AddID addType, Transform spawn) {
        int allocatedViewId = PhotonNetwork.AllocateViewID();

        //Create our local copy
        GameObject result = InstantiateAdd(allocatedViewId, addType, spawn.position, spawn.rotation);

        //Create the network payload to send for remote copies
        Packet p = new Packet();
        p.Write(allocatedViewId);
        p.Write(addType);
        p.Write(spawn.position);
        p.Write(spawn.rotation);

        //We already created our copy, so don't send to self
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.Others;

        //Send the event to create the remote copies
        R41DNetworking.RaiseEvent((byte)Tags.EventCodes.CREATEADD, p.getData(), true, options);
        return result;
    }

    public void OnEvent(byte eventcode, object content, int senderid) {
        if (eventcode != (byte)Tags.EventCodes.CREATEADD) {
            return;
        }

        Packet p = new Packet(content);
        int allocatedViewId = p.ReadInt();
        AddID addType = (AddID)p.ReadObject();
        Vector3 spawnPoint = p.ReadVector3();
        Quaternion spawnOrientation = p.ReadQuaternion();

        InstantiateAdd(allocatedViewId, addType, spawnPoint, spawnOrientation);
    }

    /// <summary>
    /// Creates and initializes an Add.
    /// </summary>
    /// <param name="BossNumber"></param>
    /// <param name="allocatedViewId"></param>
    public GameObject InstantiateAdd(int allocatedViewId, AddID addType, Vector3 spawnPoint, Quaternion spawnOrientation) {
        GameObject addGO = (GameObject)Instantiate(addPrefabs[(byte)addType], spawnPoint, spawnOrientation); //TODO: double check this works

        //assign view ID
        PhotonView toInitialize = addGO.GetComponent<PhotonView>();
        toInitialize.viewID = allocatedViewId;

        DamageHolder damageHolder = addGO.GetComponent<DamageHolder>();
        damageHolder.Initialize(new Add(damageHolder));

        return addGO;

        //TODO: add adds to camera?
        //Camera.main.transform.parent.GetComponent<CameraController>().boss = addGO.transform;
    }
}
