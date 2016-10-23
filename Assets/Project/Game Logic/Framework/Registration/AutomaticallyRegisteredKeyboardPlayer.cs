using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Intended to be used for development only, in order to mock a registered player isntead of going through player registration.
/// </summary>
public class AutomaticallyRegisteredKeyboardPlayer : RegisteredPlayer {
    protected override void Awake()
    {
        base.Awake();
        RegisteredPlayer[] otherRegisteredPlayers = GameObject.FindObjectsOfType<RegisteredPlayer>();
        foreach (RegisteredPlayer otherRegisteredPlayer in otherRegisteredPlayers)
        {
            if (this != otherRegisteredPlayer)
            {
                DestroyImmediate(this.transform.root.gameObject);
                return;
            }
        }
        inputBindings = new KeyboardMousePlayerInput();

        // this is a race condition with R41DNetworking.Main initialization (both in Awake)
        //Assert.IsTrue(R41DNetworking.Main.NetworkingMode != R41DNetworkingMode.ONLINE, "Automatic Setups will not work correctly over the network."); //we aren't hooked up correctly for online
    }
}
