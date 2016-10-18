using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// For abilities with multiple ability activations, holds an ordered reference which contains all of them.
/// </summary>
public class MultipleAbilityActivationHolder : MonoBehaviour {

    [SerializeField]
    public AbilityActivation[] abilityActivations;

    [SerializeField]
    public TargetedAbilityActivation[] targetedAbilityActivation;

	void Start () {
        //assert that we actually need this script
        Assert.IsTrue(GetComponentsInChildren<AbilityActivation>().Length >= 2 || GetComponentsInChildren<TargetedAbilityActivation>().Length >= 2);

        //assert that all IAbilityActivations are contained by this script
        Assert.IsTrue(abilityActivations.Length + targetedAbilityActivation .Length == GetComponentsInChildren<IAbilityActivation>().Length);
	}
}
