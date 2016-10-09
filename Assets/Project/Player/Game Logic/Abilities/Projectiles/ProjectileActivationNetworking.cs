using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script for ability structures which only have one ability and do not need a mltiplexing system. Common on projectiles.
/// </summary>
public class ProjectileActivationNetworking : AbstractActivationNetworking {

    IAbilityActivation abilityActivation;

    void Start() {

        Assert.IsTrue(GetComponentsInChildren<IAbilityActivation>().Length == 1);
        abilityActivation = GetComponentInChildren<IAbilityActivation>();

        abilityActivation.Initialize(this);

        if (view != null && !view.isMine) {
            //we need to disable all ability activation scripts
            foreach (IUntargetedAbilityTrigger trigger in GetComponentsInChildren<IUntargetedAbilityTrigger>()) {
                ((MonoBehaviour)trigger).enabled = false;
            }
        }
    }

    public override void ActivateRemote(IAbilityActivation ability, object[] data) {
        if (view == null)
            return;

        if (!view.isMine) {
            Debug.LogError("We do not own this object. All activations should originate from the owner. Discarding activation.");
            return;
        }

        relay.ActivateRemote(this, data);
    }

    
    public override void NetworkedActivationRPC(object[] incomingData, PhotonMessageInfo info) {
        if (view.isMine) {
            Debug.LogError("We own this object. All activations should originate from us. Discarding activation.");
            return;
        }

        abilityActivation.Activate(incomingData, info);
    }
}
