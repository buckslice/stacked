﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class ResistAbility : DurationAbilityAction {

    [SerializeField]
    protected float resistAmount = .5f;

    Damageable[] damageables;

    protected override void Start() {
        base.Start();
        damageables = transform.root.GetComponentsInChildren<Damageable>();
    }

    protected override void OnDurationBegin() {
        foreach (Damageable damageable in damageables) {
            damageable.VulnerabilityMultiplier.AddModifier(resistAmount);
        }
    }

    protected override void OnDurationEnd() {
        foreach (Damageable damageable in damageables) {
            damageable.VulnerabilityMultiplier.RemoveModifier(resistAmount);
        }
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
