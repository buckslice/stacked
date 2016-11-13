using UnityEngine;
using System.Collections;

public class ForwardSpinModelRotation : MonoBehaviour {

    public float degreesPerSecond = 200.0f;

    Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponentInParent<Rigidbody>();
        Debug.Assert(rigid);
	}

    // Update is called once per frame
    void Update() {
        transform.Rotate(rigid.transform.right, degreesPerSecond * Time.deltaTime, Space.World);
	}
}
