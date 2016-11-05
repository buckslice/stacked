using UnityEngine;
using System.Collections;


// for the autocomplete!

public static class Tags {

    [System.Serializable]
    public class TagsMask {
        [SerializeField]
        protected int mask = 0;
        public int Mask { get { return mask; } }
    }

    public const string Player = "Player";
    public const string Boss = "Boss";
    /// <summary>
    /// The only object which should be tagged with this tag is the root gameobject of the UI canvas tree.
    /// </summary>
    public const string CanvasRoot = "CanvasRoot";

    public static readonly string[] tagsArray = new string[] { Player, Boss, CanvasRoot };

    public enum EventCodes : byte
    {
        CREATEREMOTECHARACTERSELECTCURSOR,
        CREATEPLAYER,
        CREATEADD,
        REQUESTREGISTRATION,
        CREATEREGISTRATION,
        CREATEBOSS,
    };

    public static class Input
    {
        public const string Horizontal = "HorizontalKeyboard";
        public const string Vertical = "VerticalKeyboard";

        // derek: we should just redo all these keycodes to be in the input axes
        // that way we can choose multiple options for submit or cancel buttons
        // and also have all the keybindings settings in one spot
        public const KeyCode Submit = KeyCode.Space;
        public const KeyCode Cancel1 = KeyCode.Escape;
        public const KeyCode Cancel2 = KeyCode.Backspace;
        public const KeyCode Start = KeyCode.Return;
        public const KeyCode BasicAttack = KeyCode.LeftShift; 
        public const KeyCode Ability1 = KeyCode.Q; 
        public const KeyCode Ability2 = KeyCode.E;
        public const KeyCode Jump = KeyCode.Space;

        public enum axes {
            HorizontalMovement,
            VerticalMovement,
            HorizontalAiming,
            VerticalAiming,
            LeftTrigger,
            RightTrigger
        };

        public static class Joystick1
        {
            public const KeyCode button0 = KeyCode.Joystick1Button0;
            public const KeyCode button1 = KeyCode.Joystick1Button1;
            public const KeyCode button2 = KeyCode.Joystick1Button2;
            public const KeyCode button3 = KeyCode.Joystick1Button3;
            public const KeyCode button4 = KeyCode.Joystick1Button4;
            public const KeyCode button5 = KeyCode.Joystick1Button5;
            public const KeyCode button6 = KeyCode.Joystick1Button6;
            public const KeyCode button7 = KeyCode.Joystick1Button7;
            public const KeyCode button8 = KeyCode.Joystick1Button8;
            public const KeyCode button9 = KeyCode.Joystick1Button9;
            public const KeyCode button10 = KeyCode.Joystick1Button10;
            public const KeyCode button11 = KeyCode.Joystick1Button11;
            public const KeyCode button12 = KeyCode.Joystick1Button12;
            public const KeyCode button13 = KeyCode.Joystick1Button13;
            public const KeyCode button14 = KeyCode.Joystick1Button14;
            public const KeyCode button15 = KeyCode.Joystick1Button15;
            public const KeyCode button16 = KeyCode.Joystick1Button16;
            public const KeyCode button17 = KeyCode.Joystick1Button17;
            public const KeyCode button18 = KeyCode.Joystick1Button18;
            public const KeyCode button19 = KeyCode.Joystick1Button19;

            public const string HorizontalMovement = "J1_HorizontalMovement";
            public const string VerticalMovement = "J1_VerticalMovement";
            public const string HorizontalAiming = "J1_HorizontalAiming";
            public const string VerticalAiming = "J1_VerticalAiming";
            public const string LeftTrigger = "J1_LeftTrigger";
            public const string RightTrigger = "J1_RightTrigger";

            public static readonly KeyCode[] allButtons = { button0, button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19 };
            public static readonly string[] allAxes = { HorizontalMovement, VerticalMovement, HorizontalAiming, VerticalAiming, LeftTrigger, RightTrigger };
        }
        public static class Joystick2
        {
            public const KeyCode button0 = KeyCode.Joystick2Button0;
            public const KeyCode button1 = KeyCode.Joystick2Button1;
            public const KeyCode button2 = KeyCode.Joystick2Button2;
            public const KeyCode button3 = KeyCode.Joystick2Button3;
            public const KeyCode button4 = KeyCode.Joystick2Button4;
            public const KeyCode button5 = KeyCode.Joystick2Button5;
            public const KeyCode button6 = KeyCode.Joystick2Button6;
            public const KeyCode button7 = KeyCode.Joystick2Button7;
            public const KeyCode button8 = KeyCode.Joystick2Button8;
            public const KeyCode button9 = KeyCode.Joystick2Button9;
            public const KeyCode button10 = KeyCode.Joystick2Button10;
            public const KeyCode button11 = KeyCode.Joystick2Button11;
            public const KeyCode button12 = KeyCode.Joystick2Button12;
            public const KeyCode button13 = KeyCode.Joystick2Button13;
            public const KeyCode button14 = KeyCode.Joystick2Button14;
            public const KeyCode button15 = KeyCode.Joystick2Button15;
            public const KeyCode button16 = KeyCode.Joystick2Button16;
            public const KeyCode button17 = KeyCode.Joystick2Button17;
            public const KeyCode button18 = KeyCode.Joystick2Button18;
            public const KeyCode button19 = KeyCode.Joystick2Button19;

            public const string HorizontalMovement = "J2_HorizontalMovement";
            public const string VerticalMovement = "J2_VerticalMovement";
            public const string HorizontalAiming = "J2_HorizontalAiming";
            public const string VerticalAiming = "J2_VerticalAiming";
            public const string LeftTrigger = "J2_LeftTrigger";
            public const string RightTrigger = "J2_RightTrigger";

            public static readonly KeyCode[] allButtons = { button0, button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19 };
            public static readonly string[] allAxes = { HorizontalMovement, VerticalMovement, HorizontalAiming, VerticalAiming, LeftTrigger, RightTrigger };
        }
        public static class Joystick3
        {
            public const KeyCode button0 = KeyCode.Joystick3Button0;
            public const KeyCode button1 = KeyCode.Joystick3Button1;
            public const KeyCode button2 = KeyCode.Joystick3Button2;
            public const KeyCode button3 = KeyCode.Joystick3Button3;
            public const KeyCode button4 = KeyCode.Joystick3Button4;
            public const KeyCode button5 = KeyCode.Joystick3Button5;
            public const KeyCode button6 = KeyCode.Joystick3Button6;
            public const KeyCode button7 = KeyCode.Joystick3Button7;
            public const KeyCode button8 = KeyCode.Joystick3Button8;
            public const KeyCode button9 = KeyCode.Joystick3Button9;
            public const KeyCode button10 = KeyCode.Joystick3Button10;
            public const KeyCode button11 = KeyCode.Joystick3Button11;
            public const KeyCode button12 = KeyCode.Joystick3Button12;
            public const KeyCode button13 = KeyCode.Joystick3Button13;
            public const KeyCode button14 = KeyCode.Joystick3Button14;
            public const KeyCode button15 = KeyCode.Joystick3Button15;
            public const KeyCode button16 = KeyCode.Joystick3Button16;
            public const KeyCode button17 = KeyCode.Joystick3Button17;
            public const KeyCode button18 = KeyCode.Joystick3Button18;
            public const KeyCode button19 = KeyCode.Joystick3Button19;

            public const string HorizontalMovement = "J3_HorizontalMovement";
            public const string VerticalMovement = "J3_VerticalMovement";
            public const string HorizontalAiming = "J3_HorizontalAiming";
            public const string VerticalAiming = "J3_VerticalAiming";
            public const string LeftTrigger = "J3_LeftTrigger";
            public const string RightTrigger = "J3_RightTrigger";

            public static readonly KeyCode[] allButtons = { button0, button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19 };
            public static readonly string[] allAxes = { HorizontalMovement, VerticalMovement, HorizontalAiming, VerticalAiming, LeftTrigger, RightTrigger };
        }
        public static class Joystick4
        {
            public const KeyCode button0 = KeyCode.Joystick4Button0;
            public const KeyCode button1 = KeyCode.Joystick4Button1;
            public const KeyCode button2 = KeyCode.Joystick4Button2;
            public const KeyCode button3 = KeyCode.Joystick4Button3;
            public const KeyCode button4 = KeyCode.Joystick4Button4;
            public const KeyCode button5 = KeyCode.Joystick4Button5;
            public const KeyCode button6 = KeyCode.Joystick4Button6;
            public const KeyCode button7 = KeyCode.Joystick4Button7;
            public const KeyCode button8 = KeyCode.Joystick4Button8;
            public const KeyCode button9 = KeyCode.Joystick4Button9;
            public const KeyCode button10 = KeyCode.Joystick4Button10;
            public const KeyCode button11 = KeyCode.Joystick4Button11;
            public const KeyCode button12 = KeyCode.Joystick4Button12;
            public const KeyCode button13 = KeyCode.Joystick4Button13;
            public const KeyCode button14 = KeyCode.Joystick4Button14;
            public const KeyCode button15 = KeyCode.Joystick4Button15;
            public const KeyCode button16 = KeyCode.Joystick4Button16;
            public const KeyCode button17 = KeyCode.Joystick4Button17;
            public const KeyCode button18 = KeyCode.Joystick4Button18;
            public const KeyCode button19 = KeyCode.Joystick4Button19;

            public const string HorizontalMovement = "J4_HorizontalMovement";
            public const string VerticalMovement = "J4_VerticalMovement";
            public const string HorizontalAiming = "J4_HorizontalAiming";
            public const string VerticalAiming = "J4_VerticalAiming";
            public const string LeftTrigger = "J4_LeftTrigger";
            public const string RightTrigger = "J4_RightTrigger";

            public static readonly KeyCode[] allButtons = { button0, button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19 };
            public static readonly string[] allAxes = { HorizontalMovement, VerticalMovement, HorizontalAiming, VerticalAiming, LeftTrigger, RightTrigger };
        }
    }

    public static class UIPaths
    {
        /// <summary>
        /// Path from the canvas root to the layout group for player health bars.
        /// </summary>
        public const string PlayerRegistrationGroup = "PlayerRegistrationGroup";
    }

    public static class LayerNumbers
    {
    }

    public static class Layers
    {
        public const string Default = "Default";
        public const string Player = "Player";
        public const string StaticGeometry = "Static Geometry";
        public const string Enemy = "Enemy";
        public const string Boss = "Boss";
    }

    public class SortingLayers
    {
        public const string Overlay = "Overlay";
    }

    public static class AnimatorParams
    {
    }

    public static class PlayerPrefKeys
    {
    }

    public static class Resources
    {
        public const string Player = "Player";
        public const string Cursor = "PlaceholderCursor";
        public const string Boss = "Boss"; // this is temp, later we will have specific actual boss prefabs for each boss
        public const string RegistrationUI = "RegistrationUI";
        public const string PlayerHealthBar = "PlayerHealthBar";
        public const string BossHealthBar = "BossHealthBar";
        public const string FloatingHealthBar = "FloatingHealthBar";
    }

    public static class Options
    {
        public const string SoundLevel = "SoundLevel";
        public const string MusicLevel = "MusicLevel";
    }

    public static class ShaderParams
    {
        public static int color = Shader.PropertyToID("_Color");
        public static int emission = Shader.PropertyToID("_EmissionColor");
        public static int cutoff = Shader.PropertyToID("_Cutoff");
        public static int noiseStrength = Shader.PropertyToID("_NoiseStrength");
        public static int effectTexture = Shader.PropertyToID("_EffectTex");
        public static int rangeMin = Shader.PropertyToID("_RangeMin");
        public static int rangeMax = Shader.PropertyToID("_RangeMax");
        public static int imageStrength = Shader.PropertyToID("_ImageStrength");
        public static int alpha = Shader.PropertyToID("_MainTexAlpha");
    }

    public static class Scenes
    {
        public const string CharacterSelect = "CharacterSelect";
        public const string PlayerRegistration = "PlayerRegistration";
        public const string BossSelect = "BossSelect";
        public const string DefeatPopup = "DefeatPopup";
        public const string VictoryPopup = "VictoryPopup";
        public const string MainMenu = "MainMenu";
        public const string Options = "Options";
    }
}

public static class TagsMaskExtension {
    public static bool CompareTag(this Component component, Tags.TagsMask mask) {
        for (int i = 0; i < Tags.tagsArray.Length; i++) {
            if ((1 << i & mask.Mask) > 0) {
                if (component.CompareTag(Tags.tagsArray[i])) {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool CompareTag(this GameObject gameObject, Tags.TagsMask mask) {
        for (int i = 0; i < Tags.tagsArray.Length; i++) {
            if (((1 << i) & mask.Mask) > 0) {
                if (gameObject.CompareTag(Tags.tagsArray[i])) {
                    return true;
                }
            }
        }
        return false;
    }
}