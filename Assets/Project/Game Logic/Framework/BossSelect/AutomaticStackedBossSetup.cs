using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class AutomaticStackedBossSetup : StackedBossSetup {

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
