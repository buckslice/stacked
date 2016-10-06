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

    public override void Activate() {
        SimplePool.Spawn(prefab, transform.position, transform.rotation);
    }

    public override void ActivateWithRemoteData(object data) {
        Activate();
    }

    public override void ActivateRemote() {
    }
}
