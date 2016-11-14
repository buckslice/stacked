using UnityEngine;
using System.Collections;

public class ScreenShake : AbstractAbilityAction {

    CameraShakeScript cameraShakeScript;

    [SerializeField]
    protected float screenShakeDuration = 0.5f;

    protected override void Start() {
        base.Start();
        cameraShakeScript = Camera.main.GetComponent<CameraShakeScript>();
    }

    public override bool Activate(PhotonStream stream) {
        cameraShakeScript.screenShake(screenShakeDuration);
        return true;
    }
}
