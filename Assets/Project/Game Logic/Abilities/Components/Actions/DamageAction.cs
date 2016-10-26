using UnityEngine;
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
    protected Damage damage = 100;

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
        Debug.Log(target, target);
        return target.GetComponent<Damageable>() != null;
    }

    public override bool Activate(GameObject target, PhotonStream stream) {
        target.GetComponent<Damageable>().Damage(damage, trackerReference);
        return true;
    }
}
