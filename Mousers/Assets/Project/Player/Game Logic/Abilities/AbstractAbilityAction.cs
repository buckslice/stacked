using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(TargetedAbilityActivation))]
public abstract class TargetedAbilityAction : MonoBehaviour {
    protected AbilityActivation abilityActivation;

    protected virtual void Awake() {
        abilityActivation = GetComponent<AbilityActivation>();
        if (!abilityActivation.AbilityActions.Contains(this)) {
            Debug.LogError("Ability's AbilityActivation does not contain all of the ability's AbilityActions.", this);
        }
    }

    protected virtual void Start() {
    }

    /// <summary>
    /// Called when the ability is activated. The photonStream is to send or recieve data from the network. You can use stream.isWriting / stream.isReading to figure out which.
    /// Returns true if an activation needs to be sent over the network, false otherwise.
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public abstract bool Activate(GameObject context, PhotonStream stream);
}

/// <summary>
/// Component designed to receive events from IAbilityActivations, and perform actions. There can and should be many of these making up a single ability. This one can activate without a target.
/// </summary>
public abstract class AbstractAbilityAction : TargetedAbilityAction {

    public sealed override bool Activate(GameObject context, PhotonStream stream) {
        return Activate(stream);
    }

    /// <summary>
    /// Called when the ability is activated. The photonStream is to send or recieve data from the network. You can use stream.isWriting / stream.isReading to figure out which.
    /// Returns true if an activation needs to be sent over the network, false otherwise.
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public abstract bool Activate(PhotonStream stream);
}
