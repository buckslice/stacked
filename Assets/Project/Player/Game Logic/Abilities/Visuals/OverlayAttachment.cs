using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Attaches an overlay material to a collider's renderer
/// </summary>
public abstract class AbstractAttachableEffect : MonoBehaviour, IDespawnable {

    Collider targetCollider;

    public virtual void Initialize(Collider target) {
        this.targetCollider = target;
        this.transform.SetParent(target.transform);
    }

    public abstract void Despawn();

    protected virtual void OnDestroy() {
        if (targetCollider != null) {
            Despawn();
        }
    }

    public void Destroy() {
        SimplePool.Despawn(this.gameObject);
    }
}

public class OverlayAttachment : AbstractAttachableEffect {

    [SerializeField]
    protected Material overlayEffect;

    protected Overlay targetOverlay;
    /// <summary>
    /// Index in the targetRenderer's .materials array
    /// </summary>
    protected int index;

    public override void Initialize(Collider target) {
        base.Initialize(target);

        Renderer targetRenderer = target.GetComponentInParent<Renderer>();
        if (targetRenderer == null) {
            return;
        }

        targetOverlay = targetRenderer.GetComponent<Overlay>();
        if (targetOverlay == null) {
            targetOverlay = targetRenderer.gameObject.AddComponent<Overlay>();
        }
        targetOverlay.Add(overlayEffect);
    }

    public override void Despawn() {
        if (targetOverlay != null) {
            targetOverlay.Remove(overlayEffect);
            targetOverlay = null;
        }
    }
}

/// <summary>
/// A class representing all the overlays on a single gameobject. Created via AddComponent in overlyaAttachment
/// </summary>
[RequireComponent(typeof(Renderer))]
public class Overlay : MonoBehaviour {

    protected Renderer targetRenderer;

    void Awake() {
        targetRenderer = GetComponent<Renderer>();
    }

    Dictionary<Material, int> currentOverlays = new Dictionary<Material,int>();
    Dictionary<Material, int> overlayCounts = new Dictionary<Material, int>();

    public void Add(Material mat) {
        if (overlayCounts.ContainsKey(mat)) {
            overlayCounts[mat]++;
        } else {
            overlayCounts[mat] = 1;
            AddMaterial(mat);
        }
    }

    /// <summary>
    /// Adds a material to the renderer.
    /// </summary>
    /// <param name="mat"></param>
    void AddMaterial(Material mat) {
        //Get the first open index in the materials array
        Material[] existingMaterials = targetRenderer.materials; //create a single duplicate; targetRenderer.materials will create a duplicate every time it is called
        for (int i = 0; i < existingMaterials.Length; i++) {
            if (existingMaterials[i] == null) {
                //there is an open spot
                existingMaterials[i] = mat;
                currentOverlays[mat] = i;
                targetRenderer.materials = existingMaterials;
                return;
            }
        }

        //no open slots, increase the array size
        Material[] newMaterials = new Material[existingMaterials.Length + 1];
        existingMaterials.CopyTo(newMaterials, 0);
        newMaterials[existingMaterials.Length] = mat;
        currentOverlays[mat] = existingMaterials.Length;
        targetRenderer.materials = newMaterials;
    }

    public void Remove(Material mat) {
        Assert.IsTrue(overlayCounts.ContainsKey(mat));
        overlayCounts[mat]--;
        if (overlayCounts[mat] == 0) {
            overlayCounts.Remove(mat);
            RemoveMaterial(mat);
        }
    }

    /// <summary>
    /// Remove a material from the renderer.
    /// </summary>
    /// <param name="mat"></param>
    void RemoveMaterial(Material mat) {
        Material[] newMaterials = targetRenderer.materials;
        newMaterials[currentOverlays[mat]] = null;
        targetRenderer.materials = newMaterials;
        currentOverlays.Remove(mat);
    }
}