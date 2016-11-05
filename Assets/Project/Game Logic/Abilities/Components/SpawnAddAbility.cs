using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An action which spawns a pooled object
/// </summary>
public class SpawnAddAbility : AbstractAbilityAction {

    [SerializeField]
    protected AddsNetworkedData.AddID addType;

    [SerializeField]
    protected string name;

    public override bool Activate(PhotonStream stream) {
        GameObject spawnedAdd = AddsNetworkedData.Main.CreateAdd(addType, this.transform);
        spawnedAdd.name = name;
        return false;
    }
}
