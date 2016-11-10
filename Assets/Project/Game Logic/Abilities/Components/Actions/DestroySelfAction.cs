using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DestroySelfAction : AbstractAbilityAction {

    public override bool Activate(PhotonStream stream) {
        Destroy(transform.root.gameObject);
        return true;
    }
}
