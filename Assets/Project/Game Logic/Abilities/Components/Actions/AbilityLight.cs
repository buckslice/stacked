using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class AbilityLight : AbstractAbilityAction {

    [SerializeField]
    protected Light targetLight;

    [SerializeField]
    protected AnimationCurve intensityOverTime;

    [SerializeField]
    protected float intensity;

    [SerializeField]
    protected float duration;

    public override bool Activate(PhotonStream stream) {
        Callback.DoLerp((float l) => {
            targetLight.intensity = intensity * intensityOverTime.Evaluate(l);
        }, duration, this);
        return true;
    }
}
