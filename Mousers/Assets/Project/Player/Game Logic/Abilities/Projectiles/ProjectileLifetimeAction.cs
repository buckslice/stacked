using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Superclass for any component which needs to deal with the lifetime of a projectile.
/// </summary>
public abstract class ProjectileLifetimeAction : MonoBehaviour, ISpawnable {

	PhotonView view;

    protected void Awake()
    {
        view = GetComponentInParent<PhotonView>();

        if (view) {
            //networked, so no pooling.
            OnProjectileCreated();
        }
    }

    public void Spawn()
    {
        OnProjectileCreated();
    }

    protected void OnDestroy() {
        OnProjectileDestroyed();
    }

    public void Despawn() {
        OnProjectileDestroyed();
    }

    /// <summary>
    /// Called when the projectile is created.
    /// </summary>
    protected abstract void OnProjectileCreated();

    /// <summary>
    /// Called when a projectile is destroyed.
    /// </summary>
    protected virtual void OnProjectileDestroyed() { }

    /// <summary>
    /// Kills the projectile.
    /// </summary>
    protected void DestroyProjectile() {
        if (view != null && view.isMine) {
            PhotonNetwork.Destroy(view);
        } else {
            SimplePool.Despawn(transform.root.gameObject);
        }
    }
}
