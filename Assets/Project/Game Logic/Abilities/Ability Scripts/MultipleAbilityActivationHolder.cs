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

    [SerializeField]
    public SpawnAbility[] assortedOtherActivations;

	void Start () {
        //assert that we actually need this script
        Assert.IsTrue(GetComponentsInChildren<AbilityActivation>(true).Length >= 2 || GetComponentsInChildren<TargetedAbilityActivation>(true).Length >= 2 || assortedOtherActivations.Length > 0);

        //assert that all IAbilityActivations are contained by this script
        Assert.IsTrue(abilityActivations.Length + targetedAbilityActivation.Length + assortedOtherActivations.Length == GetComponentsInChildren<IAbilityActivation>(true).Length);
	}
}
