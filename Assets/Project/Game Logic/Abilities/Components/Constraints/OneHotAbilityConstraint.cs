using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

public class OneHotAbilityConstraint : UntargetedAbilityConstraint {

    OneHot oneHotReference;
    public OneHot OneHotReference { get { return oneHotReference; } }

    protected override void Start() {
        base.Start();
        oneHotReference = GetComponentInParent<OneHot>();

        if (oneHotReference == null) {
            oneHotReference = transform.root.AddComponent<OneHot>();
        }
    }

    public override bool isAbilityActivatible() {
        return oneHotReference.activationAvailable;
    }

    public override void Activate() { }
}
