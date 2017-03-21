using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryMenuLightning : MonoBehaviour {

    public GameObject[] models;

    Light dirLight;

	// Use this for initialization
	void Start () {
        dirLight = GetComponent<Light>();
	}

    float nextLightningTime = 5.0f;

	// Update is called once per frame
	void Update () {
        nextLightningTime -= Time.deltaTime;
        if(nextLightningTime < 0.0f) {
            StartCoroutine(LightningRoutine(Random.Range(0.5f, 1.0f)));
            nextLightningTime = Random.Range(2.0f, 5.0f);
        }
	}

    IEnumerator LightningRoutine(float time) {
        int index = Random.Range(0, 3);
        models[index].SetActive(true);
        dirLight.enabled = true;
        float t = 0.0f;
        while(t < time) {
            t += Time.deltaTime;
            dirLight.intensity = Mathf.PerlinNoise(Time.time * 10.0f,100.0f)*7.0f + 1.0f;
            yield return null;
        }
        dirLight.intensity = 8.0f;
        models[index].SetActive(false);
        dirLight.enabled = false;
    }
}
