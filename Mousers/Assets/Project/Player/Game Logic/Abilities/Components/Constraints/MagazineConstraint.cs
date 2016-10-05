using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class MagazineConstraint : AbstractAbilityConstraint {

    /// <summary>
    /// How many activations there are in a single magazine.
    /// </summary>
    [SerializeField]
    protected AdditiveIntStat activationsInMagazine = new AdditiveIntStat(3);

    [SerializeField]
    protected MultiplierFloatStat reloadTimeSecs = new MultiplierFloatStat(3);

    /// <summary>
    /// Number of shots remaining, ignoring the possibility of a reload
    /// </summary>
    int activationsRemaining;

    float lastActivationTime = 0;
    public int ActivationsRemaining
    {
        get
        {
            if (Time.time > lastActivationTime + reloadTimeSecs)
            {
                //we've reloaded
                return Mathf.Max(activationsRemaining, activationsInMagazine);
            }
            else
            {
                //we have not reloaded
                return activationsRemaining;
            }
        }
    }

	protected override void Awake () {
        base.Awake();
        activationsRemaining = activationsInMagazine;
	}

    public override bool isAbilityActivatible()
    {
        return ActivationsRemaining > 0;
    }

    public override void Activate()
    {
        activationsRemaining = ActivationsRemaining - 1;
        lastActivationTime = Time.time;
    }
}
