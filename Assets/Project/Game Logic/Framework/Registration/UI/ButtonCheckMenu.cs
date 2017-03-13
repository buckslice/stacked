using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonCheckMenu : MonoBehaviour {
    private IPlayerInput bindings;
    private int playerID = -1;
    public bool ready {
        get { return PlayerRegistration.Main.RegisteredPlayers[playerID].ready; }
        private set {
            PlayerRegistration.Main.setPlayerReady((byte)playerID, value);
        }
    }

    private const float REPEAT_DELAY = 0.2f;
    private float currentDelay;
    private bool startMenu;

    public GameObject xboxImage;
    public GameObject ps4Image;
    public GameObject keyboardImage;
    bool isXboxBindings = false;

    void Start() {
        currentDelay = 0;
        startMenu = true;
        ready = false;
    }

    public void Initialize(IPlayerInput bindings, int playerID) {
        // make sure all are set to false at beginning
        xboxImage.SetActive(false);
        ps4Image.SetActive(false);
        keyboardImage.SetActive(false);

        this.bindings = bindings;
        if (bindings.GetType() == typeof(ControllerPlayerInput)) {
            ps4Image.SetActive(true);
            ControllerPlayerInput controllerInput = (ControllerPlayerInput)bindings;
            controllerInput.setControllerType(false);
        }
        else if (bindings.GetType() == typeof(XinputPlayerInput)) {
            xboxImage.SetActive(true);
        }
        else {
            keyboardImage.SetActive(true);
        }

        this.playerID = playerID;
    }

    // Update is called once per frame
    void Update() {
        if (!ready) {
            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0
                && bindings.movementDirection.sqrMagnitude > 0.25f
                && bindings.GetType() == typeof(ControllerPlayerInput)) {

                isXboxBindings = !isXboxBindings;

                ControllerPlayerInput controllerInput = (ControllerPlayerInput)bindings;
                controllerInput.setControllerType(isXboxBindings);

                xboxImage.SetActive(isXboxBindings);
                ps4Image.SetActive(!isXboxBindings);
                keyboardImage.SetActive(false);

                currentDelay = REPEAT_DELAY;
            }

            if (bindings.getStartDown || bindings.getSubmitDown) {
                ready = true;
            }
            if (bindings.getCancelDown) {
                PlayerRegistration.Main.removePlayer((byte)playerID);
            }
        } else {
            if (bindings.getCancelDown) {
                ready = false;
            }
        }
    }
}
