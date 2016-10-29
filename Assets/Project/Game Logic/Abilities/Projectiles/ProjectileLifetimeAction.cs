using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Superclass for any component which needs to deal with the lifetime of a projectile.
/// </summary>
public abstract class ProjectileLifetimeAction : MonoBehaviour, ISpawnable, IDespawnable {

    PhotonView view;

    protected virtual void Awake() {
        view = GetComponentInParent<PhotonView>();

        if (view) {
            //networked, so no pooling.
            OnProjectileCreated();
        }
    }

    public void Spawn() {
        OnProjectileCreated();
    }

    protected void OnApplicationQuit() {
        this.enabled = false;
    }

    protected void OnDestroy() {
        if (enabled) {
            OnProjectileDestroyed();
        }
    }

    public void Despawn() {
        // need to figure out better way to store components that should be
        // enabled and disabled during pooling
        MeshRenderer rend = transform.root.GetComponentInChildren<MeshRenderer>();
        if (rend) {
            rend.enabled = true;
        }
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
    /// Called when the projectile destruction sequence starts
    /// </summary>
    protected virtual void OnDestructionStart() { }

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

    /// <summary>
    /// Stops the projectiles particle system first then kills the projectile after particles are finished
    /// </summary>
    protected void DestroySequence() {
        ParticleSystem ps = transform.root.GetComponentInChildren<ParticleSystem>();
        float duration = 0.0f;
        if (ps) {
            ps.Stop();
            duration = ps.startLifetime;
        }
        MeshRenderer rend = transform.root.GetComponentInChildren<MeshRenderer>();
        if (rend) {
            rend.enabled = false;
        }

        OnDestructionStart();
        Callback.FireAndForget(DestroyProjectile, duration, this);
    }

    public static void DestroyProjectile(Transform root) {
        ProjectileLifetimeAction plta = root.GetComponent<ProjectileLifetimeAction>();
        if (plta) {
            plta.DestroySequence();
        }
    }
}
