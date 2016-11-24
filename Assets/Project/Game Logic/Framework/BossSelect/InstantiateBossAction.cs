using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

public class InstantiateBossAction : AbstractAbilityAction {

    [SerializeField]
    GameObject bossDataPrefab;

    public override bool Activate(PhotonStream stream) {
        GameObject instantiatedBossDataPrefab = Instantiate(bossDataPrefab);
        instantiatedBossDataPrefab.GetComponent<AbstractBossSetup>().InstantiateBoss();
        return false;
    }
}
