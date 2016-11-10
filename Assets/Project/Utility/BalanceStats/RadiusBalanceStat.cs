using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RadiusBalanceStat : BalanceStat {

    protected override StatType type {
        get { return StatType.RADIUS; }
    }
}
