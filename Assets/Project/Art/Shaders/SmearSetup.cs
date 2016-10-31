using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// See https://github.com/cjacobwade/HelpfulScripts/tree/master/SmearEffect
/// </summary>
public class SmearSetup : AbstractAttachableEffect, IProjectileDeactivation {

    [SerializeField]
    protected Material material;

    [SerializeField]
    protected float timeLag = 2;

    [SerializeField]
    protected float endDuration = 0.5f;

    protected Overlay targetOverlay;

    Material instantiatedMat;

    /// <summary>
    /// Index in the targetRenderer's .materials array
    /// </summary>
    protected int index;

    /// <summary>
    /// Count should always be frameLag.
    /// </summary>
    Queue<TimestampedData<Vector3>> positions;

    void Awake() {
        positions = new Queue<TimestampedData<Vector3>>();

        positions.Enqueue(new TimestampedData<Vector3>(Time.time, transform.position));
        positions.Enqueue(new TimestampedData<Vector3>(Time.time + timeLag, transform.position));
    }

    public override void Initialize(Collider target) {
        base.Initialize(target);

        positions.Clear();

        positions.Enqueue(new TimestampedData<Vector3>(Time.time, transform.position));
        positions.Enqueue(new TimestampedData<Vector3>(Time.time + timeLag, transform.position));

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

    float IProjectileDeactivation.getDeactivationTime() {
        return endDuration;
    }

    void Update() {
        positions.Enqueue(new TimestampedData<Vector3>(Time.time + timeLag, transform.position));

        while (positions.Peek().outputTime < Time.time) {
            positions.Dequeue();
        }

        instantiatedMat.SetVector("_PrevPosition", positions.Peek().data);

        instantiatedMat.SetVector("_Position", transform.position);
    }
}