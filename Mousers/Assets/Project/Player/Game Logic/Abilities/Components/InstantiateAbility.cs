using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An action which spawns a pooled object
/// </summary>
public class InstantiateAbility : AbstractAbilityAction {

    [SerializeField]
    protected string prefabName;

    public override void Activate()
    {
        PhotonNetwork.Instantiate(prefabName, transform.position, transform.rotation, 0);
    }

    public override void ActivateWithRemoteData(object data)
    {
        Activate();
    }

    public override void ActivateRemote()
    {
    }
}
