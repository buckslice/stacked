using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Activates a button based on a key press.
/// </summary>
public class UIKeyboardShortcut : UIAnyKey
{
    [SerializeField]
    public KeyCode key;

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetKeyDown(key))
            SendEvent();
    }
}

