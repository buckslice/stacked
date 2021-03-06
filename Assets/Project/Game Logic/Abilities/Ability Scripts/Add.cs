﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents an add, which is a spawned unit which is friendly to players, but is not controlled by a person.
/// </summary>
[System.Serializable]
public class Add : AbstractDamageTracker {
    public Add(IDamageHolder holder) : base(holder) { }

    public override void Destroy() {}
}
