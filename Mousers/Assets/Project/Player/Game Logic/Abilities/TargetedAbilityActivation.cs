using UnityEngine;
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

    protected List<ITargetedAbilityConstraint> constraints = new List<ITargetedAbilityConstraint>();

    public void AddConstraint(ITargetedAbilityConstraint toAdd) { constraints.Add(toAdd); }
    public bool RemoveConstraint(ITargetedAbilityConstraint toRemove) { return constraints.Remove(toRemove); }

    /// <summary>
    /// For covariance.
    /// </summary>
    public void AddConstraint(UntargetedAbilityConstraint toAdd) { AddConstraint((ITargetedAbilityConstraint)toAdd); }
    public bool RemoveConstraint(UntargetedAbilityConstraint toRemove) { return RemoveConstraint((ITargetedAbilityConstraint)toRemove); }

    IActivationNetworking abilityNetwork;

    /// <summary>
    /// Constructor-like method for initialization.
    /// </summary>
    /// <param name="abilityNetwork"></param>
    public void Initialize(IActivationNetworking abilityNetwork) {
        this.abilityNetwork = abilityNetwork;
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

    void trigger_targetedAbilityTriggerEvent(GameObject target) {
        foreach (ITargetedAbilityConstraint constraint in constraints) {
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

        PhotonView targetView = target.GetComponent<PhotonView>();
        if (targetView != null) {
            object[] data = new object[stream.Count + 1];
            data[0] = targetView.viewID;
            stream.ToArray().CopyTo(data, 1);

            abilityNetwork.ActivateRemote(this, data);
        }
    }

    public void Activate(object[] incomingData) {

        //TODO : re-use this object?
        PhotonStream stream = new PhotonStream(false, incomingData);
        int viewID = (int)stream.ReceiveNext();
        GameObject target = PhotonView.Find(viewID).gameObject;

        foreach (TargetedAbilityAction abilityAction in abilityActions) {
            abilityAction.Activate(target, stream);
        }
    }
}
