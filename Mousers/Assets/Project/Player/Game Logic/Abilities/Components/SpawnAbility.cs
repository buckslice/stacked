using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An action which spawns a pooled object
/// </summary>
public class SpawnAbility : AbstractAbilityAction {

    [SerializeField]
    protected string prefabName;

    /// <summary>
    /// Use PhotonNetwork.Instantiate if yes, else use SimplePool.Spawn.
    /// </summary>
    [SerializeField]
    protected bool networkedPrefab;

    public override void Activate()
    {
        PhotonNetwork.Instantiate(prefabName, transform.position, transform.rotation, 0);
    }

    public override void ActivateWithData(object data)
    {
        Activate();
    }

    public override void ActivateRemote()
    {
    }
}
