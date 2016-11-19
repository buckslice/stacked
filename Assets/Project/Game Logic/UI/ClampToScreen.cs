using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// used to make sure a UI element never goes offscreen
public class ClampToScreen : MonoBehaviour {
    RectTransform rt;
    //CanvasScaler scaler;

    // Use this for initialization
    void Start() {
        rt = (RectTransform)transform;
        //scaler = transform.root.GetComponent<CanvasScaler>();
    }

    void LateUpdate() {
        Vector2 center = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        Vector2 p = rt.position;

        // scale UI pixel size by canvas scale
        // I think this auto happens for sprites? commenting out for now
        //float scale = Mathf.Lerp(
        //    scaler.referenceResolution.x / Screen.width,
        //    scaler.referenceResolution.y / Screen.height,
        //    scaler.matchWidthOrHeight);

        Vector2 sd = rt.sizeDelta / 2.0f;

        float widthOff = Mathf.Abs(p.x - center.x);
        float heightOff = Mathf.Abs(p.y - center.y);

        // if offscreen
        if (widthOff > center.x - sd.x || heightOff > center.y - sd.y) {
            // dir back to center
            Vector2 dir = new Vector2(center.x - p.x, center.y - p.y).normalized;

            // find aspect ratio based off the offset
            float aspect = (Screen.width - sd.x * 2.0f) / (Screen.height - sd.y * 2.0f);

            if (widthOff / heightOff > aspect) {
                dir *= (widthOff - center.x + sd.x) / Mathf.Abs(dir.x);
            } else {
                dir *= (heightOff - center.y + sd.y) / Mathf.Abs(dir.y);
            }

            transform.position = p + dir;
        }
    }

}
