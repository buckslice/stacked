using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Placed on an object to mark it as the current Music Singleton
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MusicSingleton : MonoBehaviour {
    [SerializeField]
    public bool overrideSingleton = false;

    private static MusicSingleton main = null;
    public static MusicSingleton Main { get { return main; } }

    // Use this for initialization
    void Awake() {
        Assert.IsTrue(this != Main);
        if (overrideSingleton) {
            Assert.IsNotNull(this);
            if (Main != null) {
                Destroy(main.transform.root.gameObject);
            }
            main = this;
            DontDestroyOnLoad(transform.root.gameObject);
        } else {
            if (Main == null) {
                main = this;
                DontDestroyOnLoad(transform.root.gameObject);
            } else {
                Destroy(transform.root.gameObject);
            }
        }
    }

    bool fading = false;
    public void FadeAndDestroy(float time) {
        if (!fading) {
            fading = true;
            StartCoroutine(FadeAndDestroyRoutine(time));
        }
    }

    IEnumerator FadeAndDestroyRoutine(float time) {
        overrideSingleton = false;
        if (time < 0.01f) {
            time = 0.01f;
        }
        GameObject go = main.transform.root.gameObject;
        AudioSource source = go.GetComponent<AudioSource>();
        if (!source) {
            Debug.LogWarning("Something wrong with music singletons probably");
            yield break;
        }

        float t = 1.0f;
        float startVolume = source.volume;
        while (t > 0.0f) {
            source.volume = Mathf.Lerp(0.0f, startVolume, t);
            t -= Time.unscaledDeltaTime * 1.0f / time;
            yield return null;
        }
        source.volume = 0.0f;
        Destroy(go);

    }

    private void OnDestroy() {
        if (main == this) {
            main = null;
        }
    }
}
