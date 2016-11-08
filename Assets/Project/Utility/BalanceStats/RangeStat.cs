using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IRangeStat : IBalanceStat { }

[ExecuteInEditMode]
public class RangeStat : BalanceStat<IRangeStat> { }
