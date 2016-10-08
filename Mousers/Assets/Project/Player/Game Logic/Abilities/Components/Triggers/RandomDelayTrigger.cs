using UnityEngine;
using System.Collections;

// intended to be used for bosses primarily for them to use their abilities
public class RandomDelayTrigger : MonoBehaviour, IUntargetedAbilityTrigger {

    [SerializeField]
    private float minDelay = 0.0f;
    [SerializeField]
    private float maxDelay = 3.0f;
    //[SerializeField]
    //private AnimationCurve distribution;  // prob overkill

    CooldownConstraint cc;
    float nextTriggerTime = -1.0f;

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    // Use this for initialization
    void Start() {
        cc = GetComponent<CooldownConstraint>();
    }

    // Update is called once per frame
    void Update() {
        // constrain delay variables
        if(minDelay < 0.0f) {
            minDelay = 0.0f;
        }
        if (maxDelay < 0.0f) {
            maxDelay = 0.0f;
        }
        if (minDelay > maxDelay) {
            minDelay = maxDelay;
        }

        // if there is no cooldown constraint or there is and it is off cooldown
        if (!cc || cc.isAbilityActivatible()) {
            if (nextTriggerTime < 0.0f) {
                nextTriggerTime = Time.time + Random.Range(minDelay, maxDelay);
            } else if (nextTriggerTime <= Time.time) {
                abilityTriggerEvent();
                nextTriggerTime = -1.0f;
            }
        } else {
            nextTriggerTime = -1.0f;
        }

    }
}
