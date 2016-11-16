using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Holds an instantiated version of a renderer's material, to allow values to be set by shaders without modifying things on disk.
/// </summary>
[RequireComponent(typeof(Renderer))]
public class InstantiatedMaterialHolder : MonoBehaviour {

    Material mat;
    public Material Mat { get { return mat; } }

    void Awake() {
        Renderer renderer = GetComponent<Renderer>();
        mat = renderer.material = renderer.material; //force it to be an instance, not the original
    }
}
