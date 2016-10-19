using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;



/// <summary>
/// Identifies the object as a player
/// </summary>
[System.Serializable]
public class Boss : AbstractDamageTracker {

    static List<Boss> bossList = new List<Boss>();
    public static List<Boss> Bosses { get { return bossList; } }
    public Boss(IDamageHolder holder)
        : base(holder) {
            bossList.Add(this);
    }

    public override void Destroy() {
        bossList.Remove(this);
    }
}
