using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public delegate void ActivateAbility();

/// <summary>
/// Anything which fully activates an ability, designed to be used when activating abilities.
/// If the event may or may not activate the ability, use IAbilityTrigger.
/// </summary>
public interface IAbilityActivation
{
    event ActivateAbility abilityActivationEvent;
}

public delegate void ActivateAbilityWithData(object data);

/// <summary>
/// Anything which fully activates an ability with data, designed to be used when activating abilities.
/// If the event may or may not activate the ability, use IAbilityTrigger.
/// </summary>
public interface IAbilityActivationWithData
{
    event ActivateAbilityWithData abilityActivationWithDataEvent;
}

/*
/// <summary>
/// An event that triggers based on player input.
/// </summary>
public interface IAbilityInput {}
 * */

public delegate void AbilityTrigger();

/// <summary>
/// Anything which sends out an event which can possibly activate the ability.
/// </summary>
public interface IAbilityTrigger
{
    event AbilityTrigger abilityTriggerEvent;
}

public enum AbilityKeybinding { ABILITY1, ABILITY2 };

/// <summary>
/// Interface indicating that this has a keybinding
/// </summary>
public interface IAbilityKeybound
{
    AbilityKeybinding Binding { get; set; }
}

/// <summary>
/// Interface with which ability activation can be restricted
/// </summary>
public interface IAbilityConstraint
{
    bool isAbilityActivatible();
}

/// <summary>
/// Abstract class to contain functionality that almost all ability constraints will use
/// </summary>
public abstract class AbstractAbilityConstraint : AbstractAbilityAction, IAbilityConstraint
{
    AbilityActivation activation = null;
    protected override void Start()
    {
        base.Start();
        activation = GetComponent<AbilityActivation>();
        activation.AddConstraint(this);
    }

    protected void OnDestroy()
    {
        if (activation != null)
        {
            activation.RemoveConstraint(this);
        }
    }

    public abstract bool isAbilityActivatible();

    public override void ActivateWithData(object data) { }
    public override void ActivateRemote() { }
    
}

/// <summary>
/// Filters triggers to activate abilities.
/// </summary>
public class AbilityActivation : MonoBehaviour, IAbilityActivation
{
    public event ActivateAbility abilityActivationEvent = delegate { };

    protected List<IAbilityConstraint> constraints = new List<IAbilityConstraint>();

    public void AddConstraint(IAbilityConstraint toAdd) { constraints.Add(toAdd); }
    public bool RemoveConstraint(IAbilityConstraint toRemove) { return constraints.Remove(toRemove); }

    public void Start()
    {
        foreach (IAbilityTrigger trigger in GetComponentsInParent<IAbilityTrigger>())
        {
            trigger.abilityTriggerEvent += trigger_abilityTriggerEvent;
        }
    }

    void trigger_abilityTriggerEvent()
    {
        foreach (IAbilityConstraint constraint in constraints)
        {
            if (!constraint.isAbilityActivatible())
            {
                //cannot activate, do nothing
                return;
            }
        }

        abilityActivationEvent();
    }
}
