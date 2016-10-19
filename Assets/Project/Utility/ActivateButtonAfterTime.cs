using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ActivateButtonAfterTime : MonoBehaviour {

    [SerializeField]
    protected float time = 1;

    protected float startTime = 0;

    void Awake() {
        startTime = Time.time;
    }

    protected virtual void Update() {
        if (Time.time > startTime + time)
            SendEvent();
    }

    protected virtual void SendEvent() //can override this for different functionality
    {
        ExecuteEvents.Execute(this.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler); //hook into the button's code, and cause it to act as if it was pressed
    }
}
