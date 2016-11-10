using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class SetEnabledAction : AbstractAbilityAction {

    [SerializeField]
    protected GameObject target;

    [SerializeField]
    protected bool outcomeState;

    public override bool Activate(PhotonStream stream) {
        bool initialState = target.activeSelf;
        target.SetActive(outcomeState);
        return target.activeSelf != initialState;
    }

}
