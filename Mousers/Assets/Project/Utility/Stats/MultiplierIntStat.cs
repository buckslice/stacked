using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MultiplierIntStat : Stat<int>
{

    public MultiplierIntStat(int baseValue) : base(baseValue) { }

    protected override int Recalculate()
    {
        int result = baseValue;
        foreach (int modifier in modifiers)
        {
            result *= modifier;
        }
        return result;
    }

    public static MultiplierIntStat operator +(MultiplierIntStat self, int modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static MultiplierIntStat operator *(MultiplierIntStat self, int modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static MultiplierIntStat operator -(MultiplierIntStat self, int modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }

    public static MultiplierIntStat operator /(MultiplierIntStat self, int modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }
}
