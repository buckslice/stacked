using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class SetDeathStateAction : UntargetedAbilityConstraint {

    [SerializeField]
    protected bool outcomeState;

    IDamageHolder playerHolder;
    Player player { get { return playerHolder.GetRootDamageTracker() as Player; } }

    protected override void Start() {
        base.Start();
        playerHolder = GetComponentInParent<IDamageHolder>();
    }

    public override bool isAbilityActivatible() {
        return player.dead != outcomeState;
    }

    public override void Activate() {
        player.dead = outcomeState;
        GetComponentInParent<Rigidbody>().isKinematic = true;   // so player doesnt fall through ground while dead 
    }
}
