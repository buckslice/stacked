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
            Debug.Log(target);
            ApplyBuff(target);
        }
    }

    protected abstract void ApplyBuff(Collider target);

    void OnDisable() {
        foreach (Collider target in shape.Cast(layermask)) {
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
    protected Material effectMat;

    [SerializeField]
    protected float magicalDamageResistanceBuff;

    [SerializeField]
    protected float physicalDamageResistanceBuff;

    protected override void ApplyBuff(Collider target) {
        Damageable targetDamageable = target.GetComponent<Damageable>();
        if (targetDamageable != null) {
            targetDamageable.MagicalVulnerabilityMultiplier.AddModifier(magicalDamageResistanceBuff);
            targetDamageable.PhysicalVulnerabilityMultiplier.AddModifier(physicalDamageResistanceBuff);

            Renderer rend = targetDamageable.GetComponentInParent<Renderer>();
            if (rend != null && !rend.materials.Contains(effectMat)) {
                //add effect mat
                Material[] newMats = new Material[rend.materials.Length + 1];
                rend.materials.CopyTo(newMats, 0);
                newMats[rend.materials.Length] = effectMat;
                rend.materials = newMats;
            }
        }
    }

    protected override void RemoveBuff(Collider target) {
        Damageable targetDamageable = target.GetComponent<Damageable>();
        if (targetDamageable != null) {
            targetDamageable.MagicalVulnerabilityMultiplier.RemoveModifier(magicalDamageResistanceBuff);
            targetDamageable.PhysicalVulnerabilityMultiplier.RemoveModifier(physicalDamageResistanceBuff);

            Renderer rend = targetDamageable.GetComponentInParent<Renderer>();
            if (rend != null && rend.materials.Contains(effectMat)) {
                Material[] newMats = new Material[rend.materials.Length - 1];
                int newMatIndex = 0;

                for (int rendIndex = 0; rendIndex < rend.materials.Length; rendIndex++) {
                    //remove occurance of effectMat
                    if (rend.materials[rendIndex] != effectMat) {
                        newMats[newMatIndex++] = rend.materials[rendIndex];
                    }
                    rend.materials = newMats;
                }
            }
        }
    }
}