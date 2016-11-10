using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
[RequireComponent(typeof(BuffTracker))]
public class DRField : MonoBehaviour, IDisabledAwake {
    
    [SerializeField]
    protected GameObject overlayAttachmentPrefab;

    [SerializeField]
    protected float magicalDamageResistanceBuff;

    [SerializeField]
    protected float physicalDamageResistanceBuff;

    BuffTracker tracker;
    
    Dictionary<Collider, OverlayAttachment> activeOverlays = new Dictionary<Collider, OverlayAttachment>();

    void IDisabledAwake.DisabledAwake() {
        tracker = GetComponent<BuffTracker>();
        tracker.onTargetAdded += tracker_onTargetAdded;
        tracker.onTargetRemoved += tracker_onTargetRemoved;
    }

    void tracker_onTargetAdded(Collider added) {
        ApplyBuff(added);
    }

    void tracker_onTargetRemoved(Collider removed) {
        RemoveBuff(removed);
    }

    protected void ApplyBuff(Collider target) {
        Damageable targetDamageable = target.GetComponent<Damageable>();
        if (targetDamageable != null) {
            targetDamageable.MagicalVulnerabilityMultiplier.AddModifier(magicalDamageResistanceBuff);
            targetDamageable.PhysicalVulnerabilityMultiplier.AddModifier(physicalDamageResistanceBuff);

            activeOverlays[target] = SimplePool.Spawn(overlayAttachmentPrefab).GetComponent<OverlayAttachment>();
            activeOverlays[target].Initialize(target);
        }
    }

    protected void RemoveBuff(Collider target) {

        if (target == null) { return; }

        Damageable targetDamageable = target.GetComponent<Damageable>();
        if (targetDamageable != null) {
            targetDamageable.MagicalVulnerabilityMultiplier.RemoveModifier(magicalDamageResistanceBuff);
            targetDamageable.PhysicalVulnerabilityMultiplier.RemoveModifier(physicalDamageResistanceBuff);

            if (activeOverlays[target] != null) {
                activeOverlays[target].Destroy();
            }
            activeOverlays.Remove(target);
        }
    }
}