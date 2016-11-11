using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// TODO : Distinguish between damage and healing
/// </summary>
public class HealAction : TypedTargetedAbilityAction {

    IDamageHolder trackerReference;
    public IDamageHolder TrackerReference { get { return trackerReference; } set { trackerReference = value; } }

    [SerializeField]
    protected float healing = 100;

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

            float trueHealing = damageable.Heal(healing, trackerReference);
            stream.SendNext(trueHealing);

        } else {

            float trueHealing = (float)stream.ReceiveNext();
            target.GetComponentInChildren<Damageable>().Heal(trueHealing, trackerReference);
        }
        return true;
    }
}
