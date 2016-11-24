using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class StackableInterpolation : MonoBehaviour {

    [SerializeField]
    protected float speed = 1;

    Stackable stackable;
    Coroutine activeRoutine;

	void Start () {
        stackable = GetComponentInParent<Stackable>();
        stackable.changeEvent += Stackable_changeEvent;
        Assert.IsTrue(transform.localPosition == Vector3.zero);
    }

    private void Stackable_changeEvent() {
        if(activeRoutine != null) {
            StopCoroutine(activeRoutine);
            activeRoutine = null;
        }
        activeRoutine = StartCoroutine(InterpolateToPosition());
    }

    IEnumerator InterpolateToPosition() {
        Vector3 worldPosition = transform.position;

        while(true) {
            yield return null;
            worldPosition = Vector3.MoveTowards(worldPosition, transform.parent.position, speed * Time.deltaTime);
            transform.position = worldPosition;
            if((worldPosition - transform.parent.position).magnitude == 0) {
                activeRoutine = null;
                yield break; //end
            }
        }
    }
}
