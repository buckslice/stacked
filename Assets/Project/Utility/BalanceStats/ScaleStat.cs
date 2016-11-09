using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class ScaleStat : MonoBehaviour, IBalanceStat {
    [SerializeField]
    Vector3 scaleFactor = Vector3.one;

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch (type) {
            case BalanceStat.StatType.ANY:
            default:
                Vector3 targetScale = scaleFactor * value;
                if (targetScale.x == 0) { targetScale.x = 1; }
                if (targetScale.y == 0) { targetScale.y = 1; }
                if (targetScale.z == 0) { targetScale.z = 1; }
                this.transform.localScale = targetScale;
                break;
        }
    }
}
