using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Component designed to receive events from IAbilityActivations, and perform actions. There can and should be many of these making up a single ability.
/// </summary>
[RequireComponent(typeof(PhotonView))]
public abstract class AbstractAbilityAction : MonoBehaviour {

    /// <summary>
    /// name of our RPC method. TODO : might want to move this to IAbilityActivation scripts?
    /// </summary>
    protected const string activate = "RPCActivate";

    protected PhotonView view;

    protected virtual void Awake()
    {
        view = GetComponent<PhotonView>();
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
        //view.RPC(activate, PhotonTargets.All);
        //skip the networking RPC for now

        Activate();
    }

    [PunRPC]
    public void RPCActivate()
    {
        Activate();
    }

    public abstract void Activate();
}
