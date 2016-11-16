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
    protected int spawnPointNumber = 0;

    [SerializeField]
    protected string addName;

    public override bool Activate(PhotonStream stream) {

        //get spawn point
        Transform spawnPoint;
        if(AddSpawnPoints.Main != null && spawnPointNumber >= 0 && spawnPointNumber < AddSpawnPoints.Main.SpawnPoints.Length) {
            spawnPoint = AddSpawnPoints.Main.SpawnPoints[spawnPointNumber];
        } else {
            spawnPoint = this.transform;
        }

        GameObject spawnedAdd = AddsNetworkedData.Main.CreateAdd(addType, spawnPoint);
        spawnedAdd.name = addName;
        return false;
    }
}
