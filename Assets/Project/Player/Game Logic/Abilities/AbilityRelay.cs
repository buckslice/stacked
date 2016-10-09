using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IAbilityRelay {
    void ActivateRemote(IActivationNetworking requester, object[] outboundData);
}

/// <summary>
/// class to relay ability information back to/through a centralized PhotonView
/// </summary>
public class AbilityRelay : MonoBehaviour, IAbilityRelay {

    const string networkedActivationRPCName = "RPCDataTransfer";

    PhotonView view;
    IActivationNetworking relayTarget;

    protected void Awake() {
        view = GetComponent<PhotonView>();
    }

    void Start() {
        Assert.IsTrue(GetComponents<IActivationNetworking>().Length == 1);
        relayTarget = GetComponent<IActivationNetworking>();
        relayTarget.Initialize(this, view);
    }

    public void ActivateRemote(IActivationNetworking requester, object[] outboundData) {
        view.RPC(networkedActivationRPCName, PhotonTargets.Others, (object)outboundData);
        //see http://stackoverflow.com/questions/36350/how-to-pass-a-single-object-to-a-params-object
        //the single object[] is misconstrued as the entire params array without the cast
    }

    [PunRPC]
    public void RPCDataTransfer(object[] incomingData, PhotonMessageInfo info) {
        relayTarget.NetworkedActivationRPC(incomingData, info);
    }
}