using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An action which spawns a pooled object
/// </summary>
public class SpawnAddAbility : AbstractAbilityAction {

    [SerializeField]
    protected PlayerSetup.PlayerSetupData addData;

    public override void Activate() {
        GameObject createdPlayer = PlayerSetupNetworkedData.Main.CreatePlayer((byte)Player.getFirstFreePlayerID(), new NullInput(), addData);
        createdPlayer.GetComponent<Rigidbody>().position = this.transform.position;
    }

    public override void ActivateWithRemoteData(object data) {
        Activate();
    }

    public override void ActivateRemote() {
    }
}
