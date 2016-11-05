using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonCheckMenu : MonoBehaviour {
    public IPlayerInput bindings;
    public bool ready { get; private set; }

    private static float REPEAT_DELAY = 0.2f;
    private float currentDelay;
    private bool menuEnabled;

    private struct MenuOption
    {
        public string name;
        public string currentBinding;

        public MenuOption(string name, string currentBinding) {
            this.name = name;
            this.currentBinding = currentBinding;
        }
    }
    private MenuOption[] options;
    private int currentButton = 0;

    private Text previousButtonText;
    private Text currentButtonText;
    private Text nextButtonText;

    void Start() {
        previousButtonText = transform.GetChild(0).GetComponentInChildren<Text>();
        currentButtonText = transform.GetChild(1).GetComponentInChildren<Text>();
        nextButtonText = transform.GetChild(2).GetComponentInChildren<Text>();
        currentDelay = 0;
        menuEnabled = true;
        ready = false;
    }

    public void refreshOptions () {
        options = new MenuOption[9];
        options[0] = new MenuOption("Ready", "");
        if (bindings.GetType() == typeof(ControllerPlayerInput)) {
            ControllerPlayerInput controllerInput = (ControllerPlayerInput)bindings;
            if (controllerInput.currentAxisType == ControllerPlayerInput.AxisType.XBOX) {
                options[1] = new MenuOption("Current aiming axes - ", "XBOX");
            }
            else if (controllerInput.currentAxisType == ControllerPlayerInput.AxisType.PS4) {
                options[1] = new MenuOption("Current aiming axes - ", "PS4");
            }
        }
        else {
            options[1] = new MenuOption("Current aiming axes", "");
        }
        options[2] = new MenuOption("Submit - ", bindings.submitName);
        options[3] = new MenuOption("Cancel - ", bindings.cancelName);
        options[4] = new MenuOption("Start - ", bindings.startName);
        options[5] = new MenuOption("Ability 1 - ", bindings.ability1Name);
        options[6] = new MenuOption("Ability 2 - ", bindings.ability2Name);
        options[7] = new MenuOption("Basic Attack - ", bindings.basicAttackName);
        options[8] = new MenuOption("Jump - ", bindings.jumpName);
	}
	
    private void getMovement() {
        if (currentDelay <= 0) {
            if (bindings.movementDirection.y > 0) {
                if (currentButton == 0) {
                    currentButton = options.Length - 1;
                }
                else {
                    currentButton -= 1;
                }
                currentDelay = REPEAT_DELAY;
            }
            else if (bindings.movementDirection.y < 0) {
                currentButton = (currentButton + 1) % options.Length;
                currentDelay = REPEAT_DELAY;
            }
        }
        else {
            currentDelay -= Time.deltaTime;
        }
    }

    private void getInput() {
        //If controller
        if (bindings.GetType() == typeof(ControllerPlayerInput)) {
            ControllerPlayerInput controllerInput = (ControllerPlayerInput)bindings;
            KeyCode[] allButtons;
            string[] allAxes;
            switch (controllerInput.controllerIndex) {
                case XInputDotNetPure.PlayerIndex.One:
                    allButtons = Tags.Input.Joystick1.allButtons;
                    allAxes = Tags.Input.Joystick1.allAxes;
                    break;
                case XInputDotNetPure.PlayerIndex.Two:
                    allButtons = Tags.Input.Joystick2.allButtons;
                    allAxes = Tags.Input.Joystick2.allAxes;
                    break;
                case XInputDotNetPure.PlayerIndex.Three:
                    allButtons = Tags.Input.Joystick3.allButtons;
                    allAxes = Tags.Input.Joystick3.allAxes;
                    break;
                case XInputDotNetPure.PlayerIndex.Four:
                    allButtons = Tags.Input.Joystick4.allButtons;
                    allAxes = Tags.Input.Joystick4.allAxes;
                    break;
                default:
                    throw new UnityException("PlayerIndex out of range");
            }
            foreach (KeyCode button in allButtons) {
                if (Input.GetKeyDown(button)) {
                    onInput(button);
                    print(button);
                }
            }
            foreach(string axis in allAxes) {
                for (int i = 0; i < controllerInput.bindableAxes.Length; i++) {
                    if (axis == controllerInput.bindableAxes[i]) {
                        if (Input.GetAxisRaw(axis) > .5) {
                            onInput(axis);
                        }
                    }
                }
            }
        }
        //If Keyboard
        else {

        }
    }

    private void onInput(KeyCode key) {
        if (currentButton == 0) {
            menuEnabled = false;
            ready = true;
            return;
        }
        else if (currentButton == 1) {
            if (bindings.GetType() == typeof(ControllerPlayerInput)) {
                ControllerPlayerInput controllerInput = (ControllerPlayerInput)bindings;
                controllerInput.swapRightStickAndTriggers();
                refreshOptions();
            }
        }
        else {
            if (bindings.GetType() == typeof(ControllerPlayerInput)) {
                ControllerPlayerInput controllerInput = (ControllerPlayerInput)bindings;
                controllerInput.remap((ControllerPlayerInput.Inputs)(currentButton-2), PlayerInputExtension.buttonNumbers[key], ControllerPlayerInput.InputType.KEY);
                refreshOptions();
            }
        }
    }

    private void onInput(string axis) {
        if (currentButton == 0) {
            return;
        }
        else if (currentButton == 1) {
            return;
        }
        else {
            if (bindings.GetType() == typeof(ControllerPlayerInput)) {
                ControllerPlayerInput controllerInput = (ControllerPlayerInput)bindings;
                controllerInput.remap((ControllerPlayerInput.Inputs)(currentButton - 2), PlayerInputExtension.axisNumbers[axis], ControllerPlayerInput.InputType.AXIS);
                refreshOptions();
            }
        }
    }

    private void drawMenu() {
        int previousButton = currentButton - 1;
        if (previousButton < 0){
            previousButton = options.Length - 1;
        }
        previousButtonText.text = options[previousButton].name + options[previousButton].currentBinding;
        currentButtonText.text = options[currentButton].name + options[currentButton].currentBinding;
        int nextButton = (currentButton + 1) % options.Length;
        nextButtonText.text = options[nextButton].name + options[nextButton].currentBinding;
    }

	// Update is called once per frame
	void Update () {
        if (menuEnabled) {
            getMovement();
            getInput();
        }
        else {
            if (bindings.getCancelDown) {
                menuEnabled = true;
                ready = false;
            }
        }
        drawMenu();
	}
}
