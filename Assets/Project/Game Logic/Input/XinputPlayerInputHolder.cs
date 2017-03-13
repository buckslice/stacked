using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;
using System;

/// <summary>
/// Intended to be used only in development to create players using the inspector. For all other purposes, use a PlayerInputHolder and set heldInput via script.
/// </summary>
public class XinputPlayerInputHolder : PlayerInputHolder {
    [SerializeField]
    protected XinputPlayerInput bindings;

    public XinputPlayerInputHolder(XInputDotNetPure.PlayerIndex index) {
        bindings = new XinputPlayerInput(index);
    }

    public override IPlayerInput HeldInput { //use our local bindings variable instead of parent bindings
        get {
            return bindings;
        }
        set {
            bindings = (XinputPlayerInput)value;
            Start();
        }
    }
}

[System.Serializable]
public class XinputPlayerInput : IPlayerInput {
    [SerializeField]
    public XInputDotNetPure.PlayerIndex controllerIndex = PlayerIndex.One;

    private XInputDotNetPure.GamePadState currentState;
    private XInputDotNetPure.GamePadState previousState;

    MonoBehaviour holder;

    [SerializeField]
    protected float deadZone = 0.2f;
    [SerializeField]
    protected float triggerThreshold = 0.2f;

    public Transform Player { set {; } } //set { player = value; }

    public XinputPlayerInput(XInputDotNetPure.PlayerIndex index) {
        controllerIndex = index;
    }

    public void Deactivate() {
    }

    public void Initialize(MonoBehaviour holder) {
        if (!initialized) {
            initialized = true;
            Initialize();
        }
        this.holder = holder;
    }

    bool initialized = false;

    void Initialize() {
        setCurrentState(XInputDotNetPure.GamePad.GetState(controllerIndex));
    }

    private void setCurrentState(XInputDotNetPure.GamePadState state) {
        previousState = currentState;
        currentState = state;
    }

    /// <summary>
    /// Maps a value to account for a dead zone.
    /// </summary>
    /// <param name="value">The value to map. Should be in the range [0, 1].</param>
    /// <param name="deadZone">The size of the dead zone. If the dead zone is 0.25, then values at and below 0.25 will map to zero. Remaining values will be linear.</param>
    /// <returns>A dead-zone mapped value in the range [0, 1].</returns>
    static float mapDeadZone(float value, float deadZone) {
        Assert.IsTrue(0 <= value && value <= 1);
        Assert.IsTrue(0 <= deadZone && deadZone < 1);

        float oneMinusValue = 1 - value;
        oneMinusValue /= (1 - deadZone);
        float result = 1 - oneMinusValue;
        if (result < 0) {
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
    static Vector2 getSmoothedJoystickInput(float Xvalue, float Yvalue, float deadZone) {
        Vector2 result = new Vector2(Xvalue, Yvalue);
        float magnitude = result.magnitude;
        if (magnitude == 0) {
            return result; //it'll map to zero anyways.
        }

        if (magnitude > 1) {
            magnitude = 1;
        }
        magnitude = mapDeadZone(magnitude, deadZone);
        result *= magnitude / result.magnitude; //rescale to the new magnitude, preserving direction.
        return result;
    }

    public Vector2 movementDirection {
        get {
            return getSmoothedJoystickInput(currentState.ThumbSticks.Left.X, currentState.ThumbSticks.Left.Y, deadZone);
        }
    }
    public Vector3 rotationDirection {
        get {
            return getSmoothedJoystickInput(currentState.ThumbSticks.Right.X, currentState.ThumbSticks.Right.X, deadZone).ConvertFromInputToWorld();
        }
    }

    public bool getSubmit { get { return currentState.Buttons.A == ButtonState.Pressed; } }
    public bool getCancel { get { return currentState.Buttons.B == ButtonState.Pressed; } }
    public bool getStart { get { return currentState.Buttons.Start == ButtonState.Pressed; } }
    public bool getBasicAttack { get { return currentState.Buttons.RightShoulder == ButtonState.Pressed; } }
    public bool getAbility1 { get { return currentState.Triggers.Left>triggerThreshold;  } }
    public bool getAbility2 { get { return currentState.Triggers.Right > triggerThreshold; } }
    public bool getJump { get { return currentState.Buttons.LeftShoulder == ButtonState.Pressed; } }

    public bool getSubmitDown { get { return currentState.Buttons.A == ButtonState.Pressed && previousState.Buttons.A == ButtonState.Released ; } }
    public bool getCancelDown { get { return currentState.Buttons.B == ButtonState.Pressed && previousState.Buttons.B == ButtonState.Released; } }
    public bool getStartDown { get { return currentState.Buttons.Start == ButtonState.Pressed && previousState.Buttons.Start == ButtonState.Released; } }
    public bool getBasicAttackDown { get { return currentState.Buttons.RightShoulder == ButtonState.Pressed && previousState.Buttons.RightShoulder == ButtonState.Released; } }
    public bool getAbility1Down { get { return currentState.Triggers.Left > triggerThreshold && previousState.Triggers.Left<=triggerThreshold; } }
    public bool getAbility2Down { get { return currentState.Triggers.Right > triggerThreshold && previousState.Triggers.Right <= triggerThreshold; } }
    public bool getJumpDown { get { return currentState.Buttons.LeftShoulder == ButtonState.Pressed && previousState.Buttons.LeftShoulder == ButtonState.Released; } }

    public bool getSubmitUp { get { return currentState.Buttons.A == ButtonState.Released && previousState.Buttons.A == ButtonState.Pressed; } }
    public bool getCancelUp { get { return currentState.Buttons.B == ButtonState.Released && previousState.Buttons.B == ButtonState.Pressed; } }
    public bool getStartUp { get { return currentState.Buttons.Start == ButtonState.Released && previousState.Buttons.Start == ButtonState.Pressed; } }
    public bool getBasicAttackUp { get { return currentState.Buttons.RightShoulder == ButtonState.Released && previousState.Buttons.RightShoulder == ButtonState.Pressed; } }
    public bool getAbility1Up { get { return currentState.Triggers.Left <= triggerThreshold && previousState.Triggers.Left > triggerThreshold; } }
    public bool getAbility2Up { get { return currentState.Triggers.Right <= triggerThreshold && previousState.Triggers.Right > triggerThreshold; } }
    public bool getJumpUp { get { return currentState.Buttons.LeftShoulder == ButtonState.Released && previousState.Buttons.LeftShoulder == ButtonState.Pressed; } }


    public bool getAnyKey {
        get {
            ButtonState[] states = {
                currentState.Buttons.A,
                currentState.Buttons.B,
                currentState.Buttons.X,
                currentState.Buttons.Y,
                currentState.Buttons.Start,
                currentState.Buttons.Back,
                currentState.Buttons.LeftShoulder,
                currentState.Buttons.RightShoulder,
                currentState.Buttons.RightStick,
                currentState.Buttons.LeftStick
            };
            foreach (ButtonState state in states) {
                if (state == ButtonState.Pressed) {
                    return true;
                }
            }
            return false;
        }
    }

    public string submitName { get { return "A"; } }
    public string cancelName { get { return "B"; } }
    public string startName { get { return "Start"; } }
    public string basicAttackName { get { return "RB"; } }
    public string ability1Name { get { return "LT"; } }
    public string ability2Name { get { return "RT"; } }
    public string jumpName { get { return "LB"; } }

    public override bool Equals(object obj) {
        var item = obj as ControllerPlayerInput;

        if (item == null) {
            return false;
        }

        return this.controllerIndex.Equals(item.controllerIndex); //TODO : rewrite to include all input fields once custom bindings are added
    }

    public override int GetHashCode() {
        return controllerIndex.GetHashCode();
    }

    public void Update() {
        setCurrentState(XInputDotNetPure.GamePad.GetState(controllerIndex));
    }
}