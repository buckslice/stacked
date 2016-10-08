﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Filters triggers to activate abilities. Acts with reference to a target.
/// </summary>
public class TargetedAbilityActivation : MonoBehaviour, IAbilityActivation, IAbilityConstrained {
    /// <summary>
    /// List of abilityActions to activate with this ability, includes Constraints. Order must be the same on all clients.
    /// </summary>
    [SerializeField]
    protected TargetedAbilityAction[] abilityActions;
    public TargetedAbilityAction[] AbilityActions { get { return abilityActions; } }

    protected List<TargetedAbilityConstraint> constraints = new List<TargetedAbilityConstraint>();

    public void AddConstraint(TargetedAbilityConstraint toAdd) { constraints.Add(toAdd); }
    public bool RemoveConstraint(TargetedAbilityConstraint toRemove) { return constraints.Remove(toRemove); }

    /// <summary>
    /// For covariance.
    /// </summary>
    public void AddConstraint(UntargetedAbilityConstraint toAdd) { AddConstraint((TargetedAbilityConstraint)toAdd); }
    public bool RemoveConstraint(UntargetedAbilityConstraint toRemove) { return RemoveConstraint((TargetedAbilityConstraint)toRemove); }

    IActivationNetworking abilityNetwork;

    /// <summary>
    /// Constructor-like method for initialization.
    /// </summary>
    /// <param name="abilityNetwork"></param>
    public void Initialize(IActivationNetworking abilityNetwork) {
        this.abilityNetwork = abilityNetwork;
    }

    public void Start() {
        foreach (TargetedAbilityTrigger trigger in GetComponentsInParent<TargetedAbilityTrigger>()) {
            trigger.targetedAbilityTriggerEvent += trigger_targetedAbilityTriggerEvent;
        }
    }

    void trigger_targetedAbilityTriggerEvent(GameObject target) {
        foreach (TargetedAbilityConstraint constraint in constraints) {
            if (!constraint.isAbilityActivatible(target)) {
                //cannot activate, do nothing
                return;
            }
        }

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(true, null);

        bool send = false;
        foreach (TargetedAbilityAction abilityAction in abilityActions) {
            send |= abilityAction.Activate(target, stream);
            Assert.IsTrue(send || (stream.Count == 0), string.Format("Data written to stream but not flagged to be sent. {0}", abilityAction));
        }
        //TODO: network the target
        //abilityNetwork.ActivateRemote(this, stream.ToArray());
    }

    //TODO: network the target
    /*
    public void Activate(object[] incomingData) {

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(false, incomingData);
        foreach (TargetedAbilityAction abilityAction in abilityActions) {
            abilityAction.Activate(target, stream);
        }
    }
     * */
}
