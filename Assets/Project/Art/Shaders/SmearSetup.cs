using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// See https://github.com/cjacobwade/HelpfulScripts/tree/master/SmearEffect
/// </summary>
public class SmearSetup : AbstractAttachableEffect {

    [SerializeField]
    protected Material material;

    [SerializeField]
    protected int frameLag = 2;

    protected Overlay targetOverlay;

    Material instantiatedMat;

    /// <summary>
    /// Index in the targetRenderer's .materials array
    /// </summary>
    protected int index;

    /// <summary>
    /// Count should always be frameLag.
    /// </summary>
    Queue<Vector3> positions;

    void Awake() {
        positions = new Queue<Vector3>(frameLag);
        for (int i = 0; i <= frameLag; i++) {
            positions.Enqueue(transform.position);
        }
    }

    public override void Initialize(Collider target) {
        base.Initialize(target);

        positions.Clear();
        for (int i = 0; i <= frameLag; i++) {
            positions.Enqueue(transform.position);
        }

        Renderer targetRenderer = target.GetComponentInChildren<Renderer>();
        if (targetRenderer == null) {
            return;
        }

        targetOverlay = targetRenderer.GetComponent<Overlay>();
        if (targetOverlay == null) {
            targetOverlay = targetRenderer.gameObject.AddComponent<Overlay>();
        }
        instantiatedMat = targetOverlay.Add(material);
        instantiatedMat.SetColor("_Color", Color.red);//targetOverlay.TargetRenderer.material.color);
        instantiatedMat.SetVector("_Position", transform.position);
        instantiatedMat.SetVector("_PrevPosition", transform.position);
    }

    public override void Despawn() {
        if (targetOverlay != null) {
            targetOverlay.Remove(material);
            targetOverlay = null;
        }
    }

    void LateUpdate() {
        instantiatedMat.SetVector("_PrevPosition", positions.Dequeue());

        instantiatedMat.SetVector("_Position", transform.position);
        positions.Enqueue(transform.position);
    }
}