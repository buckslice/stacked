using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Used to interface with AbilityNetworking scripts.
/// </summary>
public interface IAbilityActivation {
    void Initialize(AbstractActivationNetworking abilityNetwork);
    void Activate(object[] data, PhotonMessageInfo info);

}

/// <summary>
/// Used to interface with the UI.
/// </summary>
public interface IAbilityStatus {

    /// <summary>
    /// Time until the ability is ready, normalized to the range [0, 1]. Only includes time-related constraints
    /// </summary>
    /// <returns></returns>
    float cooldownProgress();

    /// <summary>
    /// Is the cooldown ready. Doesn't include targetable constraints.
    /// </summary>
    /// <returns></returns>
    bool Ready();
}

/// <summary>
/// Filters triggers to activate abilities. Has no target.
/// </summary>
public class AbilityActivation : MonoBehaviour, IAbilityActivation, IAbilityConstrained, IAbilityStatus {
    /// <summary>
    /// List of abilityActions to activate with this ability, includes Constraints. Order must be the same on all clients.
    /// </summary>
    [SerializeField]
    protected AbstractAbilityAction[] abilityActions;
    public AbstractAbilityAction[] AbilityActions { get { return abilityActions; } }

    protected List<UntargetedAbilityConstraint> constraints = new List<UntargetedAbilityConstraint>();
    protected List<ICooldownConstraint> cooldownConstraints = new List<ICooldownConstraint>();

    public void AddConstraint(UntargetedAbilityConstraint toAdd) {
        if (toAdd is ICooldownConstraint) {
            cooldownConstraints.Add((ICooldownConstraint)toAdd);
        }
        constraints.Add(toAdd); 
    }
    public bool RemoveConstraint(UntargetedAbilityConstraint toRemove) {
        if (toRemove is ICooldownConstraint) {
            cooldownConstraints.Remove((ICooldownConstraint)toRemove);
        }
        return constraints.Remove(toRemove); 
    }

    AbstractActivationNetworking abilityNetwork;

    /// <summary>
    /// Constructor-like method for initialization.
    /// </summary>
    /// <param name="abilityNetwork"></param>
    public void Initialize(AbstractActivationNetworking abilityNetwork) {
        this.abilityNetwork = abilityNetwork;
    }

    public void Start() {
        if (abilityNetwork == null) {
            Debug.LogErrorFormat("Ability {0} has not been initialized.", this);
            if (transform.parent == null) {
                Destroy(this.gameObject);
            }
        }

        Assert.IsNull(GetComponent<TargetedAbilityActivation>());

        foreach (IUntargetedAbilityTrigger trigger in GetComponentsInParent<IUntargetedAbilityTrigger>()) {
            trigger.abilityTriggerEvent += trigger_abilityTriggerEvent;
        }
    }

    public bool Ready() {
        foreach (UntargetedAbilityConstraint constraint in constraints) {
            if (!abilityNetwork.ActivationEnabled) {
                //activation disabled
                return false;
            }

            if (constraint.enabled && !constraint.isAbilityActivatible()) {
                //cannot activate
                return false;
            }
        }
        return true;
    }

    public float cooldownProgress() {
        float cooldownRemaining = 0;
        foreach (ICooldownConstraint cooldown in cooldownConstraints) {
            cooldownRemaining = Mathf.Max(cooldownRemaining, cooldown.cooldownProgress());
        }
        return cooldownRemaining;
    }

    /// <summary>
    /// Event subscribed to all triggers of this ability.
    /// </summary>
    void trigger_abilityTriggerEvent() {
        if (!Ready()) {
            return;
        }

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(true, null);

        bool send = false;
        foreach (AbstractAbilityAction abilityAction in abilityActions) {
            send |= abilityAction.Activate(stream);
            //Assert.IsTrue(send || (stream.Count == 0), string.Format("Data written to stream but not flagged to be sent. {0}", abilityAction));
            //ConditionalActivations will always write data, even if it doesn't need to be sent.
        }

        if (send) {
            abilityNetwork.ActivateRemote(this, stream.ToArray());
        }
    }

    public void Activate(object[] incomingData, PhotonMessageInfo info) {

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(false, incomingData);
        foreach (AbstractAbilityAction abilityAction in abilityActions) {
            abilityAction.Activate(stream);
        }
    }
}