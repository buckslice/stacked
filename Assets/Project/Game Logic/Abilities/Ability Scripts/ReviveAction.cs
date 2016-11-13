using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class ReviveAction : TargetedAbilityAction {

    [SerializeField]
    protected float reviveHealing = 10;

    IDamageHolder trackerReference;

    protected override void Start() {
        base.Start();
        trackerReference = GetComponentInParent<IDamageHolder>();
    }

    public override bool Activate(GameObject context, PhotonStream stream) {
        Health health = context.GetComponentInParent<Health>();
        float amount;

        if (stream.isWriting) {
            amount = health.Heal(reviveHealing, trackerReference.GetRootDamageTracker());
            stream.SendNext(amount);
        } else {
            amount = (float)stream.ReceiveNext();
            health.Heal(reviveHealing, trackerReference.GetRootDamageTracker());
        }

        return true;
    }
}
