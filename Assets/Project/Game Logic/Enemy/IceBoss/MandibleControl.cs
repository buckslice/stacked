﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandibleControl : MonoBehaviour {

    public Transform left;
    public Transform right;

    public bool autoTwitch = true;
    public bool autoSound = true;

    public float nextChange = 1.0f;
    float soundTimer = 7.0f;

    AudioSource source;

    Vector3 leftEulers = new Vector3(0, 150, -3);
    Vector3 rightEulers = new Vector3(-180, 30, -3);

    // Use this for initialization
    void Start() {
        source = GetComponent<AudioSource>();

        //leftEulers = left.localRotation.eulerAngles;
        //rightEulers = right.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update() {

        if (autoTwitch) {
            nextChange -= Time.deltaTime;
            if (nextChange < 0.0f) {
                Twitch(Random.Range(20.0f, 60.0f), Random.value * 0.1f + 0.01f);
                nextChange = Random.value * 0.5f;
            }
        }

        if (autoSound) {
            soundTimer -= Time.deltaTime;
            if (soundTimer < 0.0f) {
                PlaySound();
                soundTimer += Random.value * 4.0f + 2.0f;
            }
        }
    }

    public void PlaySound() {
        source.pitch = 1.0f + Random.value * 0.2f - 0.1f;
        source.Play();
    }

    public void Twitch(float angle, float time) {
        if (mandibleRoutine != null) {
            StopCoroutine(mandibleRoutine);
        }
        mandibleRoutine = StartCoroutine(TwitchRoutine(angle+10.0f, time)); // added +10 for new model
    }

    Coroutine mandibleRoutine = null;
    IEnumerator TwitchRoutine(float angle, float time) {
        if (time == 0.0f) {
            yield break;
        }

        Quaternion ls = left.localRotation;
        Quaternion rs = right.localRotation;

        Quaternion leftRot = Quaternion.Euler(leftEulers.x, 180.0f - angle, leftEulers.z);
        Quaternion rightRot = Quaternion.Euler(rightEulers.x, angle, rightEulers.z);

        float t = 0.0f;
        while (t < time) {

            left.localRotation = Quaternion.Lerp(ls, leftRot, t / time);
            right.localRotation = Quaternion.Lerp(rs, rightRot, t / time);

            t += Time.deltaTime;
            yield return null;
        }

        left.localRotation = leftRot;
        right.localRotation = rightRot;
    }
}
