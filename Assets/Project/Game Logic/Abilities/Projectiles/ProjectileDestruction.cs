using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface representing an object that requires time to deactivate. Should ideally be used for visuals effects and not core gameplay logic.
/// </summary>
public interface IProjectileDeactivation {
    float getDeactivationTime();
}

/// <summary>
/// Single script to handle destruction of a projectile, instead of every ProjectileLifetimeAction doing it in parallel.
/// Also represents the root of a projectile, if the projectile has been childed to something else.
/// </summary>
public class ProjectileDestruction : MonoBehaviour {

    /// <summary>
    /// Can be null.
    /// </summary>
    PhotonView view;
    ProjectileLifetimeAction[] pltas;
    IProjectileDeactivation[] projectileDeactivationTimers;

	void Start () {
        view = GetComponent<PhotonView>();
        pltas = GetComponentsInChildren<ProjectileLifetimeAction>();
        projectileDeactivationTimers = GetComponentsInChildren<IProjectileDeactivation>();
	}

    public void StartDestroySequence() {
        foreach (ProjectileLifetimeAction plta in pltas) {
            plta.OnProjectileDeactivated();
        }

        if (projectileDeactivationTimers.Length == 0) {
            //No timers, destroy projectile immediately
            DestroyProjectile();

        } else {
            //Timer required
            float duration = 0.0f;
            foreach (IProjectileDeactivation projectileDeactivationTimer in projectileDeactivationTimers) {
                float timeRequired = projectileDeactivationTimer.getDeactivationTime();
                if (timeRequired > duration) {
                    duration = timeRequired;
                }
            }

            Callback.FireAndForget(DestroyProjectile, duration, this);
        }
    }

    /// <summary>
    /// Kills the projectile.
    /// </summary>
    protected void DestroyProjectile() {

        if (view != null && view.isMine) {
            PhotonNetwork.Destroy(view);
        } else {
            SimplePool.Despawn(this.gameObject);
        }
    }
}
