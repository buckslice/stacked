using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class DurationBalanceStat : BalanceStat {

    protected override StatType type {
        get { return StatType.DURATION; }
    }
}
