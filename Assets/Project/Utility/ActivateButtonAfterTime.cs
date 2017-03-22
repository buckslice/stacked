using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActivateButtonAfterTime : MonoBehaviour {

    public Image panel;
    public Text timerText;

    [SerializeField]
    protected float time = 1;

    protected float startTime = 0;

    AudioSource music;

    void Start() {
        startTime = Time.realtimeSinceStartup;
        if (MusicSingleton.Main) {
            MusicSingleton.Main.FadeAndDestroy(time);
            MusicSingleton.Main.GetComponent<AudioSource>();
        }
    }

    protected virtual void Update() {
        float timeLeft = startTime + time - Time.realtimeSinceStartup;

        // set countdown on text
        if (timerText) {
            timerText.text = ((int)(timeLeft + 1)).ToString();
        }

        // fade out a panel
        if (panel) {
            Color c = panel.color;
            c.a = Mathf.Lerp(0.0f, 1.0f, 1.0f - timeLeft / time);
            panel.color = c;
        }

        // slow timescale
        Time.timeScale = Mathf.Max(0.01f, timeLeft / time);

        if (Time.realtimeSinceStartup > startTime + time) {
            Time.timeScale = 1.0f;
            SendEvent();
        }
    }

    protected virtual void SendEvent() //can override this for different functionality
    {
        ExecuteEvents.Execute(this.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler); //hook into the button's code, and cause it to act as if it was pressed
    }
}
