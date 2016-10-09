using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AllBoolStat : Stat<bool> {

    public AllBoolStat(bool baseValue) : base(baseValue) { }

    protected override bool Recalculate()
    {
        bool result = baseValue;
        foreach (bool modifier in modifiers)
        {
            if (!result)
            {
                return result;
            }
            result = result && modifier;
        }
        return result;
    }

    public static AllBoolStat operator +(AllBoolStat self, bool modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static AllBoolStat operator *(AllBoolStat self, bool modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static AllBoolStat operator -(AllBoolStat self, bool modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }

    public static AllBoolStat operator /(AllBoolStat self, bool modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }
}
