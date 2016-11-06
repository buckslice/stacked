using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class CameraBillboarded : MonoBehaviour {

    /// <summary>
    /// For billboarding
    /// </summary>
    void OnWillRenderObject() {
        Vector3 viewDir = -(Camera.current.transform.position - transform.position);
        transform.rotation = Quaternion.LookRotation(viewDir);
    }
}
