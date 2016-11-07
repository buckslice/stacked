using UnityEngine;
using UnityEngine.SceneManagement;
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

        if (R41DNetworking.Main.NetworkingMode == R41DNetworkingMode.ONLINE) {
            DestroyImmediate(this.transform.root.gameObject);
            return;
        }

        RegisteredPlayer[] otherRegisteredPlayers = GameObject.FindObjectsOfType<RegisteredPlayer>();
        foreach (RegisteredPlayer otherRegisteredPlayer in otherRegisteredPlayers)
        {
            if (this != otherRegisteredPlayer)
            {
                DestroyImmediate(this.transform.root.gameObject);
                return;
            }
        }
        Initalize(new KeyboardMousePlayerInput(), playerID);
    }
}
