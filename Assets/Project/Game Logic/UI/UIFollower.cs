using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;


public enum OffsetType {
    WORLD,
    PIXEL,
    CAMERA_LOCAL,
}

/// <summary>
/// Causes the UI element to follow a world space element, while remaining in screen space
/// </summary>
public class UIFollower : MonoBehaviour {

    CanvasScaler canvasScaler;
    RectTransform rectTransform;

    Transform followTransform;
    Vector3 offset;
    OffsetType offsetType;
    Camera cam;

    /// <summary>
    /// Constructor-like method for initialization.
    /// </summary>
    /// <param name="followTransform"></param>
    /// <param name="offset"></param>
    public void Initialize(CanvasHelper canvasHelper, Transform followTransform, Vector3 offset, OffsetType offsetType) {
        this.followTransform = followTransform;
        this.offset = offset;
        this.offsetType = offsetType;
        canvasScaler = canvasHelper.scaler;
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;
    }

    public void Initialize(CanvasHelper canvasHelper, Transform followTransform) { Initialize(canvasHelper, followTransform, Vector3.zero, OffsetType.WORLD); }

    void Update() {
        if (followTransform == null) {
            //Debug.LogWarning("UIFollower not initialized. Removing component.", this.gameObject);
            //Destroy(this);
            return;
        }
        Vector3 worldOffset = Vector3.zero;
        Vector2 pixelOffset = Vector2.zero;
        switch (offsetType) {
            case OffsetType.WORLD:
                worldOffset = offset;
                break;
            case OffsetType.PIXEL:
                pixelOffset = new Vector2(offset.x, offset.y);
                break;
            case OffsetType.CAMERA_LOCAL:
                Transform t = cam.transform.parent;
                worldOffset = t.right * offset.x + t.up * offset.y + t.forward * offset.z;
                break;
            default:
                break;
        }

        Vector3 screenPoint = cam.WorldToScreenPoint(followTransform.position + worldOffset);
        // need to account for canvas scaler (disable this if we stop using scaler)
        float scale = Mathf.Lerp(canvasScaler.referenceResolution.x / Screen.width,
                                 canvasScaler.referenceResolution.y / Screen.height,
                                 canvasScaler.matchWidthOrHeight);
        Vector2 scaledScreenPoint = new Vector2(screenPoint.x * scale, screenPoint.y * scale);
        rectTransform.anchoredPosition = scaledScreenPoint + pixelOffset;
    }

    public void SetOffset(Vector3 offset, OffsetType offsetType = OffsetType.CAMERA_LOCAL) {
        this.offset = offset;
        this.offsetType = offsetType;
    }

    public Vector3 GetOffset() {
        return offset;
    }
}
