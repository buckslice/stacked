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
    public IPlayerInputHolder PlayerInput { get { return playerInput; } }

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
            case AbilityKeybinding.BASICATTACK:
                inputValue = playerInput.getBasicAttack;
                break;
            case AbilityKeybinding.BASICATTACKDOWN:
                inputValue = playerInput.getBasicAttackDown;
                break;
            case AbilityKeybinding.BASICATTACKUP:
                inputValue = playerInput.getBasicAttackUp;
                break;

            case AbilityKeybinding.ABILITY1:
                inputValue = playerInput.getAbility1;
                break;
            case AbilityKeybinding.ABILITY1DOWN:
                inputValue = playerInput.getAbility1Down;
                break;
            case AbilityKeybinding.ABILITY1UP:
                inputValue = playerInput.getAbility1Up;
                break;

            case AbilityKeybinding.ABILITY2:
                inputValue = playerInput.getAbility2;
                break;
            case AbilityKeybinding.ABILITY2DOWN:
                inputValue = playerInput.getAbility2Down;
                break;
            case AbilityKeybinding.ABILITY2UP:
                inputValue = playerInput.getAbility2Up;
                break;

            case AbilityKeybinding.SUBMIT:
                inputValue = playerInput.getSubmit;
                break;
            case AbilityKeybinding.SUBMITDOWN:
                inputValue = playerInput.getSubmitDown;
                break;

            case AbilityKeybinding.CANCEL:
                inputValue = playerInput.getCancel;
                break;
            case AbilityKeybinding.CANCELDOWN:
                inputValue = playerInput.getCancelDown;
                break;
            case AbilityKeybinding.JUMP:
                inputValue = playerInput.getJumpDown;
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
