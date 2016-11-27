using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class TriggerOnActive : MonoBehaviour, IUntargetedAbilityTrigger {

    [SerializeField]
    protected float delay = 1;

    void OnEnable() {
        Assert.IsTrue(delay > 0);
        Callback.FireAndForget(() => abilityTriggerEvent(), delay, this);
    }

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };
}
