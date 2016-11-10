using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// sends an activation to another trigger, after a delay.
/// </summary>
public class DelayTriggerReceiver : AbstractAbilityAction {

    [SerializeField]
    protected DelayTriggerPublisher publisher;

    [SerializeField]
    protected float delay = 1;

    public override bool Activate(PhotonStream stream) {
        Callback.FireAndForget(publisher.Trigger, delay, this);
        return false; //the delayed ability will handle networking
    }
}
