using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// lets other scripts easily reference useful parts of canvas layout
public class CanvasHelper : MonoBehaviour {

    public Transform floatingHealthBarGroup;

    public Object playerHealthBarPrefab;
    public Object bossHealthBarPrefab;

    public CanvasScaler scaler;

    public static void PositionPlayerEntityGroup(RectTransform entityGroup, int playerIndex) {
        entityGroup.Reset();
        entityGroup.sizeDelta = new Vector2(200, 100);

        switch (playerIndex) {
            case 0:
                entityGroup.anchorMin = entityGroup.anchorMax = entityGroup.pivot = Vector2.up;
                break;
            case 1:
                entityGroup.anchorMin = entityGroup.anchorMax = entityGroup.pivot = Vector2.one;
                break;
            case 2:
                entityGroup.anchorMin = entityGroup.anchorMax = entityGroup.pivot = Vector2.zero;
                break;
            case 3:
                entityGroup.anchorMin = entityGroup.anchorMax = entityGroup.pivot = Vector2.right;
                break;
        }
    }
}
