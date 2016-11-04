using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class VibrateOnBossDeath : MonoBehaviour {

    [SerializeField]
    protected float vibrationStrength = 0.5f;

    [SerializeField]
    protected float vibrationDuration = 0.5f;
    
    void OnDestroy() {
        foreach (ControllerPlayerInput controller in ControllerPlayerInput.allControllers) {
            if (controller != null) {
                controller.Vibrate(vibrationStrength, vibrationDuration);
            }
        }
    }
}
