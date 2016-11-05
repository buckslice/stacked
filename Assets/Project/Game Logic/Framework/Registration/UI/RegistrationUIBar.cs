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

    public bool ready { set { readyText.enabled = value; } }
}
