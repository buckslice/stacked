using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RegisteredPlayer))]
public class ButtonCheckUI : MonoBehaviour {

    [SerializeField]
    protected GameObject buttonCheckPrefab;

    public GameObject arrowUIPrefab;

    public ButtonCheckMenu menu { get; private set; }

    // Use this for initialization
    void Start() {
        RegisteredPlayer player = GetComponent<RegisteredPlayer>();

        if (!player.locallyControlled) { return; }

        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        GameObject buttonCheckMenu = Instantiate(buttonCheckPrefab, GetComponent<IEntityUIGroupHolder>().EntityGroup.transform);
        buttonCheckMenu.transform.localScale = Vector3.one;
        menu = buttonCheckMenu.GetComponent<ButtonCheckMenu>();
        IPlayerInput bindings = player.inputBindings;
        menu.Initialize(bindings, player.PlayerID);

        // if not in keyboard mode then spawn some arrows to show you can switch between xbox and ps4
        if (!menu.keyboardImage.GetActive()) {
            GameObject arrowUI = Instantiate(arrowUIPrefab, GetComponent<IEntityUIGroupHolder>().EntityGroup.transform);
            arrowUI.transform.localScale = Vector3.one;
        }

    }
}
