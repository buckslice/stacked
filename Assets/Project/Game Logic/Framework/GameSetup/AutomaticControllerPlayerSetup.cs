using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Intended to be used for development only, in order to mock a player isntead of going through character select.
/// </summary>
public class AutomaticControllerPlayerSetup : PlayerSetup {
    protected override void Awake() {

        if (R41DNetworking.Main.NetworkingMode == R41DNetworkingMode.ONLINE) {
            DestroyImmediate(this.transform.root.gameObject);
            return;
        }

        base.Awake();
        PlayerSetup[] otherPlayerSetups = GameObject.FindObjectsOfType<PlayerSetup>();
        foreach (PlayerSetup otherPlayerSetup in otherPlayerSetups) {
            if (this != otherPlayerSetup && !(otherPlayerSetup is AutomaticControllerPlayerSetup) && !(otherPlayerSetup is AutomaticKeyboardPlayerSetup)) {
                DestroyImmediate(this.transform.root.gameObject);
                return;
            }
        }

        //I have no idea why this creates a null pointer exception when there exists more than one AutomaticSetup in the scene. Stuff still works even when the exception is thrown.
        inputBindings = new ControllerPlayerInput();
    }
}
