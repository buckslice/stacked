using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class AnimateShader : MonoBehaviour, IBalanceStat {

    [System.Serializable]
    public class ShaderAnimation {
        public string propertyName;
        public AnimationCurve curve;
    }

    [SerializeField]
    protected float duration;

    [SerializeField]
    protected Renderer target;

    [SerializeField]
    protected ShaderAnimation[] animations;

    InstantiatedMaterialHolder matHolder;
    Coroutine activeRoutine;

	void Awake () {
        matHolder = target.GetComponent<InstantiatedMaterialHolder>();
        if (matHolder == null) {
            matHolder = target.gameObject.AddComponent<InstantiatedMaterialHolder>();
        }
	}

    public void Play() {
        if (activeRoutine != null) {
            StopCoroutine(activeRoutine);
        }

        activeRoutine = Callback.DoLerp((float l) => {
            for (int i = 0; i < animations.Length; i++) {
                matHolder.Mat.SetFloat(animations[i].propertyName, animations[i].curve.Evaluate(l));
            }
        }, duration, this).FollowedBy(() => activeRoutine = null, this);
    }

    void IBalanceStat.setValue(float value, BalanceStat.StatType type) {
        switch (type) {
            case BalanceStat.StatType.DURATION:
            default:
                duration = value;
                break;
        }
    }
}
