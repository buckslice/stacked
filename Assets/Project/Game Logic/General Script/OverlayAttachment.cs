using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Attaches an overlay material to a collider's renderer
/// </summary>
public abstract class AbstractAttachableEffect : MonoBehaviour, IDespawnable {

    Collider targetCollider;

    public virtual void Initialize(Collider target) {
        this.targetCollider = target;
        this.transform.SetParent(target.transform);
        this.transform.localPosition = Vector3.zero;
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

    protected Material material;
    public Material Material { get { return material; } }

    public override void Initialize(Collider target) {
        base.Initialize(target);

        Renderer targetRenderer = target.transform.root.GetComponentInChildren<Renderer>();
        if (targetRenderer == null) {
            return;
        }

        targetOverlay = targetRenderer.GetComponent<Overlay>();
        if (targetOverlay == null) {
            targetOverlay = targetRenderer.gameObject.AddComponent<Overlay>();
        }
        material = targetOverlay.Add(overlayEffect);
        Assert.IsNotNull(material);
    }

    public override void Despawn() {
        if (targetOverlay != null) {
            targetOverlay.Remove(overlayEffect);
            targetOverlay = null;
            material = null;
        }
    }
}

/// <summary>
/// A class representing all the overlays on a single gameobject. Created via AddComponent in overlyaAttachment
/// </summary>
[RequireComponent(typeof(Renderer))]
public class Overlay : MonoBehaviour {

    protected Renderer targetRenderer;
    public Renderer TargetRenderer { get { return targetRenderer; } }

    void Awake() {
        targetRenderer = GetComponent<Renderer>();
    }

    Dictionary<Material, int> currentOverlays = new Dictionary<Material,int>();
    Dictionary<Material, int> overlayCounts = new Dictionary<Material, int>();
    HashSet<int> openIndices = new HashSet<int>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mat"></param>
    /// <returns>The instantiated material, in order to set shader properties</returns>
    public Material Add(Material mat) {
        if (overlayCounts.ContainsKey(mat)) {
            overlayCounts[mat]++;
            return targetRenderer.materials[currentOverlays[mat]];
        } else {
            overlayCounts[mat] = 1;
            int index = AddMaterial(mat);
            return targetRenderer.materials[index];
        }
    }

    /// <summary>
    /// Adds a material to the renderer.
    /// </summary>
    /// <param name="mat"></param>
    /// <returns>The index of the material.</returns>
    int AddMaterial(Material mat) {
        //Get the first open index in the materials array
        Material[] existingMaterials = targetRenderer.materials; //create a single duplicate; targetRenderer.materials will create a duplicate every time it is called

        if (openIndices.Count > 0) {
            int index = openIndices.First();
            existingMaterials[index] = mat;
            currentOverlays[mat] = index;
            targetRenderer.materials = existingMaterials;
            openIndices.Remove(index);
            return index;
        }

        //else

        //no open slots, increase the array size
        Material[] newMaterials = new Material[existingMaterials.Length + 1];
        existingMaterials.CopyTo(newMaterials, 0);
        newMaterials[existingMaterials.Length] = mat;
        currentOverlays[mat] = existingMaterials.Length;
        targetRenderer.materials = newMaterials;
        return existingMaterials.Length;
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
        openIndices.Add(currentOverlays[mat]);
        targetRenderer.materials = newMaterials;
        currentOverlays.Remove(mat);
    }
}