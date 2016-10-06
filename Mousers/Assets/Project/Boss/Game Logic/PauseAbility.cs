using UnityEngine;
using System.Collections;

public class PauseAbility : AbstractAbilityAction {

    [SerializeField]
    float pauseTime = 3.0f;

    BossAggro boss;
    Coroutine activeRoutine;

    protected override void Start() {
        base.Start();
        boss = GetComponentInParent<BossAggro>();
    }

    public override void Activate() {
        networkedActivation.ActivateRemote();
    }

    public override void ActivateWithRemoteData(object data) {
    }

    public override void ActivateRemote() {
        float endTime = Time.time + pauseTime;
        if (activeRoutine != null) {
            StopCoroutine(activeRoutine);
        }
        activeRoutine = StartCoroutine(PauseRoutine(endTime));
    }

    protected IEnumerator PauseRoutine(float endTime) {
        boss.shouldChase = false;
        while (Time.time <= endTime) {
            // TODO still should look at player with top aggro
            yield return null;
        }
        boss.shouldChase = true;
    }
}
