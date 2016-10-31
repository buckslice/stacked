using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Creates a TimeToLive on projectiles
/// </summary>
public class ProjectileTTL : ProjectileLifetimeAction {

    [SerializeField]
    protected MultiplierFloatStat duration = new MultiplierFloatStat(10);

    protected override void OnProjectileCreated() {
        Callback.FireAndForget(() => DeactivateProjectile(this.transform), duration, this);
    }
}
