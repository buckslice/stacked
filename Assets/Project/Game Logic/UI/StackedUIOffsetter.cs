using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to move around offset of ui elements during stacking
public class StackedUIOffsetter : MonoBehaviour {
    public AnimationCurve animCurve = AnimationCurve.Linear(0, 0, 1, 1);
    Stackable stack;
    EntityUIGroupHolder holder;
    Vector3 startOffset;
    Vector3 stackedOffset;

    // Use this for initialization
    void Start() {
        stack = GetComponent<Stackable>();
        holder = GetComponent<EntityUIGroupHolder>();
        startOffset = holder.offset;
        stackedOffset = new Vector3(6.0f, 0.0f, 0.0f);

        // subscribe to stack events
        stack.changeEvent += OnStackChange;
    }

    void OnStackChange() {
        //holder.SetFollowerOffset(stack.Stacked ? stackedOffset : startOffset);
        LerpTo(stack.Stacked ? stackedOffset : startOffset);
    }

    Coroutine lerpRoutine = null;
    void LerpTo(Vector3 targ) {
        if (lerpRoutine != null) {
            StopCoroutine(lerpRoutine);
        }
        lerpRoutine = StartCoroutine(LerpRoutine(targ, 0.2f));
    }

    IEnumerator LerpRoutine(Vector3 targ, float lt) {
        float t = 0.0f;
        Vector3 cur = holder.GetFollowerOffset();
        while (t < lt) {
            Vector3 v = Vector3.LerpUnclamped(cur, targ, animCurve.Evaluate(t / lt));
            holder.SetFollowerOffset(v);
            t += Time.deltaTime;
            yield return null;
        }
        holder.SetFollowerOffset(targ);
        lerpRoutine = null;
    }
}
