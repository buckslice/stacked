using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Causes the UI element to follow a world space element, while remaining in screen space
/// </summary>
public class UIFollower : MonoBehaviour {

    CanvasScaler canvasScaler;
    RectTransform rectTransform;

    Transform followTransform;
    Vector3 followOffset;

    /// <summary>
    /// Constructor-like method for initialization.
    /// </summary>
    /// <param name="followTransform"></param>
    /// <param name="followOffset"></param>
    public void Initialize(CanvasHelper canvasHelper, Transform followTransform, Vector3 followOffset) {
        this.followTransform = followTransform;
        this.followOffset = followOffset;
        canvasScaler = canvasHelper.scaler;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(CanvasHelper canvasHelper, Transform followTransform) { Initialize(canvasHelper, followTransform, Vector3.zero); }
	
	void Update () {
        if(followTransform == null)
        {
            Debug.LogWarning("UIFollower not initialized. Removing component.", this.gameObject);
            Destroy(this);
            return;
        }
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(followTransform.position + followOffset);
        // need to account for canvas scaler (disable this if we stop using scaler)
        float scale = Mathf.Lerp(canvasScaler.referenceResolution.x / Screen.width,
                                 canvasScaler.referenceResolution.y / Screen.height,
                                 canvasScaler.matchWidthOrHeight);
        Vector2 scaledScreenPoint = new Vector2(screenPoint.x * scale, screenPoint.y * scale);
        rectTransform.anchoredPosition = scaledScreenPoint;
	}
}
