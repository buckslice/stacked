using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Container class containing data to create Players over the network.
/// </summary>
public class PlayerSetupNetworkedData : MonoBehaviour {

    /// <summary>
    /// When an ability prefab is created, it needs to be registered here. Do not remove legacy abilities. Order matters.
    /// </summary>
    public enum AbilityId : byte {
        DERP,
        DASH,
        TAUNT,
        BLOCK,
        SHIELD,
        RESIST,
        FIREBALL,
        SPAWNADD,
        BLINK,
        MELEECONEBASICATTACK,
        HEALINGSPHERE,
        HEALINGBEAM,
        SHIELDING,
        DRFIELD,
        THROW,
        MAGICSHOTGUN,
        JUMP,
        GRAB,
    }

    static PlayerSetupNetworkedData main;
    public static PlayerSetupNetworkedData Main { get { return main; } }

    [SerializeField]
    protected GameObject basePlayerPrefab;

    [SerializeField]
    protected Transform[] spawnPoints;

    [SerializeField]
    protected bool externalAbilityData = false;
    private GetAbilitiesFromData dataSource;

    /// <summary>
    /// When an ability prefab is created, its prefab needs to be added here. Do not remove legacy abilities. Order matters.
    /// </summary>
    public GameObject[] abilityPrefabs;

    void Awake() {
        if (main != null) {
            PhotonNetwork.OnEventCall -= Main.OnEvent;
            Destroy(Main.transform.root.gameObject);
        }
        main = this;
        DontDestroyOnLoad(this.transform.root.gameObject);
        PhotonNetwork.OnEventCall += OnEvent;

        if (externalAbilityData) {
            dataSource = GetComponent<GetAbilitiesFromData>();
            abilityPrefabs = dataSource.getAbilities();
        }
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

    /// <summary>
    /// rebinds the ability to use the specified binding.
    /// </summary>
    /// <param name="ability"></param>
    /// <param name="binding"></param>
    void Rebind(GameObject ability, AbilityKeybinding newBinding) {
        foreach (IAbilityKeybound binding in ability.GetComponentsInChildren<IAbilityKeybound>()) {
            binding.Binding = newBinding;
        }
    }

    GameObject InstantiateAbility(AbilityId ability, Transform parent, AbilityNetworking abilityNetworking) {
        GameObject instantiatedAbility = (GameObject)Instantiate(abilityIdToPrefab(ability), parent);
        instantiatedAbility.transform.Reset();
        abilityNetworking.AddNetworkedAbility(instantiatedAbility);
        return instantiatedAbility;
    }

    /// <summary>
    /// Creates a Player on all clients, owned by the local client. Assumes all clients have already connected.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="playerID"></param>
    public GameObject CreatePlayer(byte playerID, IPlayerInput input, PlayerSetup.PlayerSetupData playerData)
    {
        int allocatedViewId = PhotonNetwork.AllocateViewID();

        //Create our local copy
        GameObject player = InstantiatePlayer(playerID, allocatedViewId, input, playerData);

        RaiseEvent(Tags.EventCodes.CREATEPLAYER, playerID, allocatedViewId, playerData);

        return player;
    }

    public GameObject CreateAIPlayer(byte playerID, PlayerSetup.PlayerSetupData playerData) {
        int allocatedViewId = PhotonNetwork.AllocateViewID();

        GameObject player = (GameObject)Instantiate(basePlayerPrefab, Vector3.zero, Quaternion.identity);
        //assign view ID
        PhotonView toInitialize = player.GetComponent<PhotonView>();
        toInitialize.viewID = allocatedViewId;

        // destroy unnecessary scripts
        player.AddComponent<AIMovement>();
        player.GetComponent<Rigidbody>().isKinematic = true;

        DestroyImmediate(player.GetComponent<PlayerMovement>());
        DestroyImmediate(player.GetComponent<PlayerInputHolder>());

        // temp for camera testing, make ai players immune to damage
        Damageable dmg = player.GetComponentInChildren<Damageable>();
        dmg.PhysicalVulnerabilityMultiplier.AddModifier(0);
        dmg.MagicalVulnerabilityMultiplier.AddModifier(0);

        AbilityNetworking abilityNetworking = player.GetComponent<AbilityNetworking>();

        foreach (AbilityId ability in playerData.basicAttacks) {
            InstantiateAbility(ability, player.transform, abilityNetworking);
        }
        foreach (AbilityId ability in playerData.firstAbilities) {
            InstantiateAbility(ability, player.transform, abilityNetworking);
        }
        foreach (AbilityId ability in playerData.secondAbilities) {
            InstantiateAbility(ability, player.transform, abilityNetworking);
        }
        foreach (AbilityId ability in playerData.abilities) {
            InstantiateAbility(ability, player.transform, abilityNetworking);
        }

        // destroy all input triggers
        InputTrigger[] triggers = player.GetComponentsInChildren<InputTrigger>();
        for(int i = 0; i < triggers.Length; ++i) {
            DestroyImmediate(triggers[i]);
        }

        player.name = "Player" + (playerID + 1) + "-AI";

        if (BossSetup.Main != null && BossSetup.Main.PlayerSpawnPoints.Length > playerID) {
            player.transform.position = BossSetup.Main.PlayerSpawnPoints[playerID].position;
            player.transform.rotation = BossSetup.Main.PlayerSpawnPoints[playerID].rotation;
        }

        DamageHolder damageHolder = player.GetComponent<DamageHolder>();

        //assign playerID
        damageHolder.Initialize(new Player(playerID, damageHolder));

        RaiseEvent(Tags.EventCodes.CREATEPLAYER, playerID, allocatedViewId, playerData);

        return player;

    }

    void RaiseEvent(Tags.EventCodes eventCode, byte playerID, int allocatedViewId, PlayerSetup.PlayerSetupData playerData) {
        //Create the network payload to send for remote copies
        object[] payloadData = new object[3];
        payloadData[0] = playerID;
        payloadData[1] = allocatedViewId;
        payloadData[2] = playerData.toByteArray();

        //We already created our copy, so don't send to self
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.Others;

        //Send the event to create the remote copies
        R41DNetworking.RaiseEvent((byte)eventCode, payloadData, true, options);
    }

    public void OnEvent(byte eventcode, object content, int senderid) {
        if (eventcode == (byte)Tags.EventCodes.CREATEPLAYER) {
            object[] data = (object[])content;
            byte playerNumber = (byte)data[0];
            int allocatedViewId = (int)data[1];
            PlayerSetup.PlayerSetupData playerData = PlayerSetup.PlayerSetupData.fromByteArray((byte[])data[2]);

            InstantiatePlayer(playerNumber, allocatedViewId, new NullInput(), playerData);
        }
    }

    /// <summary>
    /// Creates and initializes a Player.
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <param name="allocatedViewId"></param>
    public GameObject InstantiatePlayer(byte playerID, int allocatedViewId, IPlayerInput input, PlayerSetup.PlayerSetupData playerData) {
        GameObject player = InstantiatePlayer(allocatedViewId, input, playerData);
        player.name = "Player" + (playerID + 1);

        if (BossSetup.Main != null && BossSetup.Main.PlayerSpawnPoints.Length > playerID) {
            player.transform.position = BossSetup.Main.PlayerSpawnPoints[playerID].position;
            player.transform.rotation = BossSetup.Main.PlayerSpawnPoints[playerID].rotation;
        }
        
        DamageHolder damageHolder = player.GetComponent<DamageHolder>();

        //assign playerID
        damageHolder.Initialize(new Player(playerID, damageHolder));

        return player;
    }

    public GameObject InstantiatePlayer(int allocatedViewId, IPlayerInput input, PlayerSetup.PlayerSetupData playerData) {
        GameObject player = (GameObject)Instantiate(basePlayerPrefab, Vector3.zero, Quaternion.identity); //TODO: use spawn point

        //assign view ID
        PhotonView toInitialize = player.GetComponent<PhotonView>();
        toInitialize.viewID = allocatedViewId;

        //assign input bindings
        player.GetComponent<PlayerInputHolder>().HeldInput = input;

        AbilityNetworking abilityNetworking = player.GetComponent<AbilityNetworking>();

        foreach (AbilityId ability in playerData.basicAttacks) {
            GameObject instantiatedAbility = InstantiateAbility(ability, player.transform, abilityNetworking);
            Rebind(instantiatedAbility, AbilityKeybinding.BASICATTACK);
        }

        foreach (AbilityId ability in playerData.firstAbilities) {
            GameObject instantiatedAbility = InstantiateAbility(ability, player.transform, abilityNetworking);
            Rebind(instantiatedAbility, AbilityKeybinding.ABILITY1);
        }

        foreach (AbilityId ability in playerData.secondAbilities) {
            GameObject instantiatedAbility = InstantiateAbility(ability, player.transform, abilityNetworking);
            Rebind(instantiatedAbility, AbilityKeybinding.ABILITY2);
        }

        foreach (AbilityId ability in playerData.abilities) {
            InstantiateAbility(ability, player.transform, abilityNetworking);
        }

        return player;
    }

}
