using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DestroyStackAction : TypedTargetedAbilityAction {

    public override bool isAbilityActivatible(GameObject target) {
        return target.GetComponentInParent<Stackable>() != null;
    }

    public override bool Activate(GameObject target, PhotonStream stream) {
        Debug.Log(target);
        Stackable stackable = target.GetComponentInParent<Stackable>();

        if (stream.isWriting) {

            if (stackable == null) { return false; }

            foreach (IDestackImmune immunity in target.transform.root.GetComponentsInChildren<IDestackImmune>()) {
                if(immunity.Immune) {
                    //write false if we don't destack
                    stream.SendNext(false);
                    //not destacking, so return immediately
                    return true;
                }
            }

            //write true if we do destack
            stream.SendNext(true);
            stackable.DestackAll();
            return true;

        } else {

            //only destack if true was written
            if((bool)stream.ReceiveNext()) {
                stackable.DestackAll();
            }
            return true;
        }
    }
}

