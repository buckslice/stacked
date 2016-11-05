using UnityEngine;
using System.Collections;

public class ButtonCheckMenu : MonoBehaviour {
    public IPlayerInput bindings;

    private const float BUTTON_FREEZE_DELAY = .1f;
    private const float AXIS_FREEZE_DELAY = .2f;
    private float noInputUntil = -1f;

    private enum ButtonState
    {
        up,
        down,
        over
    }

    private struct MenuOption
    {
        public string name;
        public string currentBinding;
    }
    private MenuOption[] options;
    private int selectedNo = 0;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
