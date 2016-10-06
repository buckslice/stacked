using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents an add, which is a spawned unit which is friendly to players, but is not controlled by a person.
/// </summary>
public class Add : Player {

    public virtual bool isPlayer() { return false; }

    [SerializeField]
    protected Player owner;
    public Player Owner { get { return owner; } }

    public override void AddDamageDealt(float damage) { base.AddDamageDealt(damage); owner.AddDamageDealt(damage); }

    /// <summary>
    /// A construtor-like function for setup.
    /// </summary>
    /// <param name="playerReference"></param>
    public void Initialize(Player playerReference) {
        Assert.IsNull(owner, string.Format("Already instantiated with old owner {0} and new owner {1}", owner, playerReference));
        owner = playerReference;
    }
}
