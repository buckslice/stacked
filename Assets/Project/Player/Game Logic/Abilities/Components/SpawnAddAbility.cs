using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An action which spawns a pooled object
/// </summary>
public class SpawnAddAbility : AbstractAbilityAction {

    IDamageHolder trackerReference;

    [SerializeField]
    protected PlayerSetup.PlayerSetupData addData;

    protected override void Start() {
        base.Start();
        trackerReference = GetComponentInParent<IDamageHolder>();
    }

    public override bool Activate(PhotonStream stream) {
        Player owner = (Player)trackerReference.GetRootDamageTracker();
        GameObject createdPlayer = PlayerSetupNetworkedData.Main.CreateAdd((byte)owner.PlayerID, new NullInput(), addData);
        createdPlayer.GetComponent<Rigidbody>().position = this.transform.position;
        return false;
    }
}
