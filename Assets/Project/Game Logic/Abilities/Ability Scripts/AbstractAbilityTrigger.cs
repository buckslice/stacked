using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/*
/// <summary>
/// An event that triggers based on player input.
/// </summary>
public interface IAbilityInput {}
 * */

public delegate void UntargetedAbilityTrigger();
public delegate void TargetedTrigger(GameObject target);

/// <summary>
/// Anything which sends out an event which can possibly activate the ability.
/// </summary>
public interface IUntargetedAbilityTrigger {
    event UntargetedAbilityTrigger abilityTriggerEvent;
}

/// <summary>
/// 
/// </summary>
public interface ITargetedAbilityTrigger : IUntargetedAbilityTrigger {
    event TargetedTrigger targetedAbilityTriggerEvent;
}

public enum AbilityKeybinding { BASICATTACK, BASICATTACKDOWN, BASICATTACKUP,
    ABILITY1, ABILITY1DOWN, ABILITY1UP,
    ABILITY2, ABILITY2DOWN, ABILITY2UP,
    SUBMIT, SUBMITDOWN,
    CANCEL, CANCELDOWN,
    JUMP
};

/// <summary>
/// Interface indicating that this has a keybinding
/// </summary>
public interface IAbilityKeybound {
    AbilityKeybinding Binding { get; set; }
}

/// <summary>
/// Ability trigger which also supplies a target
/// </summary>
public abstract class TargetedAbilityTrigger : MonoBehaviour, ITargetedAbilityTrigger {

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };
    private void FireUntargetedEvent(GameObject target) { abilityTriggerEvent(); }

    public event TargetedTrigger targetedAbilityTriggerEvent = (target) => { };

    protected virtual void Awake() {
        targetedAbilityTriggerEvent += FireUntargetedEvent;
    }

    protected void FireTrigger(GameObject target) { targetedAbilityTriggerEvent(target); }
}