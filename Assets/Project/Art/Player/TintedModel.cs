using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IPlayerID))]
public class TintedModel : MonoBehaviour {

    [SerializeField]
    protected Renderer[] tintedRenderers;

    [SerializeField]
    protected Image[] tintedImages; //both renderers and images have a .color property, but they don't share anything in the inheritance tree for it

    void Start () {
        int playerID = GetComponent<IPlayerID>().PlayerID;

        if (playerID < Player.playerColoring.Length && playerID >= 0) {

            foreach (Renderer tintedRenderer in tintedRenderers) {
                tintedRenderer.material.color = Player.playerColoring[playerID];
            }

            foreach (Image tintedImage in tintedImages) {
                tintedImage.color = Player.playerColoring[playerID];
            }
        }
	}
}
