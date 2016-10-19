using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(DamageHolder))]
public class TintedModel : MonoBehaviour {

    [SerializeField]
    protected Renderer[] tintedRenderers;
	void Start () {
        Player player = (Player)GetComponent<DamageHolder>().GetRootDamageTracker();

        if (player.PlayerID < Player.playerColoring.Length && player.PlayerID >= 0) {

            foreach (Renderer tintedRenderer in tintedRenderers) {
                tintedRenderer.material.color = Player.playerColoring[player.PlayerID];
            }
        }
	}
}
