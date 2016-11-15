using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script used to ensure only one oneHotAbilityConstraint can activate at any time.
/// Often created at runtime by oneHotAbilityConstraints, but can be added manually to allow for multiple levels.
/// </summary>
public class OneHot : MonoBehaviour {
    public AllBoolStat activationAvailable = new AllBoolStat(true);
}
