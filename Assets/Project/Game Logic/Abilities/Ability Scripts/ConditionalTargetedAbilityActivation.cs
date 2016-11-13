using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class ConditionalTargetedAbilityActivation : TargetedAbilityAction, IAbilityConstrained, IAbilityTargetedConstrained {

    /// <summary>
    /// List of abilityActions to activate with this ability when the constraints pass, includes Constraints. Order must be the same on all clients.
    /// </summary>
    [SerializeField]
    protected TargetedAbilityAction[] successAbilityActions;
    public TargetedAbilityAction[] SuccessAbilityActions { get { return successAbilityActions; } }

    /// <summary>
    /// List of abilityActions to activate with this ability when the constraints fail, includes Constraints. Order must be the same on all clients.
    /// </summary>
    [SerializeField]
    protected TargetedAbilityAction[] failAbilityActions;
    public TargetedAbilityAction[] FailAbilityActions { get { return failAbilityActions; } }

    protected List<ITargetedAbilityConstraint> targetedConstraints = new List<ITargetedAbilityConstraint>();
    protected List<UntargetedAbilityConstraint> untargetedConstraints = new List<UntargetedAbilityConstraint>();

    public override bool Activate(GameObject context, PhotonStream stream) {

        bool constraintsSatisfied = true; //initial value

        if (stream.isWriting) {
            foreach (UntargetedAbilityConstraint constraint in untargetedConstraints) {
                constraintsSatisfied &= constraint.isAbilityActivatible();
            }

            foreach (ITargetedAbilityConstraint constraint in targetedConstraints) {
                constraintsSatisfied &= constraint.isAbilityActivatible(context);
            }

            stream.SendNext(constraintsSatisfied);
        } else {
            constraintsSatisfied = (bool)stream.ReceiveNext();
        }

        bool send = false;
        foreach (TargetedAbilityAction abilityAction in constraintsSatisfied ? successAbilityActions : failAbilityActions) {
            send |= abilityAction.Activate(context, stream);
        }

        //for now, always write and send. If necessary in the future, create a second PhotonStream to test if there is going to be any data needed to be sent.
        return send;
    }

    public void AddConstraint(ITargetedAbilityConstraint toAdd) {
        targetedConstraints.Add(toAdd);
    }
    public bool RemoveConstraint(ITargetedAbilityConstraint toRemove) {
        return targetedConstraints.Remove(toRemove);
    }

    public void AddConstraint(UntargetedAbilityConstraint toAdd) {
        untargetedConstraints.Add(toAdd);
    }
    public bool RemoveConstraint(UntargetedAbilityConstraint toRemove) {
        return untargetedConstraints.Remove(toRemove);
    }
}
