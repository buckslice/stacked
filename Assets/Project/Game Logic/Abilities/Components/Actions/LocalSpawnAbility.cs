using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Spawns an unnetworked object (for VFX, SFX, etc.)
/// </summary>
public class LocalSpawnAbility : AbstractAbilityAction {

    [SerializeField]
    protected GameObject prefab;

    public override bool Activate(PhotonStream stream) {
        GameObject spawnedPrefab = SimplePool.Spawn(prefab);
        spawnedPrefab.transform.Reset();
        spawnedPrefab.transform.position = this.transform.position;
        spawnedPrefab.transform.rotation = this.transform.rotation;
        return true;
    }

}
