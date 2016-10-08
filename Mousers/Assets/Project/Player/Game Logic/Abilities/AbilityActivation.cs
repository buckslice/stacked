using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Used to interface with AbilityNetworking scripts.
/// </summary>
public interface IAbilityActivation {
    void Initialize(IActivationNetworking abilityNetwork);
}

/// <summary>
/// Filters triggers to activate abilities. Has no target.
/// </summary>
public class AbilityActivation : MonoBehaviour, IAbilityActivation, IAbilityConstrained {
    /// <summary>
    /// List of abilityActions to activate with this ability, includes Constraints. Order must be the same on all clients.
    /// </summary>
    [SerializeField]
    protected AbstractAbilityAction[] abilityActions;
    public AbstractAbilityAction[] AbilityActions { get { return abilityActions; } }

    protected List<UntargetedAbilityConstraint> constraints = new List<UntargetedAbilityConstraint>();

    public void AddConstraint(UntargetedAbilityConstraint toAdd) { constraints.Add(toAdd); }
    public bool RemoveConstraint(UntargetedAbilityConstraint toRemove) { return constraints.Remove(toRemove); }

    IActivationNetworking abilityNetwork;

    /// <summary>
    /// Constructor-like method for initialization.
    /// </summary>
    /// <param name="abilityNetwork"></param>
    public void Initialize(IActivationNetworking abilityNetwork) {
        this.abilityNetwork = abilityNetwork;
    }

    public void Start() {
        foreach (IUntargetedAbilityTrigger trigger in GetComponentsInParent<IUntargetedAbilityTrigger>()) {
            trigger.abilityTriggerEvent += trigger_abilityTriggerEvent;
        }
    }

    void trigger_abilityTriggerEvent() {
        foreach (UntargetedAbilityConstraint constraint in constraints) {
            if (!constraint.isAbilityActivatible()) {
                //cannot activate, do nothing
                return;
            }
        }

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(true, null);

        bool send = false;
        foreach (AbstractAbilityAction abilityAction in abilityActions) {
            send |= abilityAction.Activate(stream);
            Assert.IsTrue(send || (stream.Count == 0), string.Format("Data written to stream but not flagged to be sent. {0}", abilityAction));
        }
        abilityNetwork.ActivateRemote(this, stream.ToArray());
    }

    public void Activate(object[] incomingData) {

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(false, incomingData);
        foreach (AbstractAbilityAction abilityAction in abilityActions) {
            abilityAction.Activate(stream);
        }
    }
}