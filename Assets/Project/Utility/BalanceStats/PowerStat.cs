using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IPowerStat : IBalanceStat { }

[ExecuteInEditMode]
public class PowerStat : BalanceStat<IPowerStat> { }
