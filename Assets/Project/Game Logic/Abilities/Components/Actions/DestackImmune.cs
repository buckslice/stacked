using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Interface which marks the object it is on as immune to DestroyStackAction.
/// </summary>
public interface IDestackImmune {
    bool Immune { get; }
}

/// <summary>
/// Class implementing IDestackImmune using inspector-set values.
/// </summary>
public class DestackImmune : MonoBehaviour, IDestackImmune {
    [SerializeField]
    protected bool immune = true;

    bool IDestackImmune.Immune {
        get {
            return isActiveAndEnabled && immune;
        }
    }
}
