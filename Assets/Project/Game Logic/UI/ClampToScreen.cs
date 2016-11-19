using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// used to make sure a UI element never goes offscreen
public class ClampToScreen : MonoBehaviour {
    RectTransform rt;
    CanvasScaler scaler;
    bool hasImageComponent = false;

    // Use this for initialization
    void Start() {
        rt = (RectTransform)transform;
        scaler = transform.root.GetComponent<CanvasScaler>();

        if(GetComponent<Image>()) {
            hasImageComponent = true;
        }
    }

    void LateUpdate() {
        Vector2 center = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        Vector2 pos = rt.position;

        Vector2 sd = rt.sizeDelta;
        // scale size delta by canvas scale (happens automatically for images it seems)
        if (!hasImageComponent) {
            sd /= Mathf.Lerp(
                scaler.referenceResolution.x / Screen.width,
                scaler.referenceResolution.y / Screen.height,
                scaler.matchWidthOrHeight);
        }

        // undo the pivot for calculations
        Vector2 offset = new Vector2((0.5f - rt.pivot.x) * sd.x, (0.5f - rt.pivot.y) * sd.y);
        pos += offset;

        float widthOff = Mathf.Abs(pos.x - center.x);
        float heightOff = Mathf.Abs(pos.y - center.y);

        // if offscreen
        if (widthOff > center.x - sd.x / 2.0f || heightOff > center.y - sd.y / 2.0f) {
            // dir back to center
            Vector2 dir = new Vector2(center.x - pos.x, center.y - pos.y).normalized;

            // find aspect ratio based off the offset
            float aspect = (Screen.width - sd.x) / (Screen.height - sd.y);

            if (widthOff / heightOff > aspect) {
                dir *= (widthOff - center.x + sd.x / 2.0f) / Mathf.Abs(dir.x);
            } else {
                dir *= (heightOff - center.y + sd.y / 2.0f) / Mathf.Abs(dir.y);
            }
            // add pivot back
            transform.position = pos - offset + dir;
        }
    }

}
