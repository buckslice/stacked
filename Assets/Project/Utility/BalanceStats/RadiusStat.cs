using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IRadiusStat : IBalanceStat { }

[ExecuteInEditMode]
public class RadiusStat : BalanceStat<IRadiusStat> { }
