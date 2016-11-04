using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Filters triggers to activate abilities. Acts with reference to a target.
/// </summary>
public class TargetedAbilityActivation : MonoBehaviour, IAbilityActivation, IAbilityConstrained, IAbilityStatus {
    /// <summary>
    /// List of abilityActions to activate with this ability, includes Constraints. Order must be the same on all clients.
    /// </summary>
    [SerializeField]
    protected TargetedAbilityAction[] abilityActions;
    public TargetedAbilityAction[] AbilityActions { get { return abilityActions; } }

    protected List<ITargetedAbilityConstraint> targetedConstraints = new List<ITargetedAbilityConstraint>();
    protected List<UntargetedAbilityConstraint> untargetedConstraints = new List<UntargetedAbilityConstraint>();
    protected List<ICooldownConstraint> cooldownConstraints = new List<ICooldownConstraint>();

    public void AddConstraint(ITargetedAbilityConstraint toAdd) {
        if (toAdd is ICooldownConstraint) {
            cooldownConstraints.Add((ICooldownConstraint)toAdd);
        }
        targetedConstraints.Add(toAdd);
    }
    public bool RemoveConstraint(ITargetedAbilityConstraint toRemove) {
        if (toRemove is ICooldownConstraint) {
            cooldownConstraints.Remove((ICooldownConstraint)toRemove);
        }
        return targetedConstraints.Remove(toRemove);
    }

    /// <summary>
    /// For covariance.
    /// </summary>
    public void AddConstraint(UntargetedAbilityConstraint toAdd) {
        if (toAdd is ICooldownConstraint) {
            cooldownConstraints.Add((ICooldownConstraint)toAdd);
        }
        untargetedConstraints.Add(toAdd);
    }
    public bool RemoveConstraint(UntargetedAbilityConstraint toRemove) {
        if (toRemove is ICooldownConstraint) {
            cooldownConstraints.Remove((ICooldownConstraint)toRemove);
        }
        return untargetedConstraints.Remove(toRemove);
    }

    AbstractActivationNetworking abilityNetwork;

    /// <summary>
    /// Constructor-like method for initialization.
    /// </summary>
    /// <param name="abilityNetwork"></param>
    public void Initialize(AbstractActivationNetworking abilityNetwork) {
        this.abilityNetwork = abilityNetwork;
        Assert.IsNotNull(this.abilityNetwork);
    }

    public void Start() {
        if (transform.parent == null) {
            Debug.LogErrorFormat("Ability {0} has no parent; Destroying it.", this);
            Destroy(this.gameObject);
        }

        Assert.IsNull(GetComponent<AbilityActivation>());

        foreach (ITargetedAbilityTrigger trigger in GetComponentsInParent<ITargetedAbilityTrigger>()) {
            trigger.targetedAbilityTriggerEvent += trigger_targetedAbilityTriggerEvent;
        }
    }

    /// <summary>
    /// Only includes untargeted constraints.
    /// </summary>
    /// <returns></returns>
    public bool Ready() {
        if (!abilityNetwork.ActivationEnabled) {
            //activation disabled
            return false;
        }

        foreach (UntargetedAbilityConstraint constraint in untargetedConstraints) {
            if (!constraint.isAbilityActivatible()) {
                //cannot activate
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Only includes targeted constraints.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool Ready(GameObject target) {
        foreach (ITargetedAbilityConstraint constraint in targetedConstraints) {
            if (!constraint.isAbilityActivatible(target)) {
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
    void trigger_targetedAbilityTriggerEvent(GameObject target) {
        if (!Ready() || !Ready(target)) {
            return;
        }

        PhotonView targetView = target.GetComponentInParent<PhotonView>();
        if (targetView == null) {
            return; //nothing to do; can't network the target
        }

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(true, null);

        bool send = false;
        foreach (TargetedAbilityAction abilityAction in abilityActions) {
            send |= abilityAction.Activate(target, stream);
            Assert.IsTrue(send || (stream.Count == 0), string.Format("Data written to stream but not flagged to be sent. {0}", abilityAction));
        }

        object[] data = new object[stream.Count + 1];
        data[0] = targetView.viewID;
        stream.ToArray().CopyTo(data, 1);

        abilityNetwork.ActivateRemote(this, data);

    }

    public void Activate(object[] incomingData, PhotonMessageInfo info) {

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(false, incomingData);
        int viewID = (int)stream.ReceiveNext();
        GameObject target = PhotonView.Find(viewID).gameObject;

        foreach (TargetedAbilityAction abilityAction in abilityActions) {
            abilityAction.Activate(target, stream);
        }
    }
}
