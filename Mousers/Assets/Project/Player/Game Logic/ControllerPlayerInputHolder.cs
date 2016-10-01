using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class ControllerPlayerInputHolder : PlayerInputHolder
{
    [SerializeField]
    protected ControllerPlayerInput bindings;

    protected override IPlayerInput heldInput
    {
        get
        {
            return bindings;
        }
        set
        {
            bindings = (ControllerPlayerInput)value;
            bindings.Player = this.transform;
        }
    }
}

[System.Serializable]
public class ControllerPlayerInput : IPlayerInput
{
    [SerializeField]
    public string horizontalMovementAxis = Tags.Axis.Joystick1.HorizontalMovement;
    [SerializeField]
    public string verticalMovementAxis = Tags.Axis.Joystick1.VerticalMovement;
    [SerializeField]
    public string horizontalAimingAxis = Tags.Axis.Joystick1.HorizontalAiming;
    [SerializeField]
    public string verticalAimingAxis = Tags.Axis.Joystick1.VerticalAiming;

    [SerializeField]
    [Range(0, 0.5f)]
    protected float deadZone = 0.025f;

    Transform player;
    public Transform Player { set { player = value; } }

    /// <summary>
    /// Maps a value to account for a dead zone.
    /// </summary>
    /// <param name="value">The value to map. Should be in the range [0, 1].</param>
    /// <param name="deadZone">The size of the dead zone. If the dead zone is 0.25, then values at and below 0.25 will map to zero. Remaining values will be linear.</param>
    /// <returns>A dead-zone mapped value in the range [0, 1].</returns>
    static float mapDeadZone(float value, float deadZone)
    {
        Assert.IsTrue(0 <= value && value <= 1);
        Assert.IsTrue(0 <= deadZone && deadZone < 1);

        float oneMinusValue = 1 - value;
        oneMinusValue /= (1 - deadZone);
        float result = 1 - oneMinusValue;
        if (result < 0)
        {
            result = 0;
        }
        return result;
    }

    /// <summary>
    /// Performs smoothing, clamping, and dead zone for a joystick input.
    /// </summary>
    /// <param name="horizontalAxisName">Name of the horizontal axis of the joystick.</param>
    /// <param name="verticalAxisName">Name of the vertical axis of the joystick.</param>
    /// <param name="deadZone">The size of the dead zone. If the dead zone is 0.25, then values at and below 0.25 will map to zero. Remaining values will be linear.</param>
    /// <returns>A vector representing the input direction, with magnitude in the range [0, 1].</returns>
    static Vector2 getSmoothedJoystickInput(string horizontalAxisName, string verticalAxisName, float deadZone)
    {
        Vector2 result = new Vector2(Input.GetAxisRaw(horizontalAxisName), -Input.GetAxisRaw(verticalAxisName));
        float magnitude = result.magnitude;
        if (magnitude == 0)
        {
            return result; //it'll map to zero anyways.
        }

        if(magnitude > 1)
        {
            magnitude = 1;
        }
        magnitude = mapDeadZone(magnitude, deadZone);
        result *= magnitude / result.magnitude; //rescale to the new magnitude, preserving direction.
        return result;
    }

    public Vector2 movementDirection
    {
        get
        {
            return getSmoothedJoystickInput(horizontalMovementAxis, verticalMovementAxis, deadZone);
        }
    }
    public Vector3 rotationDirection
    {
        get
        {
            return getSmoothedJoystickInput(horizontalAimingAxis, verticalAimingAxis, deadZone).ConvertFromInputToWorld();
        }
    }
}