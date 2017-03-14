using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugLight : MonoBehaviour {

    public Light pointLight;
    public bool flickerLight = true;

    float twitchTime = 0.0f;
    // basically screen shake script
    float shake = 0.0f;
    float decreaseFactor = 1.0f;
    float shakeFactor;

    float startIntensity;
    void Start() {
        startIntensity = pointLight.intensity;
    }

    // Update is called once per frame
    void Update() {
        twitchTime -= Time.deltaTime;
        if (twitchTime < 0.0f) {
            twitchTime = Random.value * 2.0f + 2.0f;
            shakeFactor = Random.value * 0.2f + 0.1f;
            shake = 0.7f;
        }

        if (shake > 0.0f) {
            transform.localPosition = Random.insideUnitSphere * shakeFactor * shake;
            shake -= Time.deltaTime * decreaseFactor;
            if (flickerLight) {
                pointLight.intensity = startIntensity - Mathf.PerlinNoise(Time.time * 10.0f, 100.0f) * 2.0f;
            }
        } else {
            shake = 0.0f;
            transform.localPosition = Vector3.zero;
            if (flickerLight) {
                pointLight.intensity = startIntensity;
            }
        }
    }
}
