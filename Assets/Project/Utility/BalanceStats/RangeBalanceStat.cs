using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class RangeBalanceStat : BalanceStat {

    protected override StatType type {
        get { return StatType.RANGE; }
    }
}
