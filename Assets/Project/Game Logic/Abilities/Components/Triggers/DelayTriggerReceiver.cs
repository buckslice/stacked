using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// sends an activation to another trigger, after a delay.
/// </summary>
public class DelayTriggerReceiver : AbstractAbilityAction, IBalanceStat {

    [SerializeField]
    protected MonoBehaviour publisher;

    [SerializeField]
    protected float delay = 1;

    public override bool Activate(PhotonStream stream) {
        Callback.FireAndForget((publisher as IRemoteTrigger).Trigger, delay, this);
        return false; //the delayed ability will handle networking
    }

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch (type) {
            case BalanceStat.StatType.DURATION:
            default:
                delay = value;
                break;
        }
    }
}
