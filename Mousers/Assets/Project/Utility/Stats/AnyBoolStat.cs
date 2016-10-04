using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AnyBoolStat : Stat<bool>
{

    public AnyBoolStat(bool baseValue) : base(baseValue) { }

    protected override bool Recalculate()
    {
        bool result = baseValue;
        foreach (bool modifier in modifiers)
        {
            if (result)
            {
                return result;
            }
            result = result || modifier;
        }
        return result;
    }

    public static AnyBoolStat operator +(AnyBoolStat self, bool modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static AnyBoolStat operator *(AnyBoolStat self, bool modifier)
    {
        self.AddModifier(modifier);
        return self;
    }

    public static AnyBoolStat operator -(AnyBoolStat self, bool modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }

    public static AnyBoolStat operator /(AnyBoolStat self, bool modifier)
    {
        self.RemoveModifier(modifier);
        return self;
    }
}
