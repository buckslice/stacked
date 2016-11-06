using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class AnimateShader : MonoBehaviour {

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

    Material mat;
    Coroutine activeRoutine;

	void Awake () {
        mat = target.material = target.material; //force it to be an instance, not the original
	}

    public void Play() {
        if (activeRoutine != null) {
            StopCoroutine(activeRoutine);
        }

        activeRoutine = Callback.DoLerp((float l) => {
            for (int i = 0; i < animations.Length; i++) {
                mat.SetFloat(animations[i].propertyName, animations[i].curve.Evaluate(l));
            }
        }, duration, this).FollowedBy(() => activeRoutine = null, this);
    }
}
