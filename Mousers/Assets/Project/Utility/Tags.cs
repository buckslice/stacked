using UnityEngine;
using System.Collections;


// for the autocomplete!

public static class Tags {
    public const string Player = "Player";
    public const string Boss = "Boss";
    /// <summary>
    /// The only object which should be tagged with this tag is the root gameobject of the UI canvas tree.
    /// </summary>
    public const string CanvasRoot = "CanvasRoot";

    public static class Input
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";

        public const KeyCode Registering = KeyCode.Space;

        public static class Joystick1
        {
            public const string HorizontalMovement = "J1_HorizontalMovement";
            public const string VerticalMovement = "J1_VerticalMovement";
            public const string HorizontalAiming = "J1_HorizontalAiming";
            public const string VerticalAiming = "J1_VerticalAiming";

            public const KeyCode Registering = KeyCode.Joystick1Button0; //A
        }
        public static class Joystick2
        {
            public const string HorizontalMovement = "J2_HorizontalMovement";
            public const string VerticalMovement = "J2_VerticalMovement";
            public const string HorizontalAiming = "J2_HorizontalAiming";
            public const string VerticalAiming = "J2_VerticalAiming";

            public const KeyCode Registering = KeyCode.Joystick2Button0; //A
        }
        public static class Joystick3
        {
            public const string HorizontalMovement = "J3_HorizontalMovement";
            public const string VerticalMovement = "J3_VerticalMovement";
            public const string HorizontalAiming = "J3_HorizontalAiming";
            public const string VerticalAiming = "J3_VerticalAiming";

            public const KeyCode Registering = KeyCode.Joystick3Button0; //A
        }
        public static class Joystick4
        {
            public const string HorizontalMovement = "J4_HorizontalMovement";
            public const string VerticalMovement = "J4_VerticalMovement";
            public const string HorizontalAiming = "J4_HorizontalAiming";
            public const string VerticalAiming = "J4_VerticalAiming";

            public const KeyCode Registering = KeyCode.Joystick4Button0; //A
        }
    }

    public static class UIPaths
    {
        /// <summary>
        /// Path from the canvas root to the layout group for player health bars.
        /// </summary>
        public const string PlayerHealthBarGroup = "PlayerHealthBarGroup";
    }

    public static class LayerNumbers
    {
    }

    public static class Layers
    {
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
        public const string Boss = "Boss"; // this is temp, later we will have specific actual boss prefabs for each boss
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
}