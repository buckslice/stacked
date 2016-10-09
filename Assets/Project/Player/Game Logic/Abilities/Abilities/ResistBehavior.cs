using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class ResistBehavior : DurationAbilityAction {

    [SerializeField]
    protected float resistAmount = .5f;

    Damageable[] damageables;

    protected override void Start() {
        base.Start();
        damageables = transform.root.GetComponentsInChildren<Damageable>();
    }

    protected override void OnDurationBegin() {
        foreach (Damageable damageable in damageables) {
            damageable.MagicalVulnerabilityMultiplier.AddModifier(resistAmount);
            damageable.PhysicalVulnerabilityMultiplier.AddModifier(resistAmount);
        }
    }

    protected override void OnDurationEnd() {
        foreach (Damageable damageable in damageables) {
            damageable.MagicalVulnerabilityMultiplier.RemoveModifier(resistAmount);
            damageable.PhysicalVulnerabilityMultiplier.RemoveModifier(resistAmount);
        }
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
