using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Can be used as any stat. Generally, always use this unless there are multiple stats you could have.
/// </summary>
public interface IGenericStat : IAngleStat, ICooldownStat, IDamageStat, IDurationStat, IPowerStat, IRadiusStat, IRangeStat { }
