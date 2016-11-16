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

    public override IPlayerInput HeldInput { //use our local bindings variable instead of parent bindings
        get {
            return bindings;
        }
        set {
            bindings = (ControllerPlayerInput)value;
            Start();
        }
    }
}

[System.Serializable]
public class ControllerPlayerInput : IPlayerInput {
    [SerializeField]
    public XInputDotNetPure.PlayerIndex controllerIndex = PlayerIndex.One;

    private string horizontalMovementAxis = Tags.Input.Joystick1.HorizontalMovement;
    private string verticalMovementAxis = Tags.Input.Joystick1.VerticalMovement;

    public enum AxisType {
        XBOX,
        PS4
    }

    public AxisType currentAxisType { get; private set; }

    private string horizontalAimingAxis = Tags.Input.Joystick1.axis4;
    private string verticalAimingAxis = Tags.Input.Joystick1.axis5;
    public string[] bindableAxes { get; private set; }

    public bool[] axisStates;
    private bool[] axisUp;
    private bool[] axisDown;

    public enum InputType {
        KEY,
        AXIS
    }

    private class Key{
        public InputType type;
        public KeyCode key;
        public string axis;

        public Key(KeyCode key) {
            this.key = key;
            this.type = InputType.KEY;
        }

        public Key(string axis) {
            this.axis = axis;
            this.type = InputType.AXIS;
        }

        public Key(Key toCopy) {
            this.type = toCopy.type;
            this.key = toCopy.key;
            this.axis = toCopy.axis;
        }
    };

    public enum Inputs {
        SUBMIT,
        CANCEL,
        START,
        ABILITY1,
        ABILITY2,
        BASIC_ATTACK,
        JUMP
    }

    private Key[] InputBindings;

    const int maxControllers = 4;
    public static readonly ControllerPlayerInput[] allControllers = new ControllerPlayerInput[maxControllers];

    MonoBehaviour holder;

    [SerializeField]
    private float deadZone = 0.2f;

    //Transform player; //not needed yet
    public Transform Player { set { ; } } //set { player = value; }

    public void Deactivate() {
        vibrationAmount.Reset();
        UpdateVibration();
        allControllers[(int)controllerIndex] = null;
    }

    AdditiveFloatStat vibrationAmount = new AdditiveFloatStat(0);

    public void Initialize(MonoBehaviour holder) {
        if (!initialized) {
            initialized = true;
            Initialize();
        }
        this.holder = holder;
    }

    bool initialized = false;

    void Initialize() {
        Assert.IsNull(allControllers[(int)controllerIndex]);
        allControllers[(int)controllerIndex] = this;

        InputBindings = new Key[7];
        axisStates = new bool[Tags.Input.Joystick1.allAxes.Length];
        axisUp = new bool[Tags.Input.Joystick1.allAxes.Length];
        axisDown = new bool[Tags.Input.Joystick1.allAxes.Length];
        currentAxisType = AxisType.XBOX;
        setXboxBindings();

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

    public bool getKey(Inputs key) {
        if (InputBindings[(int)key].type == InputType.KEY) {
            return Input.GetKey(InputBindings[(int)key].key);
        } else {
            return Input.GetAxisRaw(InputBindings[(int)key].axis) > deadZone;
        }
    }

    public bool getKeyDown(Inputs key) {
        if (InputBindings[(int)key].type == InputType.KEY) {
            return Input.GetKeyDown(InputBindings[(int)key].key);
        } else {
            return axisDown[getAxisNumberByString(InputBindings[(int)key].axis)];
        }
    }

    public bool getKeyUp(Inputs key) {
        if (InputBindings[(int)key].type == InputType.KEY) {
            return Input.GetKeyUp(InputBindings[(int)key].key);
        } else {
            return axisUp[getAxisNumberByString(InputBindings[(int)key].axis)];
        }
    }

    public bool getSubmit { get { return getKey(Inputs.SUBMIT); } }
    public bool getCancel { get { return getKey(Inputs.CANCEL); } }
    public bool getStart { get { return getKey(Inputs.START); } }
    public bool getBasicAttack { get { return getKey(Inputs.BASIC_ATTACK); } }
    public bool getAbility1 { get { return getKey(Inputs.ABILITY1); } }
    public bool getAbility2 { get { return getKey(Inputs.ABILITY2); } }
    public bool getJump { get { return getKey(Inputs.JUMP); } }

    public bool getSubmitDown { get { return getKeyDown(Inputs.SUBMIT); } }
    public bool getCancelDown { get { return getKeyDown(Inputs.CANCEL); } }
    public bool getStartDown { get { return getKeyDown(Inputs.START); } }
    public bool getBasicAttackDown { get { return getKeyDown(Inputs.BASIC_ATTACK); } }
    public bool getAbility1Down { get { return getKeyDown(Inputs.ABILITY1); } }
    public bool getAbility2Down { get { return getKeyDown(Inputs.ABILITY2); } }
    public bool getJumpDown { get { return getKeyDown(Inputs.JUMP); } }

    public bool getSubmitUp { get { return getKeyUp(Inputs.SUBMIT); } }
    public bool getCancelUp { get { return getKeyUp(Inputs.CANCEL); } }
    public bool getStartUp { get { return getKeyUp(Inputs.START); } }
    public bool getBasicAttackUp { get { return getKeyUp(Inputs.BASIC_ATTACK); }}
    public bool getAbility1Up { get { return getKeyUp(Inputs.ABILITY1); } }
    public bool getAbility2Up { get { return getKeyUp(Inputs.ABILITY2); } }
    public bool getJumpUp { get { return getKeyUp(Inputs.JUMP); } }


    //TODO re-do binding names

    public string getBindingName(Inputs key) {
        if (InputBindings[(int)key].type == InputType.KEY) {

            return PlayerInputExtension.getBindingName(InputBindings[(int)key].key, currentAxisType);

        } else if (InputBindings[(int)key].type == InputType.AXIS) {

            int axisNumber = getAxisNumberByString(InputBindings[(int)key].axis);
            return PlayerInputExtension.getBindingName(axisNumber, currentAxisType);
        }
        return "unknown type";
    }

    public string submitName { get { return getBindingName(Inputs.SUBMIT); } }
    public string cancelName { get { return getBindingName(Inputs.CANCEL); } }
    public string startName { get { return getBindingName(Inputs.START); } }
    public string basicAttackName { get { return getBindingName(Inputs.BASIC_ATTACK); } }
    public string ability1Name { get { return getBindingName(Inputs.ABILITY1); } }
    public string ability2Name { get { return getBindingName(Inputs.ABILITY2); } }
    public string jumpName { get { return getBindingName(Inputs.JUMP); } }

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

    public void remap(Inputs input, int button, InputType type) {
        Key oldKey = new Key(InputBindings[(int)input]);
        if (type == InputType.KEY) {

            InputBindings[(int)input].key = getInputByJoystickNumber(button);
            InputBindings[(int)input].type = InputType.KEY;

            for (int i = 0; i < InputBindings.Length; i++) {
                if (i != (int)input && InputBindings[i].key == getInputByJoystickNumber(button)) {
                    InputBindings[i] = oldKey;
                }
            }
        } else if (type == InputType.AXIS) {

            InputBindings[(int)input].axis = getAxisByJoystickNumber(button);
            InputBindings[(int)input].type = InputType.AXIS;

            for (int i = 0; i < InputBindings.Length; i++) {
                if (i != (int)input && InputBindings[i].axis == getAxisByJoystickNumber(button)) {
                    InputBindings[i] = oldKey;
                }
            }
        }
    }


    public void swapControllerType() {
        if (currentAxisType == AxisType.XBOX) {
            setPS4Bindings();
        }
        else {
            setXboxBindings();
        }
    }

    public void rebindToDefault() {
        setXboxBindings();
    }
    
    private void setXboxBindings() {
        currentAxisType = AxisType.XBOX;

        horizontalMovementAxis = getAxisByJoystickNumber((int)Tags.Input.axes.HorizontalMovement);
        verticalMovementAxis = getAxisByJoystickNumber((int)Tags.Input.axes.VerticalMovement);
        horizontalAimingAxis = getAxisByJoystickNumber((int)Tags.Input.axes.axis4);
        verticalAimingAxis = getAxisByJoystickNumber((int)Tags.Input.axes.axis5);

        bindableAxes = new string[] { getAxisByJoystickNumber((int)Tags.Input.axes.axis10), getAxisByJoystickNumber((int)Tags.Input.axes.axis9), getAxisByJoystickNumber((int)Tags.Input.axes.axis6), getAxisByJoystickNumber((int)Tags.Input.axes.axis3) };

        InputBindings[(int)Inputs.BASIC_ATTACK] = new Key(getInputByJoystickNumber(5));
        InputBindings[(int)Inputs.SUBMIT] = new Key(getInputByJoystickNumber(0));
        InputBindings[(int)Inputs.CANCEL] = new Key(getInputByJoystickNumber(1));
        InputBindings[(int)Inputs.START] = new Key(getInputByJoystickNumber(7));
        InputBindings[(int)Inputs.ABILITY1] = new Key(getAxisByJoystickNumber((int)Tags.Input.axes.axis9));
        InputBindings[(int)Inputs.ABILITY2] = new Key(getAxisByJoystickNumber((int)Tags.Input.axes.axis10));
        InputBindings[(int)Inputs.JUMP] = new Key(getInputByJoystickNumber(4)); 
    }

    private void setPS4Bindings() {
        currentAxisType = AxisType.PS4;

        horizontalMovementAxis = getAxisByJoystickNumber((int)Tags.Input.axes.HorizontalMovement);
        verticalMovementAxis = getAxisByJoystickNumber((int)Tags.Input.axes.VerticalMovement);
        horizontalAimingAxis = getAxisByJoystickNumber((int)Tags.Input.axes.axis3);
        verticalAimingAxis = getAxisByJoystickNumber((int)Tags.Input.axes.axis6);

        InputBindings[(int)Inputs.BASIC_ATTACK] = new Key(getInputByJoystickNumber(5));
        InputBindings[(int)Inputs.SUBMIT] = new Key(getInputByJoystickNumber(1));
        InputBindings[(int)Inputs.CANCEL] = new Key(getInputByJoystickNumber(2));
        InputBindings[(int)Inputs.START] = new Key(getInputByJoystickNumber(9));
        InputBindings[(int)Inputs.ABILITY1] = new Key(getInputByJoystickNumber(6));
        InputBindings[(int)Inputs.ABILITY2] = new Key(getInputByJoystickNumber(7));
        InputBindings[(int)Inputs.JUMP] = new Key(getInputByJoystickNumber(4));

        bindableAxes = new string[] { };
    }

    private int getButtonNumberByKeyCode(KeyCode button) {
        return PlayerInputExtension.buttonNumbers[button];
    }

    private int getAxisNumberByString(string axis) {
        return PlayerInputExtension.axisNumbers[axis];
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

    //Called on update to determine axisUp and axisDown
    void setAxisFlags() {
        string[] allAxes;
        switch (controllerIndex) {
            case PlayerIndex.One:
                allAxes = Tags.Input.Joystick1.allAxes;
                break;
            case PlayerIndex.Two:
                allAxes = Tags.Input.Joystick2.allAxes;
                break;
            case PlayerIndex.Three:
                allAxes = Tags.Input.Joystick3.allAxes;
                break;
            case PlayerIndex.Four:
                allAxes = Tags.Input.Joystick4.allAxes;
                break;
            default:
                Debug.Assert(false, "Out of range player index!");
                return;
        }
        for (int i = 0; i < axisStates.Length; i++) {
            axisUp[i] = false;
            axisDown[i] = false;
            bool currentState = Input.GetAxisRaw(allAxes[i]) > deadZone;
            if (axisStates[i] && !currentState) {
                axisUp[i] = true;
            }
            if (!axisStates[i] && currentState) {
                axisDown[i] = true;
            }
            axisStates[i] = currentState;
        }
    }

    public void Update() {
        setAxisFlags();
    }
}