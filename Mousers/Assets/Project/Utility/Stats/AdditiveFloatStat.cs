using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AdditiveFloatStat : Stat<float>
{

    public AdditiveFloatStat(float baseValue) : base(baseValue) { }

    protected override float Recalculate()
    {
        float result = baseValue;
        foreach (float modifier in modifiers)
        {
            result += modifier;
        }
        return result;
    }

    public static AdditiveFloatStat operator +(AdditiveFloatStat self, float modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static AdditiveFloatStat operator *(AdditiveFloatStat self, float modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static AdditiveFloatStat operator -(AdditiveFloatStat self, float modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }

    public static AdditiveFloatStat operator /(AdditiveFloatStat self, float modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }
}
