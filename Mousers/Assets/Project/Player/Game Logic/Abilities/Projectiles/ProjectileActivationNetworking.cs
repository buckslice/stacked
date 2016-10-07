using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script for ability structures which only have one ability and do not need a mltiplexing system. Common on projectiles.
/// </summary>
public class ProjectileActivationNetworking : MonoBehaviour, IActivationNetworking {

    const string networkedActivationRPCName = "NetworkedActivationRPC";

    PhotonView view;
    AbilityActivation abilityActivation;

    void Awake() {
        view = GetComponent<PhotonView>();
    }

    void Start() {

        Assert.IsTrue(GetComponentsInChildren<AbilityActivation>().Length == 1);
        abilityActivation = GetComponentInChildren<AbilityActivation>();

        abilityActivation.Initialize(this);

        if (view != null && !view.isMine) {
            //we need to disable all input-related ability activation scripts
            foreach (IAbilityTrigger inputActivation in GetComponentsInChildren<IAbilityTrigger>()) {
                ((MonoBehaviour)inputActivation).enabled = false;
            }
        }
    }

    public void ActivateRemote(AbilityActivation ability, object[] data) {
        if (view == null)
            return;

        if (!view.isMine) {
            Debug.LogError("We do not own this object. All activations should originate from the owner. Discarding activation.");
            return;
        }

        view.RPC(networkedActivationRPCName, PhotonTargets.Others, data);
    }

    [PunRPC]
    public void NetworkedActivationRPC(object[] incomingData, PhotonMessageInfo info) {
        if (view.isMine) {
            Debug.LogError("We own this object. All activations should originate from us. Discarding activation.");
            return;
        }

        abilityActivation.Activate(incomingData);
    }
}
