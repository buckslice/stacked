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
}

/// <summary>
/// Contains extension methods for PlayerInput.
/// </summary>
public static class PlayerInputExtension
{
    public static bool AnyKey(this IPlayerInputHolder self)
    {
        return self.getBasicAttack ||
            self.getAbility1 ||
            self.getAbility2 ||
            self.getSubmit ||
            self.getStart;
    }

    public static bool AnyAxis(this IPlayerInputHolder self)
    {
        return self.movementDirection != Vector2.zero ||
            self.rotationDirection != Vector3.zero;
    }

    /*
     * currently screwed up by mouse, which never has zero for rotation (cursor) input
    public static bool AnyInput(this IPlayerInputHolder self)
    {
        return self.AnyKey() || self.AnyAxis();
    }
     * */

    public static string getBindingName(KeyCode keycode) {
        if (bindingNames.ContainsKey(keycode)) {
            return bindingNames[keycode];
        } else {
            return "";
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
        
        {KeyCode.JoystickButton7, "STA"},
        {KeyCode.Joystick1Button7, "STA"},
        {KeyCode.Joystick2Button7, "STA"},
        {KeyCode.Joystick3Button7, "STA"},
        {KeyCode.Joystick4Button7, "STA"},
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
        {Tags.Input.Joystick1.HorizontalAiming, 2},
        {Tags.Input.Joystick1.VerticalAiming, 3},
        {Tags.Input.Joystick1.LeftTrigger, 4},
        {Tags.Input.Joystick1.RightTrigger, 5},

        {Tags.Input.Joystick2.HorizontalMovement, 0},
        {Tags.Input.Joystick2.VerticalMovement, 1},
        {Tags.Input.Joystick2.HorizontalAiming, 2},
        {Tags.Input.Joystick2.VerticalAiming, 3},
        {Tags.Input.Joystick2.LeftTrigger, 4},
        {Tags.Input.Joystick2.RightTrigger, 5},

        {Tags.Input.Joystick3.HorizontalMovement, 0},
        {Tags.Input.Joystick3.VerticalMovement, 1},
        {Tags.Input.Joystick3.HorizontalAiming, 2},
        {Tags.Input.Joystick3.VerticalAiming, 3},
        {Tags.Input.Joystick3.LeftTrigger, 4},
        {Tags.Input.Joystick3.RightTrigger, 5},

        {Tags.Input.Joystick4.HorizontalMovement, 0},
        {Tags.Input.Joystick4.VerticalMovement, 1},
        {Tags.Input.Joystick4.HorizontalAiming, 2},
        {Tags.Input.Joystick4.VerticalAiming, 3},
        {Tags.Input.Joystick4.LeftTrigger, 4},
        {Tags.Input.Joystick4.RightTrigger, 5},
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

    string submitName { get; }
    string cancelName { get; }
    string startName { get; }
    string basicAttackName { get; }
    string ability1Name { get; }
    string ability2Name { get; }
}

/// <summary>
/// This is the script through which all final gameplay input will be handled. Child classes are for drag-drop construction.
/// </summary>
public class PlayerInputHolder : MonoBehaviour, IPlayerInputHolder
{
    private IPlayerInput heldInput;
    public virtual IPlayerInput HeldInput {
        get { return heldInput; }
        set { heldInput = value; heldInput.Initialize(this); heldInput.Player = this.transform; }
    }

    public Vector2 movementDirection { get { return HeldInput.movementDirection; } }
    public Vector3 rotationDirection { get { return HeldInput.rotationDirection; } }

    public bool getSubmit { get { return HeldInput.getSubmit; } }
    public bool getCancel { get { return HeldInput.getCancel; } }
    public bool getStart { get { return HeldInput.getStart; } }
    public bool getBasicAttack { get { return HeldInput.getBasicAttack; } }
    public bool getAbility1 { get { return HeldInput.getAbility1; } }
    public bool getAbility2 { get { return HeldInput.getAbility2; } }

    public bool getSubmitDown { get { return HeldInput.getSubmitDown; } }
    public bool getCancelDown { get { return HeldInput.getCancelDown; } }
    public bool getStartDown { get { return HeldInput.getStartDown; } }
    public bool getBasicAttackDown { get { return HeldInput.getBasicAttackDown; } }
    public bool getAbility1Down { get { return HeldInput.getAbility1Down; } }
    public bool getAbility2Down { get { return HeldInput.getAbility2Down; } }

    public bool getBasicAttackUp { get { return HeldInput.getBasicAttackUp; } }
    public bool getAbility1Up { get { return HeldInput.getAbility1Up; } }
    public bool getAbility2Up { get { return HeldInput.getAbility2Up; } }

    public string submitName { get { return HeldInput.submitName; } }
    public string cancelName { get { return HeldInput.cancelName; } }
    public string startName { get { return HeldInput.startName; } }
    public string basicAttackName { get { return HeldInput.basicAttackName; } }
    public string ability1Name { get { return HeldInput.ability1Name; } }
    public string ability2Name { get { return HeldInput.ability2Name; } }

    void OnDestroy() {
        if (heldInput != null) {
            heldInput.Deactivate();
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
    public Vector2 movementDirection { get { return Vector2.zero; } }
    public Vector3 rotationDirection { get { return Vector3.zero; } }
    public bool getSubmit { get { return false; } }
    public bool getCancel { get { return false; } }
    public bool getStart { get { return false; } }
    public bool getBasicAttack { get { return false; } }
    public bool getAbility1 { get { return false; } }
    public bool getAbility2 { get { return false; } }

    public bool getSubmitDown { get { return false; } }
    public bool getCancelDown { get { return false; } }
    public bool getStartDown { get { return false; } }
    public bool getBasicAttackDown { get { return false; } }
    public bool getAbility1Down { get { return false; } }
    public bool getAbility2Down { get { return false; } }

    public bool getBasicAttackUp { get { return false; } }
    public bool getAbility1Up { get { return false; } }
    public bool getAbility2Up { get { return false; } }

    public string submitName { get{ return ""; } }
    public string cancelName { get{ return ""; } }
    public string startName { get{ return ""; } }
    public string basicAttackName { get{ return ""; } }
    public string ability1Name { get{ return ""; } }
    public string ability2Name { get { return ""; } }
}