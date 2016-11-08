using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IDurationStat : IBalanceStat { }

[ExecuteInEditMode]
public class DurationStat : BalanceStat<IDurationStat> { }
