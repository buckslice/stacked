using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour {

    public ParticleSystem ps;

    float rateScale;

    ParticleSystem.EmissionModule psem;

    // Use this for initialization
    void Start () {
        psem = ps.emission;
        ParticleSystem.ShapeModule pssm = ps.shape;
        float r = pssm.radius;
        // save initial relation between emmision rate and area of emitter
        rateScale = psem.rateOverTime.constant / (Mathf.PI * r * r);
	}

	void Update () {
        Vector3 s = transform.localScale;
        s += Vector3.one * Time.deltaTime;
        if (s.x <= 20.0f) {
            transform.localScale = s;
        }

        psem.rateOverTime = Mathf.PI * s.x * s.x * rateScale;
	}
}
