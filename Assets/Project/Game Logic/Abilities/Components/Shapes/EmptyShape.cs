using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class EmptyShape : MonoBehaviour, IShape {
    Collider[] IShape.Cast(LayerMask layermask) {
        return new Collider[0];
    }
}
