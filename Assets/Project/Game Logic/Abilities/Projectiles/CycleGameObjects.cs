using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleGameObjects : MonoBehaviour {
    public float degreesPerSecond = 60.0f;
    public float time = 2.0f;
    float t = 0.0f;
    public GameObject[] objects;
    int index = 0;

    void Start() {
        t += Random.value * time;   // random start offset
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(0.0f, degreesPerSecond * Time.deltaTime, 0.0f);

        t += Time.deltaTime;
        if (t > time) {
            index += Random.Range(1, 4);
            if (index >= objects.Length) {
                index -= objects.Length;
            }
            for (int i = 0; i < objects.Length; ++i) {
                objects[i].SetActive(i == index);
            }
            t = 0.0f;
        }
    }
}
