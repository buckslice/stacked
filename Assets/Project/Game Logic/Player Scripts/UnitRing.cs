using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class UnitRing : MonoBehaviour {

    Stackable stackable;

	void Start () {
        stackable = GetComponentInParent<Stackable>();
        stackable.changeEvent += Stackable_changeEvent;
	}

    private void Stackable_changeEvent() {
        this.gameObject.SetActive(stackable.Below == null); //only active if there is nothing below us.
    }

    void Update () {
        Vector3 flooredPosition = transform.position;
        flooredPosition.y = 0.01f;
        transform.position = flooredPosition;
	}
}
