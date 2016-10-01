using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    Image mainBar;
    [SerializeField]
    Image lerpBar;
    [SerializeField]
    Image backBar;

    public Gradient grad;

    float healthPercent = 1.0f;
    float lerpPos = 1.0f;

	// Use this for initialization
	void Start () {
	
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
    }

    public void SetPercent(float percent) {
        healthPercent = percent;
    }
}
