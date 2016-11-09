using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class ScaleStat : MonoBehaviour, IGenericStat {
    [SerializeField]
    Vector3 scaleFactor = Vector3.one;

    float IBalanceStat.Value {
        set {
            Vector3 targetScale = scaleFactor * value;
            if (targetScale.x == 0) { targetScale.x = 1; }
            if (targetScale.y == 0) { targetScale.y = 1; }
            if (targetScale.z == 0) { targetScale.z = 1; }
            this.transform.localScale = targetScale;
        }
    }
}
