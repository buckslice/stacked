using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IAngleStat : IBalanceStat { }

[ExecuteInEditMode]
public class AngleStat : BalanceStat<IAngleStat> { }
