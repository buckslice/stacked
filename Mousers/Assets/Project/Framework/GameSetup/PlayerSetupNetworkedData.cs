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
    public enum AbilityId : byte
    {
        DERP,
        DASH,
        TAUNT,
        BLOCK,
        SHIELD,
        RESIST
    }

    static PlayerSetupNetworkedData main;
    public static PlayerSetupNetworkedData Main { get { return main; } }

    [SerializeField]
    protected GameObject basePlayerPrefab;

    [SerializeField]
    protected GameObject healthBarPrefab;

    [SerializeField]
    protected Transform[] spawnPoints;

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

    /// <summary>
    /// rebinds the ability to use the specified binding.
    /// </summary>
    /// <param name="ability"></param>
    /// <param name="binding"></param>
    void Rebind(GameObject ability, AbilityKeybinding newBinding)
    {
        foreach (IAbilityKeybound binding in ability.GetComponentsInChildren<IAbilityKeybound>())
        {
            binding.Binding = newBinding;
        }
    }

    GameObject InstantiateAbility(AbilityId ability, Transform parent, AbilityNetworking abilityNetworking)
    {
        GameObject instantiatedAbility = (GameObject)Instantiate(abilityIdToPrefab(ability), parent);
        instantiatedAbility.transform.Reset();
        abilityNetworking.AddNetworkedAbility(instantiatedAbility.GetComponent<NetworkedAbilityActivation>());
        return instantiatedAbility;
    }

    /// <summary>
    /// Creates a Player on all clients, owned by the local client. Assumes all clients have already connected.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="playerNumber"></param>
    public void CreatePlayer(byte playerNumber, IPlayerInput input, PlayerSetup.PlayerSetupData playerData)
    {
        int allocatedViewId = PhotonNetwork.AllocateViewID();

        //Create our local copy
        InstantiatePlayer(playerNumber, allocatedViewId, input, playerData);

        //Create the network payload to send for remote copies

        //data variable has non-constant size; use serialization
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        byte[] serializedData;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            binaryFormatter.Serialize(memoryStream, playerData);
            serializedData = memoryStream.ToArray();
        }
        byte[] payloadData = new byte[serializedData.Length + 5];
        payloadData[0] = playerNumber;
        System.BitConverter.GetBytes(allocatedViewId).CopyTo(payloadData, 1);
        serializedData.CopyTo(payloadData, 5);

        //We already created our copy, so don't send to self
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.Others;

        //Send the event to create the remote copies
        MouserNetworking.RaiseEvent((byte)Tags.EventCodes.CREATEPLAYER, payloadData, true, options);
    }

    public void OnEvent(byte eventcode, object content, int senderid)
    {
        if (eventcode != (byte)Tags.EventCodes.CREATEPLAYER)
        {
            return;
        }

        byte[] data = (byte[])content;
        byte playerNumber = data[0];
        int allocatedViewId = System.BitConverter.ToInt32(data, 1);
        PlayerSetup.PlayerSetupData playerData;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            memoryStream.Write(data, 5, data.Length - 5);
            memoryStream.Seek(0, SeekOrigin.Begin);
            playerData = (PlayerSetup.PlayerSetupData)binaryFormatter.Deserialize(memoryStream);
        }
        InstantiatePlayer(playerNumber, allocatedViewId, new NullInput(), playerData);
    }

    /// <summary>
    /// Creates and initializes a Player.
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <param name="allocatedViewId"></param>
    public void InstantiatePlayer(byte playerID, int allocatedViewId, IPlayerInput input, PlayerSetup.PlayerSetupData playerData)
    {
        GameObject player = (GameObject)Instantiate(basePlayerPrefab, Vector3.zero, Quaternion.identity); //TODO: use spawn point
        player.name = "Player" + playerID;

        //assign view ID
        PhotonView toInitialize = player.GetComponent<PhotonView>();
        toInitialize.viewID = allocatedViewId;

        //assign playerID
        player.GetComponent<Player>().Initialize(playerID);

        //assign input bindings
        player.GetComponent<PlayerInputHolder>().heldInput = input;

        AbilityNetworking abilityNetworking = player.GetComponent<AbilityNetworking>();

        //add abilities
        foreach (AbilityId ability in playerData.abilities)
        {
            InstantiateAbility(ability, player.transform, abilityNetworking);
        }

        foreach (AbilityId ability in playerData.firstAbilities)
        {
            GameObject instantiatedAbility = InstantiateAbility(ability, player.transform, abilityNetworking);
            Rebind(instantiatedAbility, AbilityKeybinding.ABILITY1);
        }

        foreach (AbilityId ability in playerData.secondAbilities)
        {
            GameObject instantiatedAbility = InstantiateAbility(ability, player.transform, abilityNetworking);
            Rebind(instantiatedAbility, AbilityKeybinding.ABILITY2);
        }
    }
}
