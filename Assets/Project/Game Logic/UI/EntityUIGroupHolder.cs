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
            groupTransform.SetAsFirstSibling(); // so boss bars are drawn on top

            follower = groupTransform.GetComponent<UIFollower>();
            if (follower != null) {
                Debug.Assert(canvasHelper.scaler, "Need canvas scaler on canvas!");
                follower.Initialize(canvasHelper, transform, offset, offsetType);
            }
        }
        entityGroup = groupTransform.GetComponent<EntityUIGroup>();
	}

    void Start() {
        if (anchorType == AnchorType.CORNERS) {
            IPlayerID player = GetComponent<IPlayerID>();
            if (player == null) {
                player = (Player)GetComponent<IDamageHolder>().DamageTracker;
            }
            CanvasHelper.PositionPlayerEntityGroup(groupTransform, player.PlayerID);
        }
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
