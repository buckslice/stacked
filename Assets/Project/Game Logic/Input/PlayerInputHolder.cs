using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface used to denote a class which provides player input.
/// </summary>
public interface IPlayerInput : IPlayerInputHolder
{
    /// <summary>
    /// The transform component of the player.
    /// </summary>
    Transform Player { set; }
    void Initialize(MonoBehaviour holder);
    void Deactivate();

    void Update();
}

/// <summary>
/// Contains extension methods for PlayerInput.
/// </summary>
public static class PlayerInputExtension
{
    public static bool AnyKey(this IPlayerInputHolder self) {
        return self.getAnyKey;
    }

    public static bool AnyAxis(this IPlayerInputHolder self)
    {
        return self.movementDirection != Vector2.zero ||
            self.rotationDirection != Vector3.zero;
    }

    public static string getBindingName(KeyCode keycode) {
        if (bindingNames.ContainsKey(keycode)) {
            return bindingNames[keycode];
        } else {
            return "";
        }
    }

    public static string getBindingName(KeyCode keycode, ControllerPlayerInput.AxisType type) {
        switch (type) {
            case ControllerPlayerInput.AxisType.XBOX:
                if (xboxBindingNames.ContainsKey(keycode)) {
                    return xboxBindingNames[keycode];
                } else {
                    return "";
                }

            case ControllerPlayerInput.AxisType.PS4:
                if (ps4BindingNames.ContainsKey(keycode)) {
                    return ps4BindingNames[keycode];
                } else {
                    return "";
                }

            default:
                return "unknownAxisType";
        }
    }

    public static string getBindingName(int axisNumber, ControllerPlayerInput.AxisType type) {
        switch (type) {
            case ControllerPlayerInput.AxisType.XBOX:
                if (xboxAxisNames.ContainsKey(axisNumber)) {
                    return xboxAxisNames[axisNumber];
                } else {
                    return "";
                }

            case ControllerPlayerInput.AxisType.PS4:
                if (ps4AxisNames.ContainsKey(axisNumber)) {
                    return ps4AxisNames[axisNumber];
                } else {
                    return "";
                }

            default:
                return "unknownAxisType";
        }
    }

    public static readonly Dictionary<KeyCode, string> bindingNames = new Dictionary<KeyCode, string>(){
        {KeyCode.A, "A"},
        {KeyCode.B, "B"},
        {KeyCode.C, "C"},
        {KeyCode.D, "D"},
        {KeyCode.E, "E"},
        {KeyCode.F, "F"},
        {KeyCode.G, "G"},
        {KeyCode.H, "H"},
        {KeyCode.I, "I"},
        {KeyCode.J, "J"},
        {KeyCode.K, "K"},
        {KeyCode.L, "L"},
        {KeyCode.M, "M"},
        {KeyCode.N, "N"},
        {KeyCode.O, "O"},
        {KeyCode.P, "P"},
        {KeyCode.Q, "Q"},
        {KeyCode.R, "R"},
        {KeyCode.S, "S"},
        {KeyCode.T, "T"},
        {KeyCode.U, "U"},
        {KeyCode.V, "V"},
        {KeyCode.W, "W"},
        {KeyCode.X, "X"},
        {KeyCode.Y, "Y"},
        {KeyCode.Z, "Z"},

        {KeyCode.Alpha0, "0"},
        {KeyCode.Alpha1, "1"},
        {KeyCode.Alpha2, "2"},
        {KeyCode.Alpha3, "3"},
        {KeyCode.Alpha4, "4"},
        {KeyCode.Alpha5, "5"},
        {KeyCode.Alpha6, "6"},
        {KeyCode.Alpha7, "7"},
        {KeyCode.Alpha8, "8"},
        {KeyCode.Alpha9, "9"},

        {KeyCode.LeftShift, "SHF"},
        {KeyCode.Return, "NTR"},
        {KeyCode.Delete, "DEL"},
        {KeyCode.Backspace, "DEL"},
        {KeyCode.Space, "SPC"},
        {KeyCode.Escape, "ESC"},
    };

    public static readonly Dictionary<KeyCode, string> xboxBindingNames = new Dictionary<KeyCode, string>(){

        {KeyCode.JoystickButton0, "A"},
        {KeyCode.Joystick1Button0, "A"},
        {KeyCode.Joystick2Button0, "A"},
        {KeyCode.Joystick3Button0, "A"},
        {KeyCode.Joystick4Button0, "A"},
        
        {KeyCode.JoystickButton1, "B"},
        {KeyCode.Joystick1Button1, "B"},
        {KeyCode.Joystick2Button1, "B"},
        {KeyCode.Joystick3Button1, "B"},
        {KeyCode.Joystick4Button1, "B"},
        
        {KeyCode.JoystickButton2, "X"},
        {KeyCode.Joystick1Button2, "X"},
        {KeyCode.Joystick2Button2, "X"},
        {KeyCode.Joystick3Button2, "X"},
        {KeyCode.Joystick4Button2, "X"},

        {KeyCode.JoystickButton3, "Y"},
        {KeyCode.Joystick1Button3, "Y"},
        {KeyCode.Joystick2Button3, "Y"},
        {KeyCode.Joystick3Button3, "Y"},
        {KeyCode.Joystick4Button3, "Y"},

        {KeyCode.JoystickButton4, "LB"},
        {KeyCode.Joystick1Button4, "LB"},
        {KeyCode.Joystick2Button4, "LB"},
        {KeyCode.Joystick3Button4, "LB"},
        {KeyCode.Joystick4Button4, "LB"},
        
        {KeyCode.JoystickButton5, "RB"},
        {KeyCode.Joystick1Button5, "RB"},
        {KeyCode.Joystick2Button5, "RB"},
        {KeyCode.Joystick3Button5, "RB"},
        {KeyCode.Joystick4Button5, "RB"},

        {KeyCode.JoystickButton6, "BCK"},
        {KeyCode.Joystick1Button6, "BCK"},
        {KeyCode.Joystick2Button6, "BCK"},
        {KeyCode.Joystick3Button6, "BCK"},
        {KeyCode.Joystick4Button6, "BCK"},

        {KeyCode.JoystickButton7, "STRT"},
        {KeyCode.Joystick1Button7, "STRT"},
        {KeyCode.Joystick2Button7, "STRT"},
        {KeyCode.Joystick3Button7, "STRT"},
        {KeyCode.Joystick4Button7, "STRT"},

        {KeyCode.JoystickButton8, "Button 8"},
        {KeyCode.Joystick1Button8, "Button 8"},
        {KeyCode.Joystick2Button8, "Button 8"},
        {KeyCode.Joystick3Button8, "Button 8"},
        {KeyCode.Joystick4Button8, "Button 8"},

        {KeyCode.JoystickButton9, "Button 9"},
        {KeyCode.Joystick1Button9, "Button 9"},
        {KeyCode.Joystick2Button9, "Button 9"},
        {KeyCode.Joystick3Button9, "Button 9"},
        {KeyCode.Joystick4Button9, "Button 9"},

        {KeyCode.JoystickButton10, "Button 10"},
        {KeyCode.Joystick1Button10, "Button 10"},
        {KeyCode.Joystick2Button10, "Button 10"},
        {KeyCode.Joystick3Button10, "Button 10"},
        {KeyCode.Joystick4Button10, "Button 10"},

        {KeyCode.JoystickButton11, "Button 11"},
        {KeyCode.Joystick1Button11, "Button 11"},
        {KeyCode.Joystick2Button11, "Button 11"},
        {KeyCode.Joystick3Button11, "Button 11"},
        {KeyCode.Joystick4Button11, "Button 11"},

        {KeyCode.JoystickButton12, "Button 12"},
        {KeyCode.Joystick1Button12, "Button 12"},
        {KeyCode.Joystick2Button12, "Button 12"},
        {KeyCode.Joystick3Button12, "Button 12"},
        {KeyCode.Joystick4Button12, "Button 12"},

        {KeyCode.JoystickButton13, "Button 13"},
        {KeyCode.Joystick1Button13, "Button 13"},
        {KeyCode.Joystick2Button13, "Button 13"},
        {KeyCode.Joystick3Button13, "Button 13"},
        {KeyCode.Joystick4Button13, "Button 13"},

        {KeyCode.JoystickButton14, "Button 14"},
        {KeyCode.Joystick1Button14, "Button 14"},
        {KeyCode.Joystick2Button14, "Button 14"},
        {KeyCode.Joystick3Button14, "Button 14"},
        {KeyCode.Joystick4Button14, "Button 14"},

        {KeyCode.JoystickButton15, "Button 15"},
        {KeyCode.Joystick1Button15, "Button 15"},
        {KeyCode.Joystick2Button15, "Button 15"},
        {KeyCode.Joystick3Button15, "Button 15"},
        {KeyCode.Joystick4Button15, "Button 15"},

        {KeyCode.JoystickButton16, "Button 16"},
        {KeyCode.Joystick1Button16, "Button 16"},
        {KeyCode.Joystick2Button16, "Button 16"},
        {KeyCode.Joystick3Button16, "Button 16"},
        {KeyCode.Joystick4Button16, "Button 16"},

        {KeyCode.JoystickButton17, "Button 17"},
        {KeyCode.Joystick1Button17, "Button 17"},
        {KeyCode.Joystick2Button17, "Button 17"},
        {KeyCode.Joystick3Button17, "Button 17"},
        {KeyCode.Joystick4Button17, "Button 17"},

        {KeyCode.JoystickButton18, "Button 18"},
        {KeyCode.Joystick1Button18, "Button 18"},
        {KeyCode.Joystick2Button18, "Button 18"},
        {KeyCode.Joystick3Button18, "Button 18"},
        {KeyCode.Joystick4Button18, "Button 18"},

        {KeyCode.JoystickButton19, "Button 19"},
        {KeyCode.Joystick1Button19, "Button 19"},
        {KeyCode.Joystick2Button19, "Button 19"},
        {KeyCode.Joystick3Button19, "Button 19"},
        {KeyCode.Joystick4Button19, "Button 19"},
    };

    public static readonly Dictionary<KeyCode, string> ps4BindingNames = new Dictionary<KeyCode, string>(){

        {KeyCode.JoystickButton0, "SQR"},
        {KeyCode.Joystick1Button0, "SQR"},
        {KeyCode.Joystick2Button0, "SQR"},
        {KeyCode.Joystick3Button0, "SQR"},
        {KeyCode.Joystick4Button0, "SQR"},
        
        {KeyCode.JoystickButton1, "X"},
        {KeyCode.Joystick1Button1, "X"},
        {KeyCode.Joystick2Button1, "X"},
        {KeyCode.Joystick3Button1, "X"},
        {KeyCode.Joystick4Button1, "X"},
        
        {KeyCode.JoystickButton2, "CIR"},
        {KeyCode.Joystick1Button2, "CIR"},
        {KeyCode.Joystick2Button2, "CIR"},
        {KeyCode.Joystick3Button2, "CIR"},
        {KeyCode.Joystick4Button2, "CIR"},

        {KeyCode.JoystickButton3, "TRI"},
        {KeyCode.Joystick1Button3, "TRI"},
        {KeyCode.Joystick2Button3, "TRI"},
        {KeyCode.Joystick3Button3, "TRI"},
        {KeyCode.Joystick4Button3, "TRI"},

        {KeyCode.JoystickButton4, "L1"},
        {KeyCode.Joystick1Button4, "L1"},
        {KeyCode.Joystick2Button4, "L1"},
        {KeyCode.Joystick3Button4, "L1"},
        {KeyCode.Joystick4Button4, "L1"},
        
        {KeyCode.JoystickButton5, "R1"},
        {KeyCode.Joystick1Button5, "R1"},
        {KeyCode.Joystick2Button5, "R1"},
        {KeyCode.Joystick3Button5, "R1"},
        {KeyCode.Joystick4Button5, "R1"},

        {KeyCode.JoystickButton6, "L2"},
        {KeyCode.Joystick1Button6, "L2"},
        {KeyCode.Joystick2Button6, "L2"},
        {KeyCode.Joystick3Button6, "L2"},
        {KeyCode.Joystick4Button6, "L2"},

        {KeyCode.JoystickButton7, "R2"},
        {KeyCode.Joystick1Button7, "R2"},
        {KeyCode.Joystick2Button7, "R2"},
        {KeyCode.Joystick3Button7, "R2"},
        {KeyCode.Joystick4Button7, "R2"},

        {KeyCode.JoystickButton8, "Button 8"},
        {KeyCode.Joystick1Button8, "Button 8"},
        {KeyCode.Joystick2Button8, "Button 8"},
        {KeyCode.Joystick3Button8, "Button 8"},
        {KeyCode.Joystick4Button8, "Button 8"},

        {KeyCode.JoystickButton9, "STRT"},
        {KeyCode.Joystick1Button9, "STRT"},
        {KeyCode.Joystick2Button9, "STRT"},
        {KeyCode.Joystick3Button9, "STRT"},
        {KeyCode.Joystick4Button9, "STRT"},

        {KeyCode.JoystickButton10, "Button 10"},
        {KeyCode.Joystick1Button10, "Button 10"},
        {KeyCode.Joystick2Button10, "Button 10"},
        {KeyCode.Joystick3Button10, "Button 10"},
        {KeyCode.Joystick4Button10, "Button 10"},

        {KeyCode.JoystickButton11, "Button 11"},
        {KeyCode.Joystick1Button11, "Button 11"},
        {KeyCode.Joystick2Button11, "Button 11"},
        {KeyCode.Joystick3Button11, "Button 11"},
        {KeyCode.Joystick4Button11, "Button 11"},

        {KeyCode.JoystickButton12, "Button 12"},
        {KeyCode.Joystick1Button12, "Button 12"},
        {KeyCode.Joystick2Button12, "Button 12"},
        {KeyCode.Joystick3Button12, "Button 12"},
        {KeyCode.Joystick4Button12, "Button 12"},

        {KeyCode.JoystickButton13, "Button 13"},
        {KeyCode.Joystick1Button13, "Button 13"},
        {KeyCode.Joystick2Button13, "Button 13"},
        {KeyCode.Joystick3Button13, "Button 13"},
        {KeyCode.Joystick4Button13, "Button 13"},

        {KeyCode.JoystickButton14, "Button 14"},
        {KeyCode.Joystick1Button14, "Button 14"},
        {KeyCode.Joystick2Button14, "Button 14"},
        {KeyCode.Joystick3Button14, "Button 14"},
        {KeyCode.Joystick4Button14, "Button 14"},

        {KeyCode.JoystickButton15, "Button 15"},
        {KeyCode.Joystick1Button15, "Button 15"},
        {KeyCode.Joystick2Button15, "Button 15"},
        {KeyCode.Joystick3Button15, "Button 15"},
        {KeyCode.Joystick4Button15, "Button 15"},

        {KeyCode.JoystickButton16, "Button 16"},
        {KeyCode.Joystick1Button16, "Button 16"},
        {KeyCode.Joystick2Button16, "Button 16"},
        {KeyCode.Joystick3Button16, "Button 16"},
        {KeyCode.Joystick4Button16, "Button 16"},

        {KeyCode.JoystickButton17, "Button 17"},
        {KeyCode.Joystick1Button17, "Button 17"},
        {KeyCode.Joystick2Button17, "Button 17"},
        {KeyCode.Joystick3Button17, "Button 17"},
        {KeyCode.Joystick4Button17, "Button 17"},

        {KeyCode.JoystickButton18, "Button 18"},
        {KeyCode.Joystick1Button18, "Button 18"},
        {KeyCode.Joystick2Button18, "Button 18"},
        {KeyCode.Joystick3Button18, "Button 18"},
        {KeyCode.Joystick4Button18, "Button 18"},

        {KeyCode.JoystickButton19, "Button 19"},
        {KeyCode.Joystick1Button19, "Button 19"},
        {KeyCode.Joystick2Button19, "Button 19"},
        {KeyCode.Joystick3Button19, "Button 19"},
        {KeyCode.Joystick4Button19, "Button 19"},
    };


    //Potentially move these to tags?
    public static readonly Dictionary<KeyCode, int> buttonNumbers = new Dictionary<KeyCode, int>() {
        {KeyCode.Joystick1Button0, 0 },
        {KeyCode.Joystick1Button1, 1 },
        {KeyCode.Joystick1Button2, 2 },
        {KeyCode.Joystick1Button3, 3 },
        {KeyCode.Joystick1Button4, 4 },
        {KeyCode.Joystick1Button5, 5 },
        {KeyCode.Joystick1Button6, 6 },
        {KeyCode.Joystick1Button7, 7 },
        {KeyCode.Joystick1Button8, 8 },
        {KeyCode.Joystick1Button9, 9 },
        {KeyCode.Joystick1Button10, 10 },
        {KeyCode.Joystick1Button11, 11 },
        {KeyCode.Joystick1Button12, 12 },
        {KeyCode.Joystick1Button13, 13 },
        {KeyCode.Joystick1Button14, 14 },
        {KeyCode.Joystick1Button15, 15 },
        {KeyCode.Joystick1Button16, 16 },
        {KeyCode.Joystick1Button17, 17 },
        {KeyCode.Joystick1Button18, 18 },
        {KeyCode.Joystick1Button19, 19 },

        {KeyCode.Joystick2Button0, 0 },
        {KeyCode.Joystick2Button1, 1 },
        {KeyCode.Joystick2Button2, 2 },
        {KeyCode.Joystick2Button3, 3 },
        {KeyCode.Joystick2Button4, 4 },
        {KeyCode.Joystick2Button5, 5 },
        {KeyCode.Joystick2Button6, 6 },
        {KeyCode.Joystick2Button7, 7 },
        {KeyCode.Joystick2Button8, 8 },
        {KeyCode.Joystick2Button9, 9 },
        {KeyCode.Joystick2Button10, 10 },
        {KeyCode.Joystick2Button11, 11 },
        {KeyCode.Joystick2Button12, 12 },
        {KeyCode.Joystick2Button13, 13 },
        {KeyCode.Joystick2Button14, 14 },
        {KeyCode.Joystick2Button15, 15 },
        {KeyCode.Joystick2Button16, 16 },
        {KeyCode.Joystick2Button17, 17 },
        {KeyCode.Joystick2Button18, 18 },
        {KeyCode.Joystick2Button19, 19 },

        {KeyCode.Joystick3Button0, 0 },
        {KeyCode.Joystick3Button1, 1 },
        {KeyCode.Joystick3Button2, 2 },
        {KeyCode.Joystick3Button3, 3 },
        {KeyCode.Joystick3Button4, 4 },
        {KeyCode.Joystick3Button5, 5 },
        {KeyCode.Joystick3Button6, 6 },
        {KeyCode.Joystick3Button7, 7 },
        {KeyCode.Joystick3Button8, 8 },
        {KeyCode.Joystick3Button9, 9 },
        {KeyCode.Joystick3Button10, 10 },
        {KeyCode.Joystick3Button11, 11 },
        {KeyCode.Joystick3Button12, 12 },
        {KeyCode.Joystick3Button13, 13 },
        {KeyCode.Joystick3Button14, 14 },
        {KeyCode.Joystick3Button15, 15 },
        {KeyCode.Joystick3Button16, 16 },
        {KeyCode.Joystick3Button17, 17 },
        {KeyCode.Joystick3Button18, 18 },
        {KeyCode.Joystick3Button19, 19 },

        {KeyCode.Joystick4Button0, 0 },
        {KeyCode.Joystick4Button1, 1 },
        {KeyCode.Joystick4Button2, 2 },
        {KeyCode.Joystick4Button3, 3 },
        {KeyCode.Joystick4Button4, 4 },
        {KeyCode.Joystick4Button5, 5 },
        {KeyCode.Joystick4Button6, 6 },
        {KeyCode.Joystick4Button7, 7 },
        {KeyCode.Joystick4Button8, 8 },
        {KeyCode.Joystick4Button9, 9 },
        {KeyCode.Joystick4Button10, 10 },
        {KeyCode.Joystick4Button11, 11 },
        {KeyCode.Joystick4Button12, 12 },
        {KeyCode.Joystick4Button13, 13 },
        {KeyCode.Joystick4Button14, 14 },
        {KeyCode.Joystick4Button15, 15 },
        {KeyCode.Joystick4Button16, 16 },
        {KeyCode.Joystick4Button17, 17 },
        {KeyCode.Joystick4Button18, 18 },
        {KeyCode.Joystick4Button19, 19 }
    };

    public static readonly Dictionary<string, int> axisNumbers = new Dictionary<string, int>() {
        {Tags.Input.Joystick1.HorizontalMovement, 0},
        {Tags.Input.Joystick1.VerticalMovement, 1},
        {Tags.Input.Joystick1.axis3, 2},
        {Tags.Input.Joystick1.axis4, 3},
        {Tags.Input.Joystick1.axis5, 4},
        {Tags.Input.Joystick1.axis6, 5},
        {Tags.Input.Joystick1.axis7, 6},
        {Tags.Input.Joystick1.axis9, 7},
        {Tags.Input.Joystick1.axis10, 8},

        {Tags.Input.Joystick2.HorizontalMovement, 0},
        {Tags.Input.Joystick2.VerticalMovement, 1},
        {Tags.Input.Joystick2.axis3, 2},
        {Tags.Input.Joystick2.axis4, 3},
        {Tags.Input.Joystick2.axis5, 4},
        {Tags.Input.Joystick2.axis6, 5},
        {Tags.Input.Joystick2.axis7, 6},
        {Tags.Input.Joystick2.axis9, 7},
        {Tags.Input.Joystick2.axis10, 8},

        {Tags.Input.Joystick3.HorizontalMovement, 0},
        {Tags.Input.Joystick3.VerticalMovement, 1},
        {Tags.Input.Joystick3.axis3, 2},
        {Tags.Input.Joystick3.axis4, 3},
        {Tags.Input.Joystick3.axis5, 4},
        {Tags.Input.Joystick3.axis6, 5},
        {Tags.Input.Joystick3.axis7, 6},
        {Tags.Input.Joystick3.axis9, 7},
        {Tags.Input.Joystick3.axis10, 8},

        {Tags.Input.Joystick4.HorizontalMovement, 0},
        {Tags.Input.Joystick4.VerticalMovement, 1},
        {Tags.Input.Joystick4.axis3, 2},
        {Tags.Input.Joystick4.axis4, 3},
        {Tags.Input.Joystick4.axis5, 4},
        {Tags.Input.Joystick4.axis6, 5},
        {Tags.Input.Joystick4.axis7, 6},
        {Tags.Input.Joystick4.axis9, 7},
        {Tags.Input.Joystick4.axis10, 8},
    };

    public static readonly Dictionary<int, string> xboxAxisNames = new Dictionary<int, string>() {
        {0, "Left Stick Horizontal"},
        {1, "Left Stick Vertical"},
        {2, "Triggers"},
        {3, "Right Stick Horizontal"},
        {4, "Right Stick Vertical"},
        {5, "D-Pad Horizontal"},
        {6, "D-Pad Horizontal"},
        {7, "LT"},
        {8, "RT"},
    };

    public static readonly Dictionary<int, string> ps4AxisNames = new Dictionary<int, string>() {
        {0, "Left Stick Horizontal"},
        {1, "Left Stick Vertical"},
        {2, "Triggers"},
        {3, "Right Stick Horizontal"},
        {4, "Right Stick Vertical"},
        {5, "D-Pad Horizontal"},
        {6, "D-Pad Horizontal"},
        {7, "LT"},
        {8, "RT"},
    };
}

/// <summary>
/// Interface used to denote a class which is or holds an IPlayerInput.
/// </summary>
public interface IPlayerInputHolder {
    /// <summary>
    /// A vector representing the direction the player should move in. Magnitude should be in the range [0, 1]. Vector is in screen space.
    /// </summary>
    Vector2 movementDirection { get; }
    /// <summary>
    /// A vector representing the direction the player should face. Vector is in world space.
    /// </summary>
    Vector3 rotationDirection { get; }
    /// <summary>
    /// GetKey for the menu submission.
    /// </summary>
    /// <returns></returns>
    bool getSubmit { get; }
    /// <summary>
    /// GetKey for the menu cancellation.
    /// </summary>
    /// <returns></returns>
    bool getCancel { get; }
    /// <summary>
    /// GetKey for the start binding.
    /// </summary>
    /// <returns></returns>
    bool getStart { get; }
    /// <summary>
    /// GetKey for the player's basic attack.
    /// </summary>
    bool getBasicAttack { get; }
    /// <summary>
    /// GetKey for the player's first ability.
    /// </summary>
    /// <returns></returns>
    bool getAbility1 { get; }
    /// <summary>
    /// GetKey for the player's second ability.
    /// </summary>
    /// <returns></returns>
    bool getAbility2 { get; }
    /// <summary>
    /// GetKey for the player's jump.
    /// </summary>
    bool getJump { get; }

    /// <summary>
    /// GetKeyDown for the menu submission.
    /// </summary>
    /// <returns></returns>
    bool getSubmitDown { get; }
    /// <summary>
    /// GetKeyDown for the menu cancellation.
    /// </summary>
    /// <returns></returns>
    bool getCancelDown { get; }
    /// <summary>
    /// GetKeyDown for the start binding.
    /// </summary>
    /// <returns></returns>
    bool getStartDown { get; }
    /// <summary>
    /// GetKeyDown for the player's basic attack.
    /// </summary>
    bool getBasicAttackDown { get; }
    /// <summary>
    /// GetKeyDown for the player's first ability.
    /// </summary>
    /// <returns></returns>
    bool getAbility1Down { get; }
    /// <summary>
    /// GetKeyDown for the player's second ability.
    /// </summary>
    /// <returns></returns>
    bool getAbility2Down { get; }
    /// <summary>
    /// GetKeyDown for the player's jump.
    /// </summary>
    bool getJumpDown { get; }

    /// <summary>
    /// GetKeyUp for the player's basic attack.
    /// </summary>
    bool getBasicAttackUp { get; }
    /// <summary>
    /// GetKeyUp for the player's first ability.
    /// </summary>
    /// <returns></returns>
    bool getAbility1Up { get; }
    /// <summary>
    /// GetKeyUp for the player's second ability.
    /// </summary>
    /// <returns></returns>
    bool getAbility2Up { get; }
    /// <summary>
    /// GetKeyUp for the player's jump.
    /// </summary>
    bool getJumpUp { get; }

    /// <summary>
    /// Get any Key
    /// </summary>
    bool getAnyKey { get; }

    /// <summary>
    /// Get any Key down
    /// </summary>
    bool getAnyKeyDown { get; }

    string submitName { get; }
    string cancelName { get; }
    string startName { get; }
    string basicAttackName { get; }
    string ability1Name { get; }
    string ability2Name { get; }
    string jumpName { get; }
}

/// <summary>
/// This is the script through which all final gameplay input will be handled. Child classes are for drag-drop construction.
/// </summary>
public class PlayerInputHolder : MonoBehaviour, IPlayerInputHolder
{
    private IPlayerInput heldInput;
    public virtual IPlayerInput HeldInput {
        get { return heldInput; }
        set { heldInput = value; Start(); }
    }

    public Vector2 movementDirection { get { return HeldInput.movementDirection; } }
    public Vector3 rotationDirection { get { return HeldInput.rotationDirection; } }

    public bool getSubmit { get { return HeldInput.getSubmit; } }
    public bool getCancel { get { return HeldInput.getCancel; } }
    public bool getStart { get { return HeldInput.getStart; } }
    public bool getBasicAttack { get { return HeldInput.getBasicAttack; } }
    public bool getAbility1 { get { return HeldInput.getAbility1; } }
    public bool getAbility2 { get { return HeldInput.getAbility2; } }
    public bool getJump { get { return HeldInput.getJump; } }

    public bool getSubmitDown { get { return HeldInput.getSubmitDown; } }
    public bool getCancelDown { get { return HeldInput.getCancelDown; } }
    public bool getStartDown { get { return HeldInput.getStartDown; } }
    public bool getBasicAttackDown { get { return HeldInput.getBasicAttackDown; } }
    public bool getAbility1Down { get { return HeldInput.getAbility1Down; } }
    public bool getAbility2Down { get { return HeldInput.getAbility2Down; } }
    public bool getJumpDown { get { return HeldInput.getJumpDown; } }

    public bool getBasicAttackUp { get { return HeldInput.getBasicAttackUp; } }
    public bool getAbility1Up { get { return HeldInput.getAbility1Up; } }
    public bool getAbility2Up { get { return HeldInput.getAbility2Up; } }
    public bool getJumpUp { get { return HeldInput.getJumpUp; } }

    public bool getAnyKey { get { return HeldInput.getAnyKey; } }
    public bool getAnyKeyDown { get { return HeldInput.getAnyKeyDown; } }

    public string submitName { get { return HeldInput.submitName; } }
    public string cancelName { get { return HeldInput.cancelName; } }
    public string startName { get { return HeldInput.startName; } }
    public string basicAttackName { get { return HeldInput.basicAttackName; } }
    public string ability1Name { get { return HeldInput.ability1Name; } }
    public string ability2Name { get { return HeldInput.ability2Name; } }
    public string jumpName { get { return HeldInput.jumpName; } }

    protected void Start() {
        if (HeldInput != null) {
            HeldInput.Initialize(this);
            HeldInput.Player = this.transform;
        }
    }

    void OnDestroy() {
        if (HeldInput != null) {
            HeldInput.Deactivate();
        }
    }

    void Update() {
        if (HeldInput != null) {
            HeldInput.Update();
        }
    }
}

/// <summary>
/// An input which always returns no.
/// </summary>
[System.Serializable]
public class NullInput : IPlayerInput
{
    public Transform Player { set { ;} }
    public void Initialize(MonoBehaviour holder) { }
    public void Deactivate() { }
    public void Update() { }
    public Vector2 movementDirection { get { return Vector2.zero; } }
    public Vector3 rotationDirection { get { return Vector3.zero; } }
    public bool getSubmit { get { return false; } }
    public bool getCancel { get { return false; } }
    public bool getStart { get { return false; } }
    public bool getBasicAttack { get { return false; } }
    public bool getAbility1 { get { return false; } }
    public bool getAbility2 { get { return false; } }
    public bool getJump { get { return false; } }

    public bool getSubmitDown { get { return false; } }
    public bool getCancelDown { get { return false; } }
    public bool getStartDown { get { return false; } }
    public bool getBasicAttackDown { get { return false; } }
    public bool getAbility1Down { get { return false; } }
    public bool getAbility2Down { get { return false; } }
    public bool getJumpDown { get { return false; } }

    public bool getBasicAttackUp { get { return false; } }
    public bool getAbility1Up { get { return false; } }
    public bool getAbility2Up { get { return false; } }
    public bool getJumpUp { get { return false; } }

    public bool getAnyKey { get { return false; } }
    public bool getAnyKeyDown { get { return false; } }

    public string submitName { get{ return ""; } }
    public string cancelName { get{ return ""; } }
    public string startName { get{ return ""; } }
    public string basicAttackName { get{ return ""; } }
    public string ability1Name { get{ return ""; } }
    public string ability2Name { get { return ""; } }
    public string jumpName { get { return ""; } }
}