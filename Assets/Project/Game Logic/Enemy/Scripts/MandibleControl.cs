using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandibleControl : MonoBehaviour {

    public Transform left;
    public Transform right;

    public bool autoTwitch = true;
    public bool autoSound = true;

    float mandibleChange = 1.0f;
    float soundTimer = 8.0f;

    AudioSource source;

    // Use this for initialization
    void Start() {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

        if (autoTwitch) {
            mandibleChange -= Time.deltaTime;
            if (mandibleChange < 0.0f) {
                Twitch(Random.Range(20.0f, 40.0f), Random.value * 0.1f + 0.01f);
                mandibleChange = Random.value * 0.5f;
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
        mandibleRoutine = StartCoroutine(TwitchRoutine(angle, time));
    }

    Coroutine mandibleRoutine = null;
    IEnumerator TwitchRoutine(float angle, float time) {
        if (time == 0.0f) {
            yield break;
        }

        Quaternion ls = left.localRotation;
        Quaternion rs = right.localRotation;

        float t = 0.0f;
        while (t < time) {
            left.localRotation = Quaternion.Lerp(ls, Quaternion.Euler(new Vector3(0, -angle, 0)), t / time);
            right.localRotation = Quaternion.Lerp(rs, Quaternion.Euler(new Vector3(0, angle, 0)), t / time);

            t += Time.deltaTime;
            yield return null;
        }
        left.localRotation = Quaternion.Euler(new Vector3(0, -angle, 0));
        right.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
}
