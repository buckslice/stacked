using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AdditiveIntStat : Stat<int>
{

    public AdditiveIntStat(int baseValue) : base(baseValue) { }

    protected override int Recalculate()
    {
        int result = baseValue;
        foreach (int modifier in modifiers)
        {
            result += modifier;
        }
        return result;
    }

    public static AdditiveIntStat operator +(AdditiveIntStat self, int modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static AdditiveIntStat operator *(AdditiveIntStat self, int modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static AdditiveIntStat operator -(AdditiveIntStat self, int modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }

    public static AdditiveIntStat operator /(AdditiveIntStat self, int modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }
}
