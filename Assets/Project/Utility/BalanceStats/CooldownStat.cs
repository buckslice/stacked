using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface ICooldownStat : IBalanceStat { }

[ExecuteInEditMode]
public class CooldownStat : BalanceStat<ICooldownStat> { }
