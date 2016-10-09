using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script for ability structures which only have one ability and do not need a mltiplexing system. Common on projectiles.
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class ProjectileActivationNetworking : MonoBehaviour, IActivationNetworking {

    const string networkedActivationRPCName = "ProjectileNetworkedActivationRPC";

    PhotonView view;
    IAbilityActivation abilityActivation;

    void Awake() {
        view = GetComponent<PhotonView>();
    }

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

    public void ActivateRemote(IAbilityActivation ability, object[] data) {
        if (view == null)
            return;

        if (!view.isMine) {
            Debug.LogError("We do not own this object. All activations should originate from the owner. Discarding activation.");
            return;
        }

        view.RPC(networkedActivationRPCName, PhotonTargets.Others, (object)(data));
        //see http://stackoverflow.com/questions/36350/how-to-pass-a-single-object-to-a-params-object
        //the single object[] is misconstrued as the entire params array without the cast
    }

    [PunRPC]
    public void ProjectileNetworkedActivationRPC(object[] incomingData, PhotonMessageInfo info) {
        if (view.isMine) {
            Debug.LogError("We own this object. All activations should originate from us. Discarding activation.");
            return;
        }

        abilityActivation.Activate(incomingData);
    }
}
