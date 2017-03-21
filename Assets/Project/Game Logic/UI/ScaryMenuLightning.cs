using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryMenuLightning : MonoBehaviour {

    public GameObject[] models;

    Light dirLight;

    // Use this for initialization
    void Start() {
        dirLight = GetComponent<Light>();
    }

    float nextLightningTime = 5.0f;

    // Update is called once per frame
    void Update() {
        nextLightningTime -= Time.deltaTime;
        if (nextLightningTime < 0.0f) {
            StartCoroutine(LightningRoutine(Random.Range(1.0f, 1.5f)));
            nextLightningTime = Random.Range(4.0f, 6.0f);
        }
    }

    List<int> valids = new List<int>();
    int curIndex = 0;
    IEnumerator LightningRoutine(float time) {
        valids.Clear();
        for (int i = 0; i < 3; ++i) {    // no repeats
            if (i != curIndex) {
                valids.Add(i);
            }
        }
        Vector3 e = transform.localEulerAngles;
        transform.localRotation = Quaternion.Euler(e.x, Random.Range(0, 360.0f), e.z);
        curIndex = valids[Random.Range(0, 2)];

        models[curIndex].SetActive(true);
        dirLight.enabled = true;
        float t = 0.0f;
        while (t < time) {
            t += Time.deltaTime;
            dirLight.intensity = Mathf.PerlinNoise(Time.time * 10.0f, 100.0f) * 1.0f + 2.0f;
            yield return null;
        }
        dirLight.intensity = 3.0f;
        models[curIndex].SetActive(false);
        dirLight.enabled = false;
    }
}
