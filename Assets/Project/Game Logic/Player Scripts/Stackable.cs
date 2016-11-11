using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IMovement))] //not strictly required, but exists in all use cases I can think of
[RequireComponent(typeof(Rigidbody))]
public class Stackable : MonoBehaviour, IEnumerable<Stackable> {

    public delegate void StackableChangedEvent();

    public event StackableChangedEvent changeEvent = delegate { };

    [SerializeField]
    protected Rigidbody targetHolder;

    [SerializeField]
    Joint targetConnectingJoint;

    [SerializeField]
    Joint holderConnectingJoint;

    Vector3 connectedOffset;
    Rigidbody selfRigidbody;
    IMovement selfMovement;

    Stackable above = null;
    public Stackable Above { get { return above; } }

    Stackable below = null;
    public Stackable Below { get { return below; } }

    protected void Start() {
        holderConnectingJoint.connectedBody = selfRigidbody = GetComponent<Rigidbody>();
        selfMovement = GetComponent<IMovement>();
        Assert.IsTrue(holderConnectingJoint.connectedBody != targetHolder);
        targetConnectingJoint.autoConfigureConnectedAnchor = false;
        connectedOffset = transform.localPosition;
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

        targetHolder.gameObject.SetActive(true);
        Rigidbody targetRigid = target.selfRigidbody;

        IMovement targetMovement = target.selfMovement;
        targetMovement.MovementInputEnabled.AddModifier(false);
        targetMovement.HaltMovement();

        targetRigid.transform.position = targetHolder.position;
        targetRigid.transform.rotation = targetRigid.transform.rotation = targetHolder.rotation;
        targetConnectingJoint.connectedBody = targetRigid;

        holderConnectingJoint.connectedAnchor = connectedOffset; //prevent drift from physics shenannigans

        target.below = this;
        above = target;

        changeEventAll();
    }

    void DisconnectGrabbed() {
        Assert.IsTrue(above != null);

        IMovement targetMovement = above.selfMovement;

        targetConnectingJoint.connectedBody = null;

        targetMovement.MovementInputEnabled.RemoveModifier(false);

        targetHolder.gameObject.SetActive(false);
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

    public Stackable topmost {
        get {
            Stackable result = this;
            while (result.above != null) {
                result = result.above;
            }
            return result;
        }
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
        bool hasOutputSelf;
        bool started;

        public StackableRigidbodyEnumerator(Stackable target) {
            originalTarget = target;
            Reset();
        }

        public Rigidbody Current {
            get { return hasOutputSelf ? next.targetHolder : next.selfRigidbody; }
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

            if (!hasOutputSelf) {
                hasOutputSelf = true;
                return true;
            } else {
                hasOutputSelf = false;
                next = next.above;
                return next != null;
            }
        }

        public void Reset() {
            next = originalTarget.bottommost;
            hasOutputSelf = false;
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
            result.Add(stackable.targetHolder);
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
