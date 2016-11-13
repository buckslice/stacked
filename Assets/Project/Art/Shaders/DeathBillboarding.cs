using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DeathBillboarding : MonoBehaviour {

    [SerializeField]
    protected Transform visualsRoot;

    void Update() {
        Vector3 viewDir = visualsRoot.position - Camera.main.transform.position;
        viewDir -= Vector3.Project(viewDir, Vector3.up);
        visualsRoot.rotation = Quaternion.LookRotation(viewDir, Vector3.up);
    }
}
