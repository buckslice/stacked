using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class MeshRendererSortingLayers : MonoBehaviour {

    [SerializeField]
    protected string sortingLayer;

    [SerializeField]
    protected int orderInLayer;

    void Awake()
    {
        MeshRenderer rend = GetComponent<MeshRenderer>();
        rend.sortingLayerName = sortingLayer;
        rend.sortingOrder = orderInLayer;
    }
}
