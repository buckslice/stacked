using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class AxisBillboarding : MonoBehaviour {

    [SerializeField]
    protected Vector3 localAxis = Vector3.forward;

    /// <summary>
    /// For billboarding
    /// </summary>
    void OnWillRenderObject() {
        Vector3 viewDir = transform.position - Camera.current.transform.position;
        Vector3 direction = transform.TransformDirection(localAxis);
        viewDir -= Vector3.Project(viewDir, direction);
        transform.rotation = Quaternion.LookRotation(viewDir, direction);
    }
}
