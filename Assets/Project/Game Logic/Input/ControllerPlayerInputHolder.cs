using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

/// <summary>
/// Intended to be used only in development to create players using the inspector. For all other purposes, use a PlayerInputHolder and set heldInput via script.
/// </summary>
public class ControllerPlayerInputHolder : PlayerInputHolder {
    [SerializeField]
    protected ControllerPlayerInput bindings;

    void Start() {
        bindings.Start();
    }

    public override IPlayerInput HeldInput {
        get {
            return bindings;
        }
        set {
            bindings = (ControllerPlayerInput)value;
            bindings.Player = this.transform;
        }
    }
}

[System.Serializable]
public class ControllerPlayerInput : IPlayerInput {
    public XInputDotNetPure.PlayerIndex controllerIndex = PlayerIndex.One;

    private string horizontalMovementAxis = Tags.Input.Joystick1.HorizontalMovement;
    private string verticalMovementAxis = Tags.Input.Joystick1.VerticalMovement;

    private enum AxisType {
        XBOX,
        PS4
    }

    AxisType currentAxisType = AxisType.XBOX;

    private string horizontalAimingAxis = Tags.Input.Joystick1.HorizontalAiming;
    private string verticalAimingAxis = Tags.Input.Joystick1.VerticalAiming;

    private enum InputType {
        KEY,
        AXIS
    }

    private struct Key{
        InputType type;
        KeyCode key;
        string axis;
    };

    private enum Inputs {
        SUBMIT,
        CANCEL,
        START,
        ABILITY1,
        ABILITY2,
        BASIC_ATTACK
    }

    private Key[] InputBindings;

    private string basicAttackAxis = Tags.Input.Joystick1.RightTrigger;

    private bool previousBasicAttackAxisStatus = false;
    private float previousBasicAttackDownTime = 0;
    private float previousBasicAttackUpTime = 0;

    private KeyCode submitKey = Tags.Input.Joystick1.button0;
    private KeyCode cancelKey = Tags.Input.Joystick1.button1;
    private KeyCode startKey = Tags.Input.Joystick1.button7;
    private KeyCode ability1Key = Tags.Input.Joystick1.button4;
    private KeyCode ability2Key = Tags.Input.Joystick1.button5;

    MonoBehaviour holder;

    [SerializeField]
    private float deadZone = 0.2f;

    //Transform player; //not needed yet
    public Transform Player { set { ; } } //set { player = value; }
    public void Initialize(MonoBehaviour holder) { this.holder = holder; }
    public void Deactivate() { vibrationAmount.Reset(); UpdateVibration(); }

    AdditiveFloatStat vibrationAmount = new AdditiveFloatStat(0);

    public void Start() {    // manually calling this in the holder..
        switch (controllerIndex) {
            case PlayerIndex.One:
                horizontalMovementAxis = Tags.Input.Joystick1.HorizontalMovement;
                verticalMovementAxis = Tags.Input.Joystick1.VerticalMovement;
                horizontalAimingAxis = Tags.Input.Joystick1.HorizontalAiming;
                verticalAimingAxis = Tags.Input.Joystick1.VerticalAiming;
                basicAttackAxis = Tags.Input.Joystick1.RightTrigger;
                submitKey = Tags.Input.Joystick1.button0;
                cancelKey = Tags.Input.Joystick1.button1;
                startKey = Tags.Input.Joystick1.button7;
                ability1Key = Tags.Input.Joystick1.button4;
                ability2Key = Tags.Input.Joystick1.button5;
                break;
            case PlayerIndex.Two:
                horizontalMovementAxis = Tags.Input.Joystick2.HorizontalMovement;
                verticalMovementAxis = Tags.Input.Joystick2.VerticalMovement;
                horizontalAimingAxis = Tags.Input.Joystick2.HorizontalAiming;
                verticalAimingAxis = Tags.Input.Joystick2.VerticalAiming;
                basicAttackAxis = Tags.Input.Joystick2.RightTrigger;
                submitKey = Tags.Input.Joystick2.button0;
                cancelKey = Tags.Input.Joystick2.button1;
                startKey = Tags.Input.Joystick2.button7;
                ability1Key = Tags.Input.Joystick2.button4;
                ability2Key = Tags.Input.Joystick2.button5;
                break;
            case PlayerIndex.Three:
                horizontalMovementAxis = Tags.Input.Joystick3.HorizontalMovement;
                verticalMovementAxis = Tags.Input.Joystick3.VerticalMovement;
                horizontalAimingAxis = Tags.Input.Joystick3.HorizontalAiming;
                verticalAimingAxis = Tags.Input.Joystick3.VerticalAiming;
                basicAttackAxis = Tags.Input.Joystick3.RightTrigger;
                submitKey = Tags.Input.Joystick3.button0;
                cancelKey = Tags.Input.Joystick3.button1;
                startKey = Tags.Input.Joystick3.button7;
                ability1Key = Tags.Input.Joystick3.button4;
                ability2Key = Tags.Input.Joystick3.button5;
                break;
            case PlayerIndex.Four:
                horizontalMovementAxis = Tags.Input.Joystick4.HorizontalMovement;
                verticalMovementAxis = Tags.Input.Joystick4.VerticalMovement;
                horizontalAimingAxis = Tags.Input.Joystick4.HorizontalAiming;
                verticalAimingAxis = Tags.Input.Joystick4.VerticalAiming;
                basicAttackAxis = Tags.Input.Joystick4.RightTrigger;
                submitKey = Tags.Input.Joystick4.button0;
                cancelKey = Tags.Input.Joystick4.button1;
                startKey = Tags.Input.Joystick4.button7;
                ability1Key = Tags.Input.Joystick4.button4;
                ability2Key = Tags.Input.Joystick4.button5;
                break;
            default:
                Debug.Assert(false, "Out of range player index!");
                break;
        }

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
    static Vector2 getSmoothedJoystickInput(string horizontalAxisName, string verticalAxisName, float deadZone) {
        Vector2 result = new Vector2(Input.GetAxisRaw(horizontalAxisName), -Input.GetAxisRaw(verticalAxisName));
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
            return getSmoothedJoystickInput(horizontalMovementAxis, verticalMovementAxis, deadZone);
        }
    }
    public Vector3 rotationDirection {
        get {
            return getSmoothedJoystickInput(horizontalAimingAxis, verticalAimingAxis, deadZone).ConvertFromInputToWorld();
        }
    }
    public bool getSubmit { get { return Input.GetKey(submitKey); } }
    public bool getCancel { get { return Input.GetKey(cancelKey); } }
    public bool getStart { get { return Input.GetKey(startKey); } }
    public bool getBasicAttack { get { return Input.GetAxisRaw(basicAttackAxis) > deadZone; } }
    public bool getAbility1 { get { return Input.GetKey(ability1Key); } }
    public bool getAbility2 { get { return Input.GetKey(ability2Key); } }

    public bool getSubmitDown { get { return Input.GetKeyDown(submitKey); } }
    public bool getCancelDown { get { return Input.GetKeyDown(cancelKey); } }
    public bool getStartDown { get { return Input.GetKeyDown(startKey); } }
    public bool getBasicAttackDown { get {
        checkBasicAttackStatus();
        return Time.time == previousBasicAttackDownTime;
    } }
    public bool getAbility1Down { get { return Input.GetKeyDown(ability1Key); } }
    public bool getAbility2Down { get { return Input.GetKeyDown(ability2Key); } }

    public bool getBasicAttackUp {
        get {
            checkBasicAttackStatus();
            return Time.time == previousBasicAttackUpTime;
        }
    }
    public bool getAbility1Up { get { return Input.GetKeyUp(ability1Key); } }
    public bool getAbility2Up { get { return Input.GetKeyUp(ability2Key); } }

    public string submitName { get { return PlayerInputExtension.getBindingName(submitKey); } }
    public string cancelName { get { return PlayerInputExtension.getBindingName(cancelKey); } }
    public string startName { get { return PlayerInputExtension.getBindingName(startKey); } }
    public string basicAttackName { get { return basicAttackAxis; } }
    public string ability1Name { get { return PlayerInputExtension.getBindingName(ability1Key); } }
    public string ability2Name { get { return PlayerInputExtension.getBindingName(ability2Key); } }

    /// <summary>
    /// Updates tracking for an axis.
    /// </summary>
    void checkBasicAttackStatus() {
        if (Time.time == previousBasicAttackDownTime || Time.time == previousBasicAttackUpTime) { return; }
        bool down = !previousBasicAttackAxisStatus && getBasicAttack;
        bool up = previousBasicAttackAxisStatus && !getBasicAttack;

        previousBasicAttackAxisStatus = getBasicAttack;

        if (down) {
            previousBasicAttackDownTime = Time.time;
        }
        if (up) {
            previousBasicAttackUpTime = Time.time;
        }
    }

    public void Vibrate(float strength, float duration, MonoBehaviour callingScript) {
        vibrationAmount.AddModifier(strength);
        UpdateVibration();

        Callback.FireAndForget(() => {
            vibrationAmount.RemoveModifier(strength);
            UpdateVibration();
        }, duration, callingScript);
    }

    public void Vibrate(float strength, float duration) { Vibrate(strength, duration, holder); }

    public void UpdateVibration() {
        GamePad.SetVibration(controllerIndex, vibrationAmount.Value, vibrationAmount.Value);
    }

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

    public void remap(int button) {

    }

    public void swapRightStickAndTriggers() {
        if (currentAxisType == AxisType.XBOX) {
            currentAxisType = AxisType.PS4;
            horizontalAimingAxis = getAxisByJoystickNumber((int)Tags.Input.axes.RightTrigger);
            verticalAimingAxis = getAxisByJoystickNumber((int)Tags.Input.axes.LeftTrigger);
        }
        else {
            currentAxisType = AxisType.XBOX;
            horizontalAimingAxis = getAxisByJoystickNumber((int)Tags.Input.axes.HorizontalAiming);
            verticalAimingAxis = getAxisByJoystickNumber((int)Tags.Input.axes.VerticalAiming);
        }
    }

    private KeyCode getInputByJoystickNumber(int button) {
        switch (controllerIndex) {
            case PlayerIndex.One:
                return Tags.Input.Joystick1.allButtons[button];
            case PlayerIndex.Two:
                return Tags.Input.Joystick2.allButtons[button];
            case PlayerIndex.Three:
                return Tags.Input.Joystick3.allButtons[button];
            case PlayerIndex.Four:
                return Tags.Input.Joystick4.allButtons[button];
            default:
                Debug.Assert(false, "Out of range player index!");
                break;
        }
        return 0;
    }

    private string getAxisByJoystickNumber(int axis) {
        switch (controllerIndex) {
            case PlayerIndex.One:
                return Tags.Input.Joystick1.allAxes[axis];
            case PlayerIndex.Two:
                return Tags.Input.Joystick2.allAxes[axis];
            case PlayerIndex.Three:
                return Tags.Input.Joystick3.allAxes[axis];
            case PlayerIndex.Four:
                return Tags.Input.Joystick4.allAxes[axis];
            default:
                Debug.Assert(false, "Out of range player index!");
                break;
        }
        return "";
    }
}