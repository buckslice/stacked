using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class InputTrigger : MonoBehaviour, IUntargetedAbilityTrigger, IAbilityKeybound
{
    [SerializeField]
    protected AbilityKeybinding binding = AbilityKeybinding.ABILITY1;
    public AbilityKeybinding Binding { get { return binding; } set { binding = value; } }

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    IPlayerInputHolder playerInput;

    void Start()
    {
        playerInput = GetComponentInParent<IPlayerInputHolder>();
        Assert.IsNotNull(playerInput, "Input not found.");
    }

    void Update()
    {
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
            abilityTriggerEvent();
        }
    }
}
