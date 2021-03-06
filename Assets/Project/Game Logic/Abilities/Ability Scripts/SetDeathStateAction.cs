﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class SetDeathStateAction : UntargetedAbilityConstraint {

    [SerializeField]
    protected bool outcomeState;

    EntityUIGroupHolder uiHolder;

    IDamageHolder playerHolder;
    Player player { get { return playerHolder.GetRootDamageTracker() as Player; } }

    protected override void Start() {
        base.Start();
        playerHolder = GetComponentInParent<IDamageHolder>();
        uiHolder = GetComponentInParent<EntityUIGroupHolder>();
    }

    public override bool isAbilityActivatible() {
        return player.dead != outcomeState;
    }

    public override void Activate() {
        player.dead = outcomeState;
        uiHolder.SetStatusActive(!outcomeState);
    }
}
