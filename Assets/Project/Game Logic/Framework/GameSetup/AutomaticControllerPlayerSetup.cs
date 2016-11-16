using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;


/// <summary>
/// Intended to be used for development only, in order to mock a player isntead of going through character select.
/// </summary>
public class AutomaticControllerPlayerSetup : PlayerSetup {
    [SerializeField]
    protected ControllerPlayerInput.AxisType bindings = ControllerPlayerInput.AxisType.XBOX;
    [SerializeField]
    protected XInputDotNetPure.PlayerIndex controllerIndex = PlayerIndex.One;


    private ControllerPlayerInput instantiatedBindings;

    protected override void Awake() {

        if (R41DNetworking.Main != null && R41DNetworking.Main.NetworkingMode == R41DNetworkingMode.ONLINE) {
            DestroyImmediate(this.transform.root.gameObject);
            return;
        }

        PlayerSetup[] otherPlayerSetups = GameObject.FindObjectsOfType<PlayerSetup>();
        foreach (PlayerSetup otherPlayerSetup in otherPlayerSetups) {
            if (this != otherPlayerSetup &&
                !(otherPlayerSetup is AutomaticControllerPlayerSetup) &&
                !(otherPlayerSetup is AutomaticKeyboardPlayerSetup) &&
                !otherPlayerSetup.AIPlayer) {
                //if there exists a player setup that was set up properly, instead of for quick setup, delete the one for quick setup (ourselves)
                DestroyImmediate(this.transform.root.gameObject);
                return;
            }
        }

        base.Awake();


        //I have no idea why this creates a null pointer exception when there exists more than one AutomaticSetup in the scene. Stuff still works even when the exception is thrown.

        instantiatedBindings = new ControllerPlayerInput();
        inputBindings = instantiatedBindings;
    }

    void Start() {
        instantiatedBindings.controllerIndex = controllerIndex;
        instantiatedBindings.rebindToDefault();
        if (bindings == ControllerPlayerInput.AxisType.PS4) {
            instantiatedBindings.swapControllerType();
        }
    }
}
