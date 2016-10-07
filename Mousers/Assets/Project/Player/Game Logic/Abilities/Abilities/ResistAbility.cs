using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class ResistAbility : AbstractAbilityAction {

    [SerializeField]
    protected float duration = 1.0f;

    [SerializeField]
    protected float resistAmount = .5f;

    Damageable damageable;

    int layermask;
    Coroutine activeRoutine;

    protected override void Start() {
        base.Start();
        damageable = transform.root.GetComponentInChildren<CapsuleCollider>().GetComponent<Damageable>();
    }

    public override bool Activate(PhotonStream stream) {
        if (activeRoutine != null) {
            StopCoroutine(activeRoutine);
        }
        activeRoutine = StartCoroutine(DurationRoutine());
        return true;
    }

    protected IEnumerator DurationRoutine() {
        MultiplierFloatStat multiplier = damageable.getVulnerabilityMultiplier();
        multiplier *= resistAmount;

        yield return new WaitForSeconds(duration);

        multiplier /= resistAmount;
    }
}
