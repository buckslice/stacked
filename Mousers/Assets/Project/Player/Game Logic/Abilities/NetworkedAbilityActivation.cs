using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Networking component placed on an ability. 
/// </summary>
public class NetworkedAbilityActivation : MonoBehaviour, IAbilityActivation, IAbilityActivationWithData {

    public event ActivateAbility abilityActivationEvent = delegate { };

    public event ActivateAbilityWithRemoteData abilityActivationWithDataEvent = delegate { };

    AbilityNetworking abilityNetwork;

    /// <summary>
    /// Constructor-like method for initialization.
    /// </summary>
    /// <param name="abilityNetwork"></param>
    public void Initialize(AbilityNetworking abilityNetwork)
    {
        this.abilityNetwork = abilityNetwork;
    }

    /// <summary>
    /// Activates the ability on this local client.
    /// </summary>
    public void ActivateLocal()
    {
        abilityActivationWithDataEvent(null);
    }

    /// <summary>
    /// Activates the ability on this local client. Used for networking.
    /// </summary>
    public void ActivateLocalWithData(object data)
    {
        abilityActivationWithDataEvent(data);
    }

    /// <summary>
    /// Tells other clients to activate this ability on their end.
    /// </summary>
    public void ActivateRemote()
    {
        abilityNetwork.ActivateRemote(this);
    }

    /// <summary>
    /// Tells other clients to activate this ability on their end.
    /// </summary>
    public void ActivateRemoteWithData(object data)
    {
        abilityNetwork.ActivateRemoteWithData(this, data);
    }
}
