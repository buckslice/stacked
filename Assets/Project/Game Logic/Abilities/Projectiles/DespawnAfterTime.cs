using UnityEngine;
using System.Collections;

public class DespawnAfterTime : ProjectileLifetimeAction {

    [SerializeField]
    protected float timeToLive = 1;

    float dieTime;

    protected override void OnProjectileCreated() {
        dieTime = Time.time + timeToLive;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > dieTime) {
            SimplePool.Despawn(transform.root.gameObject);
        }
    }
}
