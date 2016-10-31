using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Superclass for any component which needs to deal with the lifetime of a projectile.
/// </summary>
public abstract class ProjectileLifetimeAction : MonoBehaviour, ISpawnable, IDespawnable {

    PhotonView view;
    ProjectileDestruction root;

    protected virtual void Awake() {
        view = GetComponentInParent<PhotonView>();

        if (view) {
            //networked, so no pooling.
            OnProjectileCreated();
        }
    }

    protected void Start() {
        root = getRoot(this.transform);
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
    public virtual void OnProjectileDeactivated() { }

    public void DeactivateProjectile() {
        root.StartDestroySequence();
    }

    public static ProjectileDestruction getRoot(Transform target) {
        ProjectileDestruction projectileRoot = target.GetComponentInParent<ProjectileDestruction>();
        if (projectileRoot == null) {
            Debug.LogWarning("Projectile does not have a ProjectileDestruction script; adding one to the root transform.", target);
            projectileRoot = target.root.AddComponent<ProjectileDestruction>();
        }
        return projectileRoot;
    }
}
