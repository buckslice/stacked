using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum HealthBarType {
    PLAYER,     // bars at bottom, smash bros style
    BOSS,       // big bar at top
    FLOATING,   // 2D UI floating over mob
    WORLDSPACE  // 3D worldspace maybe?
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

    float healthPercent = 1.0f;
    float lerpPos = 1.0f;

    public HealthBarType type { get; set; }
    public Transform followTransform { get; set; }
    public Vector3 followOffset { get; set; }
    public CanvasScaler scaler { get; set; }

    private RectTransform rt;

    // Use this for initialization
    void Start () {
        rt = GetComponent<RectTransform>();
    }

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

        if (type == HealthBarType.FLOATING && followTransform) {
            Vector3 sp = Camera.main.WorldToScreenPoint(followTransform.position + followOffset);
            // need to account for canvas scaler (disable this if we stop using scaler)
            float scale = Mathf.Lerp(scaler.referenceResolution.x / Screen.width, 
                                     scaler.referenceResolution.y / Screen.height, 
                                     scaler.matchWidthOrHeight);
            Vector2 screenPoint = new Vector2(sp.x * scale, sp.y * scale);
            rt.anchoredPosition = screenPoint;
        }
    }

    public void SetPercent(float percent) {
        healthPercent = percent;
    }

    public void SetText(string t) {
        text.text = t;
    }

}
