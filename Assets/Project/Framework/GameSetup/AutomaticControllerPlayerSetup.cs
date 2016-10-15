using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Intended to be used for development only, in order to mock a player isntead of going through character select.
/// </summary>
public class AutomaticControllerPlayerSetup : PlayerSetup {
    protected override void Awake() {
        base.Awake();
        PlayerSetup[] otherPlayerSetups = GameObject.FindObjectsOfType<PlayerSetup>();
        foreach (PlayerSetup otherPlayerSetup in otherPlayerSetups) {
            if (this != otherPlayerSetup) {
                DestroyImmediate(this.transform.root.gameObject);
                return;
            }
        }
        inputBindings = new ControllerPlayerInput();

        Assert.IsTrue(R41DNetworking.Main.NetworkingMode != R41DNetworkingMode.ONLINE, "Automatic Setups will not work correctly over the network."); //we aren't hooked up correctly for online
    }
}
