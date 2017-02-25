using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RegistrationUIBar : MonoBehaviour {

    [SerializeField]
    protected Image[] tintedImages;

    [SerializeField]
    protected Text title;

    [SerializeField]
    protected Text readyText;

    public Color color {
        set {
            foreach (Image tintedImage in tintedImages) {
                tintedImage.color = value;
            }
        }
    }

    public int PlayerID {
        set {
            title.text = "Player" + (value + 1);
            if (value < Player.playerColoring.Length) {
                color = Player.playerColoring[value];
            }
        }
    }

    void Update() {
        if (readyText.enabled) {
            // triangle wave
            float pos = Mathf.Repeat(Time.time - 0.5f, 0.5f) / 0.5f;
            float t = pos < 0.5f ? Mathf.Lerp(0, 1, pos * 2f) : Mathf.Lerp(1, 0, (pos - .5f) * 2f);
            readyText.color = Color.Lerp(Color.white, Color.magenta, t);
        }
    }

    public bool ready { set { readyText.enabled = value; } }
}
