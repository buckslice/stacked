using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class PauseAbility : DurationAbilityAction {

    BossAggro boss;

    protected override void Start() {
        base.Start();
        boss = GetComponentInParent<BossAggro>();
        Assert.IsNotNull(boss);
    }

    protected override void OnDurationBegin() {
        boss.ShouldChase.AddModifier(false);
    }

    protected override void OnDurationEnd() {
        boss.ShouldChase.RemoveModifier(false);
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
