using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class TopBarEntityUIGroupHolder : MonoBehaviour, IEntityUIGroupHolder {

    [SerializeField]
    public float leftAnchor;

    [SerializeField]
    public float rightAnchor;

    [SerializeField]
    protected GameObject entityGroupUIPrefab;

    RectTransform groupTransform;
    public RectTransform EntityGroupTransform { get { return groupTransform; } }
    EntityUIGroup entityGroup;
    public EntityUIGroup EntityGroup { get { return entityGroup; } }

    void Awake() {
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        CanvasHelper canvasHelper = canvasRoot.GetComponent<CanvasHelper>();
        Assert.IsNotNull(canvasHelper);

        groupTransform = ((GameObject)Instantiate(entityGroupUIPrefab, canvasHelper.transform)).GetComponent<RectTransform>();
        
        entityGroup = groupTransform.GetComponent<EntityUIGroup>();
    }

    void Start() {

        groupTransform.Reset();
        groupTransform.sizeDelta = new Vector2(0, 50);

        groupTransform.anchorMin = new Vector2(leftAnchor, 1);
        groupTransform.anchorMax = new Vector2(rightAnchor, 1);
        groupTransform.pivot = new Vector2((leftAnchor + rightAnchor) / 2, 1);
    }

    void OnDestroy() {
        if (groupTransform != null) {
            Destroy(groupTransform.gameObject);
        }
    }
}
