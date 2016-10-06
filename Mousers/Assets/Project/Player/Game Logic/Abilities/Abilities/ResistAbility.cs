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

    public override void Activate() {
        networkedActivation.ActivateRemote();
    }

    public override void ActivateWithRemoteData(object data) {
    }

    public override void ActivateRemote() {
        float endTime = Time.time + duration;
        if (activeRoutine != null) {
            StopCoroutine(activeRoutine);
        }
        activeRoutine = StartCoroutine(DurationRoutine(endTime));
    }

    protected IEnumerator DurationRoutine(float endTime) {
        MultiplierFloatStat multiplier = damageable.getVulnerabilityMultiplier();
        multiplier *= resistAmount;

        while (Time.time <= endTime) {
            yield return null;
        }

        multiplier /= resistAmount;
    }
}
