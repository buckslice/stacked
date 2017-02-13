using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Intended to be used for development only, in order to mock a player isntead of going through character select.
/// </summary>
public class AutomaticBossSetup : BossSetup {
    protected override void Awake() {

        if (R41DNetworking.Main != null && R41DNetworking.Main.NetworkingMode == R41DNetworkingMode.ONLINE) {
            DestroyImmediate(this.transform.root.gameObject);
            return;
        }

        AbstractBossSetup[] otherBossSetups = GameObject.FindObjectsOfType<AbstractBossSetup>();
        foreach (AbstractBossSetup otherBossSetup in otherBossSetups) {
            if (this != otherBossSetup) {
                DestroyImmediate(this.transform.root.gameObject);
                return;
            }
        }

        base.Awake();

    }

    void Start() {
        Assert.IsTrue(R41DNetworking.Main.NetworkingMode != R41DNetworkingMode.ONLINE, "Automatic Setups will not work correctly over the network."); //we aren't hooked up correctly for online
    }
}
