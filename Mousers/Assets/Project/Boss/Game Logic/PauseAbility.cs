using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class PauseAbility : AbstractAbilityAction {

    [SerializeField]
    float pauseTime = 3.0f;

    BossAggro boss;
    Coroutine activeRoutine;

    protected override void Start() {
        base.Start();
        boss = GetComponentInParent<BossAggro>();
        Assert.IsNotNull(boss);
    }

    public override bool Activate(PhotonStream stream) {
        if (activeRoutine != null) {
            StopCoroutine(activeRoutine);
        }
        activeRoutine = StartCoroutine(PauseRoutine());

        return true;
    }

    protected IEnumerator PauseRoutine() {
        boss.shouldChase = false;
        yield return new WaitForSeconds(pauseTime);
        boss.shouldChase = true;
    }
}
