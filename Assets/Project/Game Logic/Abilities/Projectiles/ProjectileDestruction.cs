using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Single script to handle destruction of a projectile, instead of every ProjectileLifetimeAction doing it in parallel.
/// Also represents the root of a projectile, if the projectile has been childed to something else.
/// </summary>
public class ProjectileDestruction : MonoBehaviour {

    /// <summary>
    /// Can be null.
    /// </summary>
    PhotonView view;

	void Start () {
        view = GetComponent<PhotonView>();
	}

    public void StartDestroySequence() {
        //TODO: refactor into child scripts, which this script references and polls.
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        float duration = 0.0f;
        if (ps) {
            ps.Stop();
            duration = ps.startLifetime;
        }
        MeshRenderer rend = GetComponentInChildren<MeshRenderer>();
        if (rend) {
            rend.enabled = false;
        }

        Callback.FireAndForget(DestroyProjectile, duration, this);
    }

    /// <summary>
    /// Kills the projectile.
    /// </summary>
    protected void DestroyProjectile() {
        MeshRenderer rend = transform.root.GetComponentInChildren<MeshRenderer>();
        if (rend) {
            rend.enabled = true;
        }

        if (view != null && view.isMine) {
            PhotonNetwork.Destroy(view);
        } else {
            SimplePool.Despawn(this.gameObject);
        }
    }
}
