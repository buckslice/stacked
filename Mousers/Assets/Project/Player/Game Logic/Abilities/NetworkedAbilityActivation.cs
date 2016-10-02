using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Networking component placed on an ability. 
/// </summary>
public class NetworkedAbilityActivation : MonoBehaviour, IAbilityActivation {

    public event ActivateAbility abilityActivationEvent = delegate { };

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
        abilityActivationEvent();
    }

    /// <summary>
    /// Tells other clients to activate this ability on their end.
    /// </summary>
    public void ActivateRemote()
    {
        abilityNetwork.ActivateRemote(this);
    }
}
