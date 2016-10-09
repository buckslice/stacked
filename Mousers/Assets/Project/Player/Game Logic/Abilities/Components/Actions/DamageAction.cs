﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DamageAction : TypedTargetedAbilityAction {

    //TODO: refactor these to be more universal
    IDamageTracker trackerReference;

    //TODO; refactor to include damage type
    [SerializeField]
    protected Damage damage = 100;

    protected override void Start() {
        base.Start();
        trackerReference = GetComponentInParent<IDamageTracker>();

        if (trackerReference == null) {
            PhotonView view = GetComponentInParent<PhotonView>();
            trackerReference = PhotonView.Find((int)view.instantiationData[0]).GetComponentInChildren<IDamageTracker>();
        }
        Assert.IsNotNull(trackerReference);
    }

    public override bool isAbilityActivatible(GameObject target) {
        return target.GetComponent<Damageable>() != null;
    }

    public override bool Activate(GameObject target, PhotonStream stream) {
        target.GetComponent<Damageable>().Damage(damage, trackerReference);
        return true;
    }
}
