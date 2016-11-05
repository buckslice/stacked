using UnityEngine;
using System.Collections;

public class ButtonCheckUI : MonoBehaviour {

    [SerializeField]
    protected GameObject buttonCheckPrefab;

    public ButtonCheckMenu menu { get; private set; }
    // Use this for initialization
    void Start () {
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Debug.Assert(canvasRoot, "Scene requires a UI canvas for healthbars!");

        GameObject buttonCheckMenu = (GameObject)Instantiate(buttonCheckPrefab, GetComponent<EntityUIGroupHolder>().EntityGroup.transform);
        (buttonCheckMenu.transform as RectTransform).Reset();
        int playerID = GetComponent<RegisteredPlayer>().PlayerID;
        RectTransform t = ((RectTransform)buttonCheckMenu.transform);
        print(playerID);
        if (playerID == 0 || playerID == 1) {
            t.offsetMax = new Vector2(0, -50);
            t.offsetMin = new Vector2(0, -100);
        }
        else {
            t.offsetMax = new Vector2(0, 150);
            t.offsetMin = new Vector2(0, 150);
        }
        menu = buttonCheckMenu.GetComponent<ButtonCheckMenu>();
        IPlayerInput bindings = GetComponent<RegisteredPlayer>().inputBindings;
        menu.bindings = bindings;
        menu.refreshOptions();
    }
}
