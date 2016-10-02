using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Component designed to receive events from IAbilityActivations, and perform actions. There can and should be many of these making up a single ability.
/// </summary>
[RequireComponent(typeof(NetworkedAbilityActivation))]
public abstract class AbstractAbilityAction : MonoBehaviour {

    protected PhotonView view;
    protected NetworkedAbilityActivation networkedActivation;

    protected virtual void Awake()
    {
        view = GetComponentInParent<PhotonView>();
        networkedActivation = GetComponent<NetworkedAbilityActivation>();
    }

	protected virtual void Start () {
        foreach (IAbilityActivation activation in GetComponentsInParent<IAbilityActivation>())
        {
            activation.abilityActivationEvent += activation_abilityActivationEvent;
        }
	}

    //delegate for IAbilityActivation.abilityActivationEvent
    public void activation_abilityActivationEvent()
    {
        Activate();
        ActivateRemote();
    }

    public abstract void Activate();

    /// <summary>
    /// In this function, do all code related to activating the ability on other clients. Can be empty.
    /// </summary>
    public abstract void ActivateRemote();
}
