using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class BlockAbility : AbstractAbilityAction {

    [SerializeField]
    protected float duration = 1.0f;

    PlayerMovement movement;
    Damageable damageable;

    Coroutine activeRoutine;

    protected override void Start() {
        base.Start();
        damageable = transform.root.GetComponentInChildren<CapsuleCollider>().GetComponent<Damageable>();
        movement = GetComponentInParent<PlayerMovement>();
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
        movement.controlEnabled += false;
        movement.haltMovement();
        MultiplierFloatStat multiplier = damageable.getVulnerabilityMultiplier();
        multiplier*=0;

        while (Time.time <= endTime) {
            yield return null;
        }

        movement.controlEnabled -= false;
        multiplier/=0;
    }
}
