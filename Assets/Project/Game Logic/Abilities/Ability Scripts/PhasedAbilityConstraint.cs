using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class which indicates we should only be enabled for certain stages
/// </summary>
public class PhasedAbilityConstraint : UntargetedAbilityConstraint {

    [SerializeField]
    protected int[] activePhases = {0};

    public int[] ActivePhases { get { return activePhases; } }

    public int currentPhase { get; set; }

    public override bool isAbilityActivatible() {
        return activePhases.Contains(currentPhase);
    }

    public override void Activate() { }
}
