using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum AnchorType {
    CORNERS,        // bars in 4 corners
    FLOATING,       // 2D UI floating over entity
    //BOSS,         // big bar at top         // not yet implemented
    //WORLDSPACE    // 3D worldspace maybe?   // not yet implemented
};

public enum HealthBarType {
    MINIMAL,
    BOSS,
};

public class HealthBar : MonoBehaviour {

    [SerializeField]
    Image mainBar;
    [SerializeField]
    Image lerpBar;
    [SerializeField]
    Image backBar;
    [SerializeField]
    Text text;

    public Gradient grad;

    HealthBar playerReference;

    float healthPercent = 1.0f;
    float lerpPos = 1.0f;

	// Update is called once per frame
	void Update () {
        mainBar.rectTransform.anchorMin = new Vector2(0.0f, 0.0f);
        mainBar.rectTransform.anchorMax = new Vector2(healthPercent, 1.0f);
        Color color = grad.Evaluate(healthPercent);
        mainBar.color = color;
        color.a = 0.66f;
        lerpBar.color = color;

        lerpPos = Mathf.Lerp(lerpPos, healthPercent, Time.deltaTime * 2.0f);
        lerpBar.rectTransform.anchorMin = new Vector2(healthPercent, 0.0f);
        lerpBar.rectTransform.anchorMax = new Vector2(lerpPos, 1.0f);
    }

    public void SetPercent(float percent) {
        healthPercent = percent;
    }

    public void SetText(string t) {
        if (text) {
            text.text = t;
        }
    }

    public void SetTextColor(Color c) {
        if (text) {
            text.color = c;
        }
    }

}
