using UnityEngine;
using System.Collections;

public class VibrateControllerScript : AbstractAbilityAction {

    [SerializeField]
    protected float vibrationStrength = 0.5f;

    [SerializeField]
    protected float vibrationDuration = 0.5f;

    // Use this for initialization
    protected override void Start () {
        base.Start();
    }

    public override bool Activate(PhotonStream stream) {
        //foreach (ControllerPlayerInput controller in ControllerPlayerInput.allControllers) {
        //    if (controller != null) {
        //        controller.Vibrate(vibrationStrength, vibrationDuration);
        //    }
        //}
        return false;
    }
}
