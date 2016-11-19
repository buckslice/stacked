using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DeathBillboarding : MonoBehaviour {

    [SerializeField]
    protected Transform visualsRoot;

    Transform cam;
    void Start() {
        cam = Camera.main.transform;
    }

    void Update() {
        Vector3 viewDir = visualsRoot.position - cam.position;
        viewDir -= Vector3.Project(viewDir, Vector3.up);
        visualsRoot.rotation = Quaternion.LookRotation(viewDir, Vector3.up);
    }
}
