using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;
#endif

public interface IBalanceStat {
    void setValue(float value, BalanceStat.StatType type);
}

/// <summary>
/// General system for implementing one-spot balance changes.
/// </summary>
/// <typeparam name="T"></typeparam>
[ExecuteInEditMode]
public abstract class BalanceStat : MonoBehaviour, IBalanceStat {

    public enum StatType {
        ANY,
        ANGLE,
        COOLDOWN,
        DAMAGE,
        DURATION,
        POWER,
        RADIUS,
        RANGE,
    }

    [SerializeField]
    protected float value = 0;

    [SerializeField]
    protected BalanceStatReference[] targets;

#if UNITY_EDITOR
    void Update() {
        if (EditorApplication.isPlaying) { this.enabled = false; }

        if (targets == null) { return; }

        foreach (BalanceStatReference target in targets) {
            if (target == null || target.Target == null) {
                break;
            }

            target.Target.setValue(target.Evaulate(value), type);
        }
    }
#endif

    protected abstract StatType type { get; }

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch (type) {
            case BalanceStat.StatType.ANY:
            default:
                this.value = value; Update();
                break;
        }
    }
}

[System.Serializable]
public class BalanceStatReference {

    const string numberPattern = @"(-?\d+\.?\d*)";
    const string parenPattern = @"(\([^\(\)]*\))";

    static readonly Regex powRegex = new Regex(numberPattern + @"\^" + numberPattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
    static readonly Regex mulRegex = new Regex(numberPattern + @"\*" + numberPattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
    static readonly Regex divRegex = new Regex(numberPattern + "/" + numberPattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
    static readonly Regex addRegex = new Regex(numberPattern + @"\+" + numberPattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
    static readonly Regex subRegex = new Regex(numberPattern + "-" + numberPattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
    static readonly Regex parenRegex = new Regex(parenPattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

    [SerializeField]
    private MonoBehaviour target;
    public IBalanceStat Target { 
        get { 
            if (target == null) { return null; }
            IBalanceStat result = (IBalanceStat)(target as object);
            return result;
        }
    }

    [SerializeField]
    protected string function = "x";

    float EvaluateWithoutParens(string input) {
        Assert.IsFalse(input.Contains("(") || input.Contains(")"));

        int startIndex;
        Match match;

        startIndex = 0;
        match = powRegex.Match(input, startIndex);
        while (match.Success) {
            float first = float.Parse(match.Groups[1].Value);
            float second = float.Parse(match.Groups[2].Value);
            float computation = Mathf.Pow(first, second);
            input = input.Substring(0, match.Index) + computation + input.Substring(match.Index + match.Length);
            startIndex = match.Index;
            match = powRegex.Match(input, startIndex);
        }

        startIndex = 0;
        match = mulRegex.Match(input, startIndex);
        while (match.Success) {
            float first = float.Parse(match.Groups[1].Value);
            float second = float.Parse(match.Groups[2].Value);
            float computation = first * second;
            input = input.Substring(0, match.Index) + computation + input.Substring(match.Index + match.Length);
            startIndex = match.Index;
            match = mulRegex.Match(input, startIndex);
        }

        startIndex = 0;
        match = divRegex.Match(input, startIndex);
        while (match.Success) {
            float first = float.Parse(match.Groups[1].Value);
            float second = float.Parse(match.Groups[2].Value);
            float computation = first / second;
            input = input.Substring(0, match.Index) + computation + input.Substring(match.Index + match.Length);
            startIndex = match.Index;
            match = divRegex.Match(input, startIndex);
        }

        startIndex = 0;
        match = addRegex.Match(input, startIndex);
        while (match.Success) {
            float first = float.Parse(match.Groups[1].Value);
            float second = float.Parse(match.Groups[2].Value);
            float computation = first + second;
            input = input.Substring(0, match.Index) + computation + input.Substring(match.Index + match.Length);
            startIndex = match.Index;
            match = addRegex.Match(input, startIndex);
        }

        startIndex = 0;
        match = subRegex.Match(input, startIndex);
        while (match.Success) {
            float first = float.Parse(match.Groups[1].Value);
            float second = float.Parse(match.Groups[2].Value);
            float computation = first - second;
            input = input.Substring(0, match.Index) + computation + input.Substring(match.Index + match.Length);
            startIndex = match.Index;
            match = subRegex.Match(input, startIndex);
        }

        return float.Parse(input);
    }

    public float Evaulate(float value) {

        if (string.IsNullOrEmpty(function)) { function = "x"; }

        string inputString = function.Replace("x", value.ToString());

        if (inputString.Replace("(", "").Length != inputString.Replace(")", "").Length) {
            //if parens are imbalanced (see http://stackoverflow.com/questions/541954/how-would-you-count-occurrences-of-a-string-within-a-string)
            //then remove all parens
            inputString = inputString.Replace("(", "").Replace(")", "");
        }

        Match match = parenRegex.Match(inputString);
        while (match.Success) {
            string parenInterior = match.Value;
            parenInterior = parenInterior.Replace("(", "").Replace(")", "");
            float computation = EvaluateWithoutParens(parenInterior);
            inputString = inputString.Substring(0, match.Index) + computation + inputString.Substring(match.Index + match.Length);
            match = parenRegex.Match(inputString);
        }

        return EvaluateWithoutParens(inputString);
    }
}