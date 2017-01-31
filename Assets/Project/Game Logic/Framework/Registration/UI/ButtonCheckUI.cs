using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RegisteredPlayer))]
public class ButtonCheckUI : MonoBehaviour {

    [SerializeField]
    protected GameObject buttonCheckPrefab;

    public ButtonCheckMenu menu { get; private set; }

    // Use this for initialization
    void Start() {
        RegisteredPlayer player = GetComponent<RegisteredPlayer>();

        if (!player.locallyControlled) { return; }

        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        GameObject buttonCheckMenu = Instantiate(buttonCheckPrefab, GetComponent<IEntityUIGroupHolder>().EntityGroup.transform);

        menu = buttonCheckMenu.GetComponent<ButtonCheckMenu>();
        IPlayerInput bindings = player.inputBindings;
        menu.Initialize(bindings, player.PlayerID);
    }
}
