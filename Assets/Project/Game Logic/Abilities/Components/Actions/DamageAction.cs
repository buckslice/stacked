﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// TODO : Distinguish between damage and healing
/// </summary>
public class DamageAction : TypedTargetedAbilityAction {

    IDamageHolder trackerReference;
    public IDamageHolder TrackerReference { get { return trackerReference; } set { trackerReference = value; } }

    [SerializeField]
    protected Damage damage = new Damage(100, Damage.DamageType.MAGICAL);

    protected override void Start() {
        base.Start();

        if (trackerReference == null) {
            trackerReference = GetComponentInParent<IDamageHolder>();

            if (trackerReference == null) {
                //if not found by the GetComponent
                PhotonView view = GetComponentInParent<PhotonView>();
                if (view.instantiationData.Length > 0) {
                    trackerReference = PhotonView.Find((int)view.instantiationData[0]).GetComponentInChildren<IDamageTracker>();
                }
            }
        }

        
        Assert.IsNotNull(trackerReference);
    }

    public override bool isAbilityActivatible(GameObject target) {
        return target.GetComponent<Damageable>() != null;
    }

    public override bool Activate(GameObject target, PhotonStream stream) {
        if (stream.isWriting) {
            Damageable damageable = target.GetComponent<Damageable>();
            if (damageable == null) { return false; }

            float trueDamage = damageable.Damage(damage, trackerReference);
            stream.SendNext(trueDamage);
        } else {
            float trueDamage = (float)stream.ReceiveNext();
            Damage ourDamage = new Damage(trueDamage, damage.Type);

            target.GetComponentInChildren<Damageable>().Damage(ourDamage, trackerReference, trueDamage: true);
        }
        return true;
    }
}
