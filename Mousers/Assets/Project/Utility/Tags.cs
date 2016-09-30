using UnityEngine;
using System.Collections;


// for the autocomplete!

public static class Tags{
    public const string player = "Player";

    //public static class Axis
    //{
    //    //public const string horizontal = "Horizontal";
    //    //public const string vertical = "Vertical";
    //}

    public static class LayerNumbers
    {
        public const int Static = 18;
    }

    public static class Layers
    {
        public const string Static = "Static Geometry";
    }

    public class SortingLayers
    {
        public const string overlay = "Overlay";
    }

    public static class AnimatorParams
    {
    }

    public static class PlayerPrefKeys
    {
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