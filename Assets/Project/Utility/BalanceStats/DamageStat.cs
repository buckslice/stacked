using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IDamageStat : IBalanceStat { }

[ExecuteInEditMode]
public class DamageStat : BalanceStat<IDamageStat> { }
