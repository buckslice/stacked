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

    public override bool Activate(PhotonStream stream) {
        if (activeRoutine != null) {
            StopCoroutine(activeRoutine);
        }
        activeRoutine = StartCoroutine(DurationRoutine());
        return true;
    }

    protected IEnumerator DurationRoutine() {
        movement.controlEnabled += false;
        movement.haltMovement();
        MultiplierFloatStat multiplier = damageable.getVulnerabilityMultiplier();
        multiplier*=0;

        yield return new WaitForSeconds(duration);

        movement.controlEnabled -= false;
        multiplier/=0;
    }
}
