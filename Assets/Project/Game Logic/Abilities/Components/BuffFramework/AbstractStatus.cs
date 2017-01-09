using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An status is any gameplay status effect (slow, stun, silence, etc.) where multiple applications have custom behaviour.
/// </summary>
public abstract class AbstractStatus : ProjectileLifetimeAction {

    protected override void Awake() {
        base.Awake();

        //only one status per attachment, since the attachment is often deleted when applications overlap.
        Assert.IsTrue(transform.root.GetComponentsInChildren<AbstractStatus>().Length == 1); 
    }

    /// <summary>
    /// Attaches the effect to an object, and handles duplicates.
    /// </summary>
    /// <param name="target"></param>
    /// <returns>True if actually attached, False otherwise.</returns>
    public bool Attach(Transform target) {
        bool shouldAttach = handleDuplicates(target);
        if(shouldAttach) {
            transform.SetParent(target);
            transform.Reset();
            OnProjectileAttached();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Called immediately after the projectile has been attached to its correct position.
    /// TODO: maybe turn this into an event?
    /// </summary>
    protected virtual void OnProjectileAttached() { }

    /// <summary>
    /// Detect and resolve duplicates of this effect.
    /// </summary>
    /// <param name="target">The object we will be attached to.</param>
    /// <returns>False if the object should not be attached, True otherwise.</returns>
    protected abstract bool handleDuplicates(Transform target);

    /// <summary>
    /// Implementation of handleDuplicates which uses the current instance to "refresh" the duration of the status on the target.
    /// Refreshes by clobbering all other instances.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <returns></returns>
    protected static bool refreshDuplicates<T>(T self, Transform target) where T : AbstractStatus {
        foreach(T duplicate in target.root.GetComponentsInChildren<T>()) {
            if (duplicate != self && duplicate.isActiveAndEnabled) {
                duplicate.DeactivateProjectile();
            }
        }
        return true;
    }
}
