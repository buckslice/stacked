using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An action which spawns a pooled object, and functions as a relay for all spawned object abilities
/// </summary>
public class SpawnAbility : AbstractAbilityAction, IAbilityActivation, IAbilityRelay {

    [SerializeField]
    protected GameObject prefab;

    [SerializeField]
    protected bool networkPosition = true;

    [SerializeField]
    protected bool networkRotation = true;

    PhotonView view;
    AbstractActivationNetworking abilityNetwork;
    IDamageHolder trackerReference;

    /// <summary>
    /// The index of an object in this list is its spawnedObjectID.
    /// </summary>
    List<SpawnedObjectTracker> activeObjects = new List<SpawnedObjectTracker>();

    /// <summary>
    /// Secondary data structure to look up the index (and thus the spawnedObjectID) of an element in activeObjects
    /// </summary>
    Dictionary<IActivationNetworking, int> activeObjectIndices = new Dictionary<IActivationNetworking, int>();

    public void Initialize(AbstractActivationNetworking abilityNetwork) {
        this.abilityNetwork = abilityNetwork;
        view = abilityNetwork.GetComponent<PhotonView>();
    }

    protected override void Start() {
        base.Start();
        trackerReference = GetComponentInParent<IDamageHolder>();
        Assert.IsNotNull(trackerReference);
        Assert.IsNotNull(trackerReference.GetRootDamageTracker());
    }

    /// <summary>
    /// Normal Ability activation
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public override bool Activate(PhotonStream stream) {
        byte spawnedObjectID;
        Vector3 position;
        float rotation;

        if (stream.isWriting) {
            spawnedObjectID = (byte)firstOpenIndex();
            stream.SendNext(spawnedObjectID);

            position = transform.position;
            if (networkPosition) {
                stream.SendNext(position);
            }

            rotation = transform.eulerAngles.y;
            if (networkRotation) {
                stream.SendNext(rotation);
            }
        } else {
            spawnedObjectID = (byte)stream.ReceiveNext();

            position = networkPosition ? (Vector3)stream.ReceiveNext() : transform.position;

            rotation = networkRotation ? (float)stream.ReceiveNext() : transform.eulerAngles.y;
        }

        Assert.IsTrue(spawnedObjectID == activeObjects.Count || activeObjects[spawnedObjectID] == null);
        GameObject spawnedObject = SimplePool.Spawn(prefab, transform.position, transform.rotation);
        IActivationNetworking spawnedNetworking = spawnedObject.GetComponent<IActivationNetworking>();
        Assert.IsNotNull(spawnedNetworking);
        spawnedNetworking.Initialize(this, view);

        SpawnedObjectTracker objectTracker = spawnedObject.GetComponent<SpawnedObjectTracker>();
        if (objectTracker == null) {
            objectTracker = spawnedObject.AddComponent<SpawnedObjectTracker>();
        }
        objectTracker.Initialize(trackerReference);
        objectTracker.onProjectileDestroyed += objectTracker_onProjectileDestroyed;

        activeObjects.Insert(spawnedObjectID, objectTracker);
        activeObjectIndices[spawnedNetworking] = spawnedObjectID;
        //other initialization using the stream?
        return true;
    }

    void objectTracker_onProjectileDestroyed(SpawnedObjectTracker self) {
        self.onProjectileDestroyed -= objectTracker_onProjectileDestroyed;
        byte spawnedObjectID = getSpawnedObjectID(self.ActivationNetworking);
        if (spawnedObjectID != 255) { //error value
            activeObjects[spawnedObjectID] = null;
        }
        
        activeObjectIndices.Remove(self.ActivationNetworking);
    }

    /// <summary>
    /// Tunneled ability activation
    /// </summary>
    /// <param name="data"></param>
    public void Activate(object[] data, PhotonMessageInfo info) {
        byte spawnedObjectID = (byte)data[0];
        object[] unjoinedData = new object[data.Length - 1];
        System.Array.Copy(data, 1, unjoinedData, 0, unjoinedData.Length);
        Assert.IsNotNull(activeObjects[spawnedObjectID]);
        activeObjects[spawnedObjectID].ActivationNetworking.NetworkedActivationRPC(unjoinedData, info);
    }

    public void ActivateRemote(IActivationNetworking requester, object[] outboundData) {
        byte spawnedObjectID = getSpawnedObjectID(requester);
        ActivateRemote(spawnedObjectID, outboundData);
    }

    void ActivateRemote(byte spawnedObjectID, object[] outboundData) {
        object[] joinedData = new object[outboundData.Length + 1];
        joinedData[0] = spawnedObjectID;
        outboundData.CopyTo(joinedData, 1);
        abilityNetwork.ActivateRemote(this, joinedData);
    }

    public byte getSpawnedObjectID(IActivationNetworking spawnedObject) {
        if (((MonoBehaviour)spawnedObject) == null) {
            //should not be null as an input
            return byte.MaxValue;
        }

        if (!activeObjectIndices.ContainsKey(spawnedObject)) {
            if (this != null) {//if we have not been despawned

                //this line often gets run when the game ends. I'm still trying to suppress that from happening.
                Debug.LogErrorFormat(this, "{0} is not present as a spawned object of {1}. Ignore this if the scene is being changed or ended.", spawnedObject.ToString(), this.ToString());
            }
            return byte.MaxValue;
        } else {
            int result = activeObjectIndices[spawnedObject];
            return (byte)result;
        }
    }

    /// <summary>
    /// locates and returns the first null value in activeObjects
    /// </summary>
    /// <returns></returns>
    int firstOpenIndex() {
        int i = 0;
        while (i < activeObjects.Count && activeObjects[i] != null) {
            i++;
        }

        Assert.IsTrue(i < 255, "Number of objects outside of byte range.");

        return i;
    }
}
