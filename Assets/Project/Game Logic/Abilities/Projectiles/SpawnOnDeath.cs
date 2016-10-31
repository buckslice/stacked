using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class SpawnOnDeath : ProjectileLifetimeAction {

    [SerializeField]
    protected GameObject prefab;

    protected override void OnProjectileCreated() { }

    protected override void OnProjectileDeactivated() {
        base.OnProjectileDeactivated();
        GameObject spawnedPrefab = SimplePool.Spawn(prefab);
        spawnedPrefab.transform.Reset();
        spawnedPrefab.transform.position = this.transform.position;
        spawnedPrefab.transform.rotation = this.transform.rotation;
    }
}
