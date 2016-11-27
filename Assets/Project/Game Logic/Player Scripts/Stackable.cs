using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IMovement))] //not strictly required, but exists in all use cases I can think of
[RequireComponent(typeof(Rigidbody))]
public class Stackable : MonoBehaviour, IEnumerable<Stackable> {

    public const float height = 2f;
    public static int heightToStackElevation(float inputHeight) {

        if (inputHeight < 2 * Stackable.height) {
            if (inputHeight < Stackable.height) {
                return 0;
            } else {
                return 1;
            }
        } else {
            if (inputHeight >= 3 * Stackable.height) {
                return 3;
            } else {
                return 2;
            }
        }
    }

    public delegate void StackableChangedEvent();

    public event StackableChangedEvent changeEvent = delegate { };

    [SerializeField]
    StackableJoint connectingJoint;

    Rigidbody selfRigidbody;
    IMovement selfMovement;

    Stackable above = null;
    public Stackable Above { get { return above; } }

    Stackable below = null;
    public Stackable Below { get { return below; } }

    protected void Start() {
        selfMovement = GetComponent<IMovement>();
        selfRigidbody = GetComponent<Rigidbody>();

        Assert.AreApproximatelyEqual((connectingJoint.transform.position - this.transform.position).y, height);
    }

    void changeEventAll() {
        Stackable iteration = this;
        while (iteration.below != null) {
            iteration = iteration.below;
            iteration.changeEvent();
        }

        iteration = this;
        while (iteration.above != null) {
            iteration = iteration.above;
            iteration.changeEvent();
        }

        this.changeEvent();
    }

    public void Grab(Stackable target) {
        Assert.IsTrue(target.below == null);
        
        if(above != null) {
            Stackable detached = above;
            DisconnectGrabbed();
            detached.PlaceAtop(target.topmost);
        }

        connectingJoint.enabled = true;
        Rigidbody targetRigid = target.selfRigidbody;

        IMovement targetMovement = target.selfMovement;
        targetMovement.MovementInputEnabled.AddModifier(false);
        targetMovement.HaltMovement();

        connectingJoint.Target = targetRigid;
        target.below = this;
        above = target;

        changeEventAll();
    }

    void DisconnectGrabbed() {
        Assert.IsTrue(above != null);

        IMovement targetMovement = above.selfMovement;

        connectingJoint.Target = null;

        targetMovement.MovementInputEnabled.RemoveModifier(false);

        connectingJoint.enabled = false;
        above.below = null;
        above = null;
    }

    public void PlaceAtop(Stackable target) {
        Assert.IsTrue(below == null);
        target = target.topmost;
        target.Grab(this);
    }

    public bool RemoveSelf() {

        if (below == null && above == null) { return false; }

        Stackable targetBelow = below;
        Stackable targetAbove = above;

        if (targetBelow != null) {
            targetBelow.DisconnectGrabbed();
        }

        if (targetAbove != null) {
            DisconnectGrabbed();
        }

        if (targetAbove != null && targetBelow != null) {
            targetBelow.Grab(targetAbove);
        } else {
            changeEvent();
            if (targetBelow != null) {
                targetBelow.changeEventAll();
            }
            if (targetAbove != null) {
                targetAbove.changeEventAll();
            }
        }

        return true;
    }

    public bool DestackAll() {
        Stackable current = bottommost;
        Stackable next = current.above;

        if(next != null) {
            return false;
        }

        while(next != null) {
            current.DisconnectGrabbed();
            current.changeEvent();

            current = next;
            next = current.above;
        }

        return true;
    }

    public Stackable topmost {
        get {
            Stackable result = this;
            while (result.above != null) {
                result = result.above;
            }
            return result;
        }
    }

    public int elevationInStack() {
        int result = 0;
        Stackable current = this;
        while (current.below != null) {
            current = current.below;
            result++;
        }
        return result;
    }

    public Stackable bottommost {
        get {
            Stackable result = this;
            while (result.below != null) {
                result = result.below;
            }
            return result;
        }
    }

    public bool Stacked { get { return above != null || below != null; } }

    public class StackableRigidbodyEnumerator : IEnumerator<Rigidbody> {

        Stackable originalTarget;
        Stackable next;
        bool started;

        public StackableRigidbodyEnumerator(Stackable target) {
            originalTarget = target;
            Reset();
        }

        public Rigidbody Current {
            get { return next.selfRigidbody; }
        }

        void System.IDisposable.Dispose() { }

        object IEnumerator.Current {
            get { return Current; }
        }

        bool IEnumerator.MoveNext() {
            if (!started) {
                started = true;
                return true;
            }

            next = next.above;
            return next != null;
        }

        public void Reset() {
            next = originalTarget.bottommost;
            started = false;
        }
    }

    public class StackableEnumerator : IEnumerator<Stackable> {

        Stackable originalTarget;
        Stackable next;
        bool started;

        public StackableEnumerator(Stackable target) {
            originalTarget = target;
            Reset();
        }

        public Stackable Current {
            get { return next; }
        }

        void System.IDisposable.Dispose() { }

        object IEnumerator.Current {
            get { return Current; }
        }

        bool IEnumerator.MoveNext() {
            if (!started) {
                started = true;
                return true;
            }

            next = next.above;
            return next != null;
        }

        public void Reset() {
            next = originalTarget.bottommost;
            started = false;
        }
    }

    public List<Rigidbody> Rigidbodies() {
        List<Rigidbody> result = new List<Rigidbody>();
        foreach (Stackable stackable in this) {
            result.Add(stackable.selfRigidbody);
        }
        return result;
    }

    public IEnumerator<Stackable> GetEnumerator() {
        return new StackableEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}
