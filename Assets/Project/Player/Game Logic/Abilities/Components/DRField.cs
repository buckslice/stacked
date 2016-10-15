using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Applies a buff/debuff to targets in a shape
/// </summary>
[RequireComponent(typeof(IShape))]
[RequireComponent(typeof(Collider))]
public abstract class AbstractBuffField : MonoBehaviour {

    [SerializeField]
    protected LayerMask layermask;

    IShape shape;
    HashSet<Collider> appliedTargets = new HashSet<Collider>();

    void Awake() {
        shape = GetComponent<IShape>();
    }

    void OnEnable() {
        Assert.IsTrue(appliedTargets.Count == 0);

        foreach (Collider target in shape.Cast(layermask)) {
            OnTriggerEnter(target);
        }
    }

    void OnTriggerEnter(Collider target) {
        if (((1 << target.gameObject.layer) & layermask) == 0) {
            //not on layermask
            return;
        }

        if (appliedTargets.Add(target)) {
            ApplyBuff(target);
        }
    }

    protected abstract void ApplyBuff(Collider target);

    void OnDisable() {
        //to array to avoid mutation of the original object we are iterating over
        foreach (Collider target in appliedTargets.ToArray()) {
            OnTriggerExit(target);
        }
        Assert.IsTrue(appliedTargets.Count == 0);
    }

    void OnTriggerExit(Collider target) {
        if (appliedTargets.Remove(target)) {
            RemoveBuff(target);
        }
    }

    protected abstract void RemoveBuff(Collider target);
}

public class DRField : AbstractBuffField {
    [SerializeField]
    protected GameObject overlayAttachmentPrefab;

    [SerializeField]
    protected float magicalDamageResistanceBuff;

    [SerializeField]
    protected float physicalDamageResistanceBuff;

    Dictionary<Collider, OverlayAttachment> activeOverlays = new Dictionary<Collider, OverlayAttachment>();

    protected override void ApplyBuff(Collider target) {
        Damageable targetDamageable = target.GetComponent<Damageable>();
        if (targetDamageable != null) {
            targetDamageable.MagicalVulnerabilityMultiplier.AddModifier(magicalDamageResistanceBuff);
            targetDamageable.PhysicalVulnerabilityMultiplier.AddModifier(physicalDamageResistanceBuff);

            activeOverlays[target] = SimplePool.Spawn(overlayAttachmentPrefab).GetComponent<OverlayAttachment>();
            activeOverlays[target].Initialize(target);
        }
    }

    protected override void RemoveBuff(Collider target) {
        Damageable targetDamageable = target.GetComponent<Damageable>();
        if (targetDamageable != null) {
            targetDamageable.MagicalVulnerabilityMultiplier.RemoveModifier(magicalDamageResistanceBuff);
            targetDamageable.PhysicalVulnerabilityMultiplier.RemoveModifier(physicalDamageResistanceBuff);

            activeOverlays[target].Destroy();
            activeOverlays.Remove(target);
        }
    }
}