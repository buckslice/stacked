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

    /// <summary>
    /// The index of an object in this list is its spawnedObjectID.
    /// </summary>
    List<IActivationNetworking> activeObjects = new List<IActivationNetworking>();

    /// <summary>
    /// Secondary data structure to look up the index (and thus the spawnedObjectID) of an element in activeObjects
    /// </summary>
    Dictionary<IActivationNetworking, int> activeObjectIndices = new Dictionary<IActivationNetworking, int>();

    public void Initialize(AbstractActivationNetworking abilityNetwork) {
        this.abilityNetwork = abilityNetwork;
        view = abilityNetwork.GetComponent<PhotonView>();
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

        Assert.IsNull(activeObjects[spawnedObjectID]);
        IActivationNetworking spawnedObject = SimplePool.Spawn(prefab, transform.position, transform.rotation).GetComponent<IActivationNetworking>();
        spawnedObject.Initialize(this, view);
        activeObjects.Insert(spawnedObjectID, spawnedObject);
        activeObjectIndices[spawnedObject] = spawnedObjectID;
        //other initialization using the stream?
        return true;
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
        activeObjects[spawnedObjectID].NetworkedActivationRPC(unjoinedData, info);
    }

    public void ActivateRemote(IActivationNetworking requester, object[] outboundData) {
        
    }

    void ActivateRemote(byte spawnedObjectID, object[] outboundData) {
        object[] joinedData = new object[outboundData.Length + 1];
        joinedData[0] = spawnedObjectID;
        outboundData.CopyTo(joinedData, 1);
        abilityNetwork.ActivateRemote(this, joinedData);
    }

    public byte getSpawnedObjectID(IActivationNetworking spawnedObject) {
        if (!activeObjectIndices.ContainsKey(spawnedObject)) {
            Debug.LogErrorFormat(this, "{0} is not present as a spawned object of {1}", spawnedObject.ToString(), this.ToString());
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
        while (activeObjects[i] != null && i < activeObjects.Count) {
            i++;
        }

        Assert.IsTrue(i <= 255, "Number of objects outside of byte range.");

        return i;
    }
}
