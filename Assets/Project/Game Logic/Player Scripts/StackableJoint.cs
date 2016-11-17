using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class StackableJoint : MonoBehaviour {

    Queue<TimestampedData<Vector3>> bufferedTargetPositions = new Queue<TimestampedData<Vector3>>();

    TimestampedData<Vector3> previousTargetPosition;

    /// <summary>
    /// Delay used for wobbling animation.
    /// </summary>
    [SerializeField]
    protected float wobbleDelay = 0f;

    Rigidbody target;
    public Rigidbody Target {
        get { return target; }
        set {
            RemoveTarget(); AddTarget(value);
        }
    }

    void AddTarget(Rigidbody target) {
        if (target) {
            target.isKinematic = true;
            target.MovePosition(transform.position);

            previousTargetPosition = new TimestampedData<Vector3>(Time.time - 1, transform.position);
        }
        this.target = target;
    }

    void RemoveTarget() {
        if (target) {
            target.isKinematic = false;
        }
        bufferedTargetPositions.Clear();
        this.target = null;
    }

    void LateUpdate() {
        if (target == null) {
            return;
        }

        bufferedTargetPositions.Enqueue(new TimestampedData<Vector3>(Time.time, transform.position));

        while (bufferedTargetPositions.Count != 0 && bufferedTargetPositions.Peek().outputTime + wobbleDelay < Time.time) {
            //we have new data we can use
            previousTargetPosition = bufferedTargetPositions.Dequeue();
        }

        target.MovePosition(previousTargetPosition);

    }
}
