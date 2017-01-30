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

        GameObject buttonCheckMenu = (GameObject)Instantiate(buttonCheckPrefab, GetComponent<IEntityUIGroupHolder>().EntityGroup.transform);
        (buttonCheckMenu.transform as RectTransform).Reset();
        RectTransform t = ((RectTransform)buttonCheckMenu.transform);


        if (player.PlayerID == 0 || player.PlayerID == 1) {
            t.offsetMax = new Vector2(0, -50);
            t.offsetMin = new Vector2(0, -100);
        } else {
            t.offsetMax = new Vector2(0, 100);
            t.offsetMin = new Vector2(0, 50);
        }
        menu = buttonCheckMenu.GetComponent<ButtonCheckMenu>();
        IPlayerInput bindings = player.inputBindings;
        menu.Initialize(bindings, player.PlayerID);
    }
}
