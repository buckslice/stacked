using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An action which spawns a pooled object
/// </summary>
public class SpawnAbility : AbstractAbilityAction {

    [SerializeField]
    protected GameObject prefab;

    public override bool Activate(PhotonStream stream) {
        SimplePool.Spawn(prefab, transform.position, transform.rotation);
        return true;
    }
}
