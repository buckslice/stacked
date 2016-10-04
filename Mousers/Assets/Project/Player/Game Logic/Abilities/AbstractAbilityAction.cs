using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Component designed to receive events from IAbilityActivations, and perform actions. There can and should be many of these making up a single ability.
/// </summary>
[RequireComponent(typeof(AbilityActivation))]
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

        foreach (IAbilityActivationWithData activation in GetComponentsInParent<IAbilityActivationWithData>())
        {
            activation.abilityActivationWithDataEvent += activation_abilityActivationWithDataEvent;
        }
	}

    //delegate for IAbilityActivationWithData.abilityActivationWithDataEvent
    void activation_abilityActivationWithDataEvent(object data)
    {
        ActivateWithData(data);
    }

    //delegate for IAbilityActivation.abilityActivationEvent
    public void activation_abilityActivationEvent()
    {
        Activate();
        ActivateRemote();
    }

    public abstract void Activate();

    /// <summary>
    /// Can just call activate, if no data is used. The only intended use is for networking, where local data is not available.
    /// </summary>
    public abstract void ActivateWithData(object data);

    /// <summary>
    /// In this function, do all code related to activating the ability on other clients. Can be empty.
    /// </summary>
    public abstract void ActivateRemote();
}
