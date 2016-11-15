using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Makes targets immune for a period of time.
/// </summary>
public class DeDupTargets : TargetedAbilityConstraint, IBalanceStat {

    [SerializeField]
    protected float immunityDuration = 1;

    Queue<TimestampedData<GameObject>> immuneTargetsQueue = new Queue<TimestampedData<GameObject>>();
    HashSet<GameObject> immuneTargetsSet = new HashSet<GameObject>();

    public override bool isAbilityActivatible(GameObject target) {
        removeExpiredImmunities();
        return !immuneTargetsSet.Contains(target);
    }

    public override void Activate(GameObject context) {
        immuneTargetsQueue.Enqueue(new TimestampedData<GameObject>(Time.time, context));
        immuneTargetsSet.Add(context);
    }

    void removeExpiredImmunities() {
        while (immuneTargetsQueue.Count > 0 && immuneTargetsQueue.Peek().outputTime + immunityDuration <= Time.time) {
            bool success = immuneTargetsSet.Remove(immuneTargetsQueue.Dequeue().data);
            Assert.IsTrue(success);
        }
    }

    public void Reset() {
        immuneTargetsQueue.Clear();
        immuneTargetsSet.Clear();
    }

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch(type) {
            case BalanceStat.StatType.DURATION:
            default:
                immunityDuration = value;
                break;
        }
    }
}
