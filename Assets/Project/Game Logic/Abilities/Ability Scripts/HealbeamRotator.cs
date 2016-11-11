using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class HealbeamRotator : AbstractAbilityAction {

    [SerializeField]
    protected Transform rotator;

    Stackable stackable;
    IPlayerInputHolder input;

    private enum State : byte {
        UNSTACKED,
        UP,
        DOWN,
    }

    State currentState = State.UNSTACKED;

	void Start () {
        stackable = GetComponentInParent<Stackable>();
        input = GetComponentInParent<IPlayerInputHolder>();
	}

    public override bool Activate(PhotonStream stream) {
        State newState;

        if (stream.isWriting) {

            if (!stackable.Stacked) {
                newState = State.UNSTACKED;
            } else if (stackable.Below == null) {
                newState = State.UP;
            } else if (stackable.Above == null) {
                newState = State.DOWN;
            } else {

                float inputDirection = input.movementDirection.y;
                if (inputDirection > 0) {
                    newState = State.UP;
                } else if (inputDirection < 0) {
                    newState = State.DOWN;
                } else {
                    newState = currentState;
                }
            }

            if (newState == currentState) { return false; } //no action required
            stream.SendNext(newState);

        } else {

            newState = (State)stream.ReceiveNext();
            Assert.IsTrue(newState != currentState);
        }

        switch (newState) {
            case State.UNSTACKED:
                rotator.rotation = Quaternion.identity;
                break;
            case State.UP:
                rotator.rotation = Quaternion.Euler(-90, 0, 0);
                break;
            case State.DOWN:
                rotator.rotation = Quaternion.Euler(90, 0, 0);
                break;
        }

        currentState = newState;
        return true;
    }
}
