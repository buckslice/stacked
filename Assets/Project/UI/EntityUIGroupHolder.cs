using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Creates and acts as a reference group to all of a particular entity's UI.
/// 
/// My naming choices here are definitely suboptimal, and could be improved.
/// </summary>
public class EntityUIGroupHolder : MonoBehaviour {

    [SerializeField]
    public HealthBarType barType = HealthBarType.PLAYER;

    [SerializeField]
    protected GameObject entityGroupUIPrefab;

    RectTransform instantiatedEntityGroupTransform;
    public RectTransform EntityGroupTransform { get { return instantiatedEntityGroupTransform; } }
    EntityUIGroup entityGroup;
    public EntityUIGroup EntityGroup { get { return entityGroup; } }

	void Awake () {
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        CanvasHelper canvasHelper = canvasRoot.GetComponent<CanvasHelper>();

        instantiatedEntityGroupTransform = ((GameObject)Instantiate(entityGroupUIPrefab, canvasHelper.playerHealthBarGroup)).GetComponent<RectTransform>();
        instantiatedEntityGroupTransform.localScale = Vector3.one;
        if (barType == HealthBarType.PLAYER) {
            // need to implement boss bars still so this is temp
        } else {
            // need to implement boss bars still so this is temp

            UIFollower follower = instantiatedEntityGroupTransform.GetComponent<UIFollower>();
            if (follower != null) {
                // try to find bounds for object to use as floating health bar offset
                Bounds bounds = new Bounds();
                Collider col = GetComponent<Collider>();
                if (col) {
                    bounds = col.bounds;
                } else {
                    Renderer rend = GetComponent<Renderer>();
                    if (rend) {
                        bounds = rend.bounds;
                    }
                }

                Debug.Assert(canvasHelper.scaler, "Need canvas scaler on canvas!");
                follower.Initialize(canvasHelper, this.transform, bounds.size.y * 1.5f * Vector3.up);
            }
        }
        entityGroup = instantiatedEntityGroupTransform.GetComponent<EntityUIGroup>();
	}
}
