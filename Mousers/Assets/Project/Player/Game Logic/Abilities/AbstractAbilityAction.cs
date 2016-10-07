using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Component designed to receive events from IAbilityActivations, and perform actions. There can and should be many of these making up a single ability.
/// </summary>
[RequireComponent(typeof(AbilityActivation))]
public abstract class AbstractAbilityAction : MonoBehaviour {

    protected AbilityActivation abilityActivation;

    protected virtual void Awake()
    {
        abilityActivation = GetComponent<AbilityActivation>();
        if (!abilityActivation.AbilityActions.Contains(this)) {
            Debug.LogError("Ability's AbilityActivation does not contain all of the ability's AbilityActions.", this);
        }
    }

	protected virtual void Start () {
	}

    /// <summary>
    /// Called when the ability is activated. The photonStream is to send or recieve data from the network. You can use stream.isWriting / stream.isReading to figure out which.
    /// Returns true if an activation needs to be sent over the network, false otherwise.
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public abstract bool Activate(PhotonStream stream);
}
