using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MultiplierFloatStat : Stat<float>
{

    public MultiplierFloatStat(float baseValue) : base(baseValue) { }

    protected override float Recalculate()
    {
        float result = baseValue;
        foreach (float modifier in modifiers)
        {
            result *= modifier;
        }
        return result;
    }

    public static MultiplierFloatStat operator +(MultiplierFloatStat self, float modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static MultiplierFloatStat operator *(MultiplierFloatStat self, float modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static MultiplierFloatStat operator -(MultiplierFloatStat self, float modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }

    public static MultiplierFloatStat operator /(MultiplierFloatStat self, float modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }
}
