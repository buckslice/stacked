using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedChecker : MonoBehaviour {

    public bool isGrounded = false;
    public LayerMask groundLayer;

    void Update() {
        // assuming transform.position is at bottom of object
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f, groundLayer);
    }
}
