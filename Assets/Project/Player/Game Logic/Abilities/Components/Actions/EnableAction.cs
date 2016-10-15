using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class EnableAction : DurationAbilityAction {

    [SerializeField]
    protected GameObject target;

    protected override void Awake() {
        base.Awake();
        target.SetActive(false);
    }

    protected override void OnDurationBegin() {
        target.SetActive(true);
    }

    protected override void OnDurationEnd() {
        target.SetActive(false);
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
