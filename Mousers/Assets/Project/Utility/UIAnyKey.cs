using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Allows buttons to be activated by any keyboard action. Place on the same gameObject as the button.
/// </summary>
public class UIAnyKey : MonoBehaviour
{
    protected virtual void Update()
    {
        if (Input.anyKeyDown)
            SendEvent();

    }

    protected virtual void SendEvent() //can override this for different functionality
    {
        ExecuteEvents.Execute(this.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler); //hook into the button's code, and cause it to act as if it was pressed
    }
}

