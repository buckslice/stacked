using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Placed on an object to mark it as the current Music Singleton
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MusicSingleton : MonoBehaviour {

    private static MusicSingleton main = null;
    public static MusicSingleton Main { get { return main; } }

    /// <summary>
    /// If the Main MusicSingleton is null (there isn't one yet), set it to newMain.
    /// </summary>
    /// <param name="newMain"></param>
    public static bool SetMain(MusicSingleton newMain) {
        if(Main == null) {
            main = newMain;
            DontDestroyOnLoad(newMain.gameObject.transform.root.gameObject);
            return true;
        } else {
            Destroy(newMain.transform.root.gameObject);
            return false;
        }
    }

    /// <summary>
    /// Destroy the old main, if it existed, and set main to newMain.
    /// </summary>
    /// <param name="newMain"></param>
    /// <returns></returns>
    public static void ChangeMain(MusicSingleton newMain) {
        Assert.IsNotNull(newMain);
        if (Main != null) {
            Destroy(main.transform.root.gameObject);
        }
        main = newMain;
        DontDestroyOnLoad(newMain.gameObject.transform.root.gameObject);
    }

    private void OnDestroy() {
        if (main == this) {
            main = null;
        }
    }
}
