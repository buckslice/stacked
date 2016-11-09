using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class ConditionalAbilityActivation : AbstractAbilityAction, IAbilityConstrained {

    /// <summary>
    /// List of abilityActions to activate with this ability when the constraints pass, includes Constraints. Order must be the same on all clients.
    /// </summary>
    [SerializeField]
    protected AbstractAbilityAction[] successAbilityActions;
    public AbstractAbilityAction[] SuccessAbilityActions { get { return successAbilityActions; } }

    /// <summary>
    /// List of abilityActions to activate with this ability when the constraints fail, includes Constraints. Order must be the same on all clients.
    /// </summary>
    [SerializeField]
    protected AbstractAbilityAction[] failAbilityActions;
    public AbstractAbilityAction[] FailAbilityActions { get { return failAbilityActions; } }

    protected List<UntargetedAbilityConstraint> constraints = new List<UntargetedAbilityConstraint>();

    public override bool Activate(PhotonStream stream) {

        bool constraintsSatisfied = true; //initial value

        if (stream.isWriting) {
            foreach (UntargetedAbilityConstraint constraint in constraints) {
                Debug.Log(constraint);
                constraintsSatisfied &= constraint.isAbilityActivatible();
            }
            stream.SendNext(constraintsSatisfied);
        } else {
            constraintsSatisfied = (bool)stream.ReceiveNext();
        }

        foreach (AbstractAbilityAction abilityAction in constraintsSatisfied ? successAbilityActions : failAbilityActions) {
            abilityAction.Activate(stream);
        }

        //for now, always write and send. If necessary in the future, create a second PhotonStream to test if there is going to be any data needed to be sent.
        return true;
    }

    void IAbilityConstrained.AddConstraint(UntargetedAbilityConstraint toAdd) {
        constraints.Add(toAdd);
    }

    bool IAbilityConstrained.RemoveConstraint(UntargetedAbilityConstraint toRemove) {
        return constraints.Remove(toRemove);
    }
}
