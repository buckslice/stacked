using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Container class containing data to create Bosss over the network.
/// </summary>
public class BossSetupNetworkedData : MonoBehaviour
{
    /// <summary>
    /// When an ability prefab is created, it needs to be registered here. Do not remove legacy abilities. Order matters.
    /// </summary>
    public enum AbilityId : byte
    {
        PAUSE,
    }

    static BossSetupNetworkedData main;
    public static BossSetupNetworkedData Main { get { return main; } }

    [SerializeField]
    protected GameObject baseBossPrefab;

    [SerializeField]
    protected GameObject healthBarPrefab;

    /// <summary>
    /// When an ability prefab is created, its prefab needs to be added here. Do not remove legacy abilities. Order matters.
    /// </summary>
    public GameObject[] abilityPrefabs;

    void Awake()
    {
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

    GameObject abilityIdToPrefab(AbilityId id)
    {
        int index = (int)id;
        Assert.IsTrue(index < abilityPrefabs.Length); //check to make sure the ability ID data is self-consistent
        return abilityPrefabs[index];
    }

    GameObject InstantiateAbility(AbilityId ability, Transform parent, AbilityNetworking abilityNetworking)
    {
        GameObject instantiatedAbility = (GameObject)Instantiate(abilityIdToPrefab(ability), parent);
        instantiatedAbility.transform.Reset();
        abilityNetworking.AddNetworkedAbility(instantiatedAbility);
        return instantiatedAbility;
    }

    /// <summary>
    /// Creates a Boss on all clients, owned by the local client. Assumes all clients have already connected.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="BossNumber"></param>
    public void CreateBoss(BossSetup.BossSetupData BossData)
    {
        int allocatedViewId = PhotonNetwork.AllocateViewID();

        //Create our local copy
        InstantiateBoss(allocatedViewId, BossData);

        //Create the network payload to send for remote copies

        //data variable has non-constant size; use serialization
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        byte[] serializedData;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            binaryFormatter.Serialize(memoryStream, BossData);
            serializedData = memoryStream.ToArray();
        }
        byte[] payloadData = new byte[serializedData.Length + 4];
        System.BitConverter.GetBytes(allocatedViewId).CopyTo(payloadData, 0);
        serializedData.CopyTo(payloadData, 4);

        //We already created our copy, so don't send to self
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.Others;

        //Send the event to create the remote copies
        R41DNetworking.RaiseEvent((byte)Tags.EventCodes.CREATEBOSS, payloadData, true, options);
    }

    public void OnEvent(byte eventcode, object content, int senderid)
    {
        if (eventcode != (byte)Tags.EventCodes.CREATEBOSS)
        {
            return;
        }

        byte[] data = (byte[])content;
        int allocatedViewId = System.BitConverter.ToInt32(data, 0);
        BossSetup.BossSetupData BossData;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            memoryStream.Write(data, 4, data.Length - 4);
            memoryStream.Seek(0, SeekOrigin.Begin);
            BossData = (BossSetup.BossSetupData)binaryFormatter.Deserialize(memoryStream);
        }
        InstantiateBoss(allocatedViewId, BossData);
    }

    /// <summary>
    /// Creates and initializes a Boss.
    /// </summary>
    /// <param name="BossNumber"></param>
    /// <param name="allocatedViewId"></param>
    public void InstantiateBoss(int allocatedViewId, BossSetup.BossSetupData BossData)
    {
        GameObject Boss = (GameObject)Instantiate(baseBossPrefab, Vector3.zero, Quaternion.identity); //TODO: use spawn point
        Boss.name = "Boss";

        //assign view ID
        PhotonView toInitialize = Boss.GetComponent<PhotonView>();
        toInitialize.viewID = allocatedViewId;

        //TODO: health bar

        AbilityNetworking abilityNetworking = Boss.GetComponent<AbilityNetworking>();

        //add abilities
        foreach (AbilityId ability in BossData.abilities)
        {
            InstantiateAbility(ability, Boss.transform, abilityNetworking);
        }
    }
}
