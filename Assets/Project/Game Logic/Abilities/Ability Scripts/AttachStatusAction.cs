using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class AttachStatusAction : TargetedAbilityAction {

    [SerializeField]
    protected GameObject statusPrefab;

    protected override void Awake() {
        base.Awake();

        //ensure that statusPrefab is a valid status.
        Assert.IsTrue(statusPrefab.GetComponentsInChildren<AbstractStatus>().Length == 1);
    }

    public override bool Activate(GameObject context, PhotonStream stream) {

        //TODO: maybe make the stream an argument to AbstractStatus's Attach() method?
        GameObject spawnedStatus = SimplePool.Spawn(statusPrefab);
        return spawnedStatus.GetComponent<AbstractStatus>().Attach(context.transform);
    }
}
