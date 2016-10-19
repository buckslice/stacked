using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class EntityUIGroup : MonoBehaviour {

    [SerializeField]
    protected RectTransform healthBarHolder;
    public RectTransform HealthBarHolder { get { return healthBarHolder; } }

    [SerializeField]
    protected RectTransform statusHolder;
    public RectTransform StatusHolder { get { return statusHolder; } }
}
