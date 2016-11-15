using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public abstract class DurationAbilityAction : AbstractAbilityAction, IBalanceStat {

    [SerializeField]
    protected MultiplierFloatStat duration = new MultiplierFloatStat(1);

    Coroutine durationCoroutine = null;

    public override bool Activate(PhotonStream stream) {
        //only have one active routine at a time.
        if (durationCoroutine != null) {
            OnDurationInterrupted();
            StopCoroutine(durationCoroutine);
        }

        durationCoroutine = StartCoroutine(DurationRoutine());
        return true;
    }

    protected IEnumerator DurationRoutine() {
        OnDurationBegin();
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime) {
            OnDurationTick(Mathf.InverseLerp(startTime, endTime, Time.time));
            yield return null;
        }

        durationCoroutine = null;
        OnDurationEnd();
    }

    protected abstract void OnDurationBegin();
    /// <summary>
    /// Called every frame while the duration is active.
    /// </summary>
    /// <param name="lerpProgress">The progress of the ability, in the range [0..1]. progress is zero when the duration starts, and 1 when it ends.</param>
    protected virtual void OnDurationTick(float lerpProgress) { }
    protected abstract void OnDurationEnd();
    protected virtual void OnDurationInterrupted() { }

    public virtual void setValue(float value, BalanceStat.StatType type) {
        switch(type) {
            case BalanceStat.StatType.DURATION:
            default:
                duration = new MultiplierFloatStat(value);
                break;
        }
    }
}
