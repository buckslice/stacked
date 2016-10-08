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

public enum AbilityKeybinding { ABILITY1, ABILITY2 };

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

/// <summary>
/// An action which triggers a set of targeted actions via raycasting.
/// </summary>
public abstract class AbstractAreaCast : AbstractAbilityAction, ITargetedAbilityTrigger {

    [SerializeField]
    protected LayerMask layermask;

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };
    public event TargetedTrigger targetedAbilityTriggerEvent = (target) => { };

    public sealed override bool Activate(PhotonStream stream) {
        foreach (Collider collider in performCast()) {
            targetedAbilityTriggerEvent(collider.gameObject);
        }

        return false; //any networking action is handled by the child ativation
    }

    protected abstract Collider[] performCast();
}