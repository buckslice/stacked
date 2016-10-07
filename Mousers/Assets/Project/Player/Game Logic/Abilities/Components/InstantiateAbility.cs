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

    public override bool Activate(PhotonStream stream)
    {
        PhotonNetwork.Instantiate(prefabName, transform.position, transform.rotation, 0);
        return false;
    }
}
