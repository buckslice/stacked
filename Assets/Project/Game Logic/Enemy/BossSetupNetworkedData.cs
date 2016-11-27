using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Container class containing data to create Bosss over the network.
/// </summary>
public class BossSetupNetworkedData : MonoBehaviour {
    /// <summary>
    /// When an ability prefab is created, it needs to be registered here. Do not remove legacy abilities. Order matters.
    /// </summary>
    public enum AbilityId : byte {
        PAUSE,
        VOIDZONE,
        CHARGE,
        EXPLOSIVEADD,
        RANGEDADD,
        KNOCKBACK,
        FLAMESWEEP,
        DEATHWAVE,
        PHASEINVULNERABILITY,
        FLAMESWEEPMK2,
    }

    public enum BossID : byte {
        JOHN,
        DEREK,
        BOSS1_1,
        BOSS1_2,
        BOSS1_3,
        BOSS1_4,
        BOSS1_SUMMONER,
    }

    static BossSetupNetworkedData main;
    public static BossSetupNetworkedData Main { get { return main; } }

    /// <summary>
    /// When a boss is created, its prefab needs to be added here. Do not remove legacy bosses. Order matters.
    /// </summary>
    [SerializeField]
    protected GameObject[] baseBossPrefabs;

    /// <summary>
    /// When an ability prefab is created, its prefab needs to be added here. Do not remove legacy abilities. Order matters.
    /// </summary>
    public GameObject[] abilityPrefabs;

    /// <summary>
    /// When a bossID is created, its data needs to be added here. Do not remove legacy data. Order matters.
    /// </summary>
    public GameObject[] bossDataPrefabs;


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

    GameObject abilityIdToPrefab(AbilityId id) {
        int index = (int)id;
        Assert.IsTrue(index < abilityPrefabs.Length); //check to make sure the ability ID data is self-consistent
        return abilityPrefabs[index];
    }

    GameObject InstantiateAbility(AbilityId ability, Transform parent, AbilityNetworking abilityNetworking) {
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
    public GameObject CreateBoss(BossSetup.BossSetupData bossData, Transform spawn) {
        int allocatedViewId = PhotonNetwork.AllocateViewID();

        //Create our local copy
        GameObject result = InstantiateBoss(allocatedViewId, bossData, spawn.position, spawn.rotation);

        //Create the network payload to send for remote copies
        Packet p = new Packet();
        p.Write(allocatedViewId);
        p.Write(bossData);
        p.Write(spawn.position);
        p.Write(spawn.rotation);

        //We already created our copy, so don't send to self
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.Others;

        //Send the event to create the remote copies
        R41DNetworking.RaiseEvent((byte)Tags.EventCodes.CREATEBOSS, p.getData(), true, options);

        return result;
    }

    public void OnEvent(byte eventcode, object content, int senderid) {
        if (eventcode != (byte)Tags.EventCodes.CREATEBOSS) {
            return;
        }

        Packet p = new Packet(content);
        int allocatedViewId = p.ReadInt();
        BossSetup.BossSetupData bossData = (BossSetup.BossSetupData)p.ReadObject();
        Vector3 spawnPoint = p.ReadVector3();
        Quaternion spawnOrientation = p.ReadQuaternion();

        InstantiateBoss(allocatedViewId, bossData, spawnPoint, spawnOrientation);
    }

    /// <summary>
    /// Creates and initializes a Boss.
    /// </summary>
    /// <param name="BossNumber"></param>
    /// <param name="allocatedViewId"></param>
    public GameObject InstantiateBoss(int allocatedViewId, BossSetup.BossSetupData BossData, Vector3 spawnPoint, Quaternion spawnOrientation) {
        GameObject bossGO = (GameObject)Instantiate(baseBossPrefabs[(byte)BossData.bossID], spawnPoint, spawnOrientation); //TODO: double check this works
        bossGO.name = "Boss";

        //assign view ID
        PhotonView toInitialize = bossGO.GetComponent<PhotonView>();
        toInitialize.viewID = allocatedViewId;

        DamageHolder damageHolder = bossGO.GetComponent<DamageHolder>();
        damageHolder.Initialize(new Boss(damageHolder));

        Camera.main.transform.parent.GetComponent<CameraController>().boss = bossGO.transform;

        //TODO: health bar

        AbilityNetworking abilityNetworking = bossGO.GetComponent<AbilityNetworking>();

        //add abilities
        foreach (AbilityId ability in BossData.abilities) {
            InstantiateAbility(ability, bossGO.transform, abilityNetworking);
        }

        return bossGO;
    }
}
