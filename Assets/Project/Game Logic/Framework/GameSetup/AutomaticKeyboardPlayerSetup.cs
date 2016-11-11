using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Intended to be used for development only, in order to mock a player isntead of going through character select.
/// </summary>
public class AutomaticKeyboardPlayerSetup : PlayerSetup
{
    protected override void Awake()
    {
        base.Awake();

        // What is purpose? why would you want to delete yourself if there are other PlayerSetups?

        //PlayerSetup[] otherPlayerSetups = GameObject.FindObjectsOfType<PlayerSetup>();
        //foreach (PlayerSetup otherPlayerSetup in otherPlayerSetups)
        //{
        //    if (this != otherPlayerSetup && !(otherPlayerSetup is AutomaticControllerPlayerSetup) && !(otherPlayerSetup is AutomaticKeyboardPlayerSetup))
        //    {
        //        DestroyImmediate(this.transform.root.gameObject);
        //        return;
        //    }
        //}

        //I have no idea why this creates a null pointer exception when there exists more than one AutomaticSetup in the scene. Stuff still works even when the exception is thrown.
        inputBindings = new KeyboardMousePlayerInput();

        Assert.IsTrue(R41DNetworking.Main.NetworkingMode != R41DNetworkingMode.ONLINE, "Automatic Setups will not work correctly over the network."); //we aren't hooked up correctly for online
    }
}
