using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IEntityUIGroupHolder {
    EntityUIGroup EntityGroup { get; }
}

/// <summary>
/// Creates and acts as a reference group to all of a particular entity's UI.
/// 
/// My naming choices here are definitely suboptimal, and could be improved.
/// </summary>
public class EntityUIGroupHolder : MonoBehaviour, IEntityUIGroupHolder {

    [SerializeField]
    public AnchorType anchorType = AnchorType.FLOATING;
    public OffsetType offsetType = OffsetType.WORLD;
    public Vector3 offset = Vector3.zero;

    [SerializeField]
    protected GameObject entityGroupUIPrefab;

    RectTransform groupTransform;
    public RectTransform EntityGroupTransform { get { return groupTransform; } }
    EntityUIGroup entityGroup;
    public EntityUIGroup EntityGroup { get { return entityGroup; } }

    UIFollower follower;

    void Awake () {
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        CanvasHelper canvasHelper = canvasRoot.GetComponent<CanvasHelper>();
        Assert.IsNotNull(canvasHelper);

        if (anchorType == AnchorType.CORNERS) {
            groupTransform = (Instantiate(entityGroupUIPrefab, canvasHelper.transform)).GetComponent<RectTransform>();
        } else {
            groupTransform = (Instantiate(entityGroupUIPrefab, canvasHelper.floatingHealthBarGroup)).GetComponent<RectTransform>();
            groupTransform.localScale = Vector3.one;

            follower = groupTransform.GetComponent<UIFollower>();
            if (follower != null) {
                Debug.Assert(canvasHelper.scaler, "Need canvas scaler on canvas!");
                follower.Initialize(canvasHelper, transform, offset, offsetType);
            }
        }
        groupTransform.SetAsFirstSibling(); 
        entityGroup = groupTransform.GetComponent<EntityUIGroup>();
	}

    void Start() {
        if (anchorType == AnchorType.CORNERS) {
            IPlayerID player = GetComponent<IPlayerID>();
            if (player == null) {
                player = (Player)GetComponent<IDamageHolder>().DamageTracker;
            }
            PositionPlayerEntityGroup(groupTransform, player.PlayerID);
        }
    }

    public void PositionPlayerEntityGroup(RectTransform entityGroup, int playerIndex) {
        Vector2 sd = entityGroup.sizeDelta;  // just save this but reset everything else
        entityGroup.Reset();
        entityGroup.sizeDelta = sd;

        switch (playerIndex) {
            case 0:
                entityGroup.anchorMin = entityGroup.anchorMax = entityGroup.pivot = Vector2.up;
                break;
            case 1:
                entityGroup.anchorMin = entityGroup.anchorMax = entityGroup.pivot = Vector2.one;
                break;
            case 2:
                entityGroup.anchorMin = entityGroup.anchorMax = entityGroup.pivot = Vector2.zero;
                StartCoroutine(InvertRoutine(entityGroup));
                break;
            case 3:
                entityGroup.anchorMin = entityGroup.anchorMax = entityGroup.pivot = Vector2.right;
                StartCoroutine(InvertRoutine(entityGroup));
                break;
        }

    }

    public void SetGroupActive(bool active) {
        groupTransform.gameObject.SetActive(active);
        follower.Follow();
    }

    // coroutine that is basically like a 3rd start (to make sure UI object is fully costructed by the time it tries to reorder children)
    IEnumerator InvertRoutine(Transform tform) {
        entityGroup.gameObject.SetActive(false);
        yield return null;
        int childNum = tform.childCount;
        for (int i = 0; i < childNum; ++i) {
            tform.GetChild(0).SetSiblingIndex(childNum - i - 1);
        }
        tform.gameObject.SetActive(true);
    }

    void OnDestroy() {
        if (groupTransform != null) {
            Destroy(groupTransform.gameObject);
        }
    }

    public void SetFollowerOffset(Vector3 offset, OffsetType type = OffsetType.CAMERA_LOCAL) {
        follower.SetOffset(offset, type);
    }
    public Vector3 GetFollowerOffset() {
        return follower.GetOffset();
    }

}
