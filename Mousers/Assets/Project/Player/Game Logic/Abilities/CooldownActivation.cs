using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public delegate void ActivateAbility();

/// <summary>
/// Anything which sends out an event, designed to be used when activating abilities
/// </summary>
public interface IAbilityActivation
{
    event ActivateAbility abilityActivationEvent;
}

public enum AbilityKeybinding { ABILITY1, ABILITY2 };

public interface IAbilityKeybound
{
    AbilityKeybinding Binding { get; set; }
}

/// <summary>
/// An activation with a cooldown.
/// </summary>
public class CooldownActivation : MonoBehaviour, IAbilityActivation, IAbilityKeybound
{
    [SerializeField]
    protected AbilityKeybinding binding = AbilityKeybinding.ABILITY1;
    public AbilityKeybinding Binding { get { return binding; } set { binding = value; } }

    [SerializeField]
    protected float cooldownSecs;

    public event ActivateAbility abilityActivationEvent = delegate { };

    IPlayerInputHolder playerInput;

    /// <summary>
    /// Time.time at which this ability is ready. If Time.time is greater than readyTime, the ability is ready. Otherwise, it is not.
    /// //TODO : Maybe change to use PhotonNetwork's synchronized time?
    /// </summary>
    float readyTime = 0;

    void Start()
    {
        playerInput = GetComponentInParent<IPlayerInputHolder>();
        Assert.IsNotNull(playerInput);
    }

	void Update () {

        if (Time.time < readyTime)
        {
            return; //we are on cooldown
        }

        bool inputValue;
        switch (binding)
        {
            case AbilityKeybinding.ABILITY1:
                inputValue = playerInput.getAbility1;
                break;
            case AbilityKeybinding.ABILITY2:
                inputValue = playerInput.getAbility2;
                break;
            default:
                inputValue = false;
                break;
        }

        if (inputValue)
        {
            readyTime = Time.time + cooldownSecs;
            abilityActivationEvent();
        }
	}
}
