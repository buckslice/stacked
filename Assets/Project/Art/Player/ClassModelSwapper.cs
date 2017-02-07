using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassModelSwapper : MonoBehaviour {

    public Object bearPrefab;
    public Object birdPrefab;
    public Object foxPrefab;
    public Object turtlePrefab;

    public Transform bodyParent;

    private PlayerClass playerClass;

    public void SwapModel(PlayerClass playerClass) {
        this.playerClass = playerClass;
    }

    void Start() {
        bodyParent.GetChild(0).gameObject.SetActive(false);

        GameObject go = null;
        switch (playerClass) {
            case PlayerClass.BEAR:
                go = (GameObject)Instantiate(bearPrefab, bodyParent, false);
                break;
            case PlayerClass.BIRD:
                go = (GameObject)Instantiate(birdPrefab, bodyParent, false);
                break;
            case PlayerClass.FOX:
                go = (GameObject)Instantiate(foxPrefab, bodyParent, false);
                break;
            case PlayerClass.TURTLE:
                go = (GameObject)Instantiate(turtlePrefab, bodyParent, false);
                break;
            default:
                break;

        }

        int playerID = GetComponent<IPlayerID>().PlayerID;

        if (go && playerID < Player.playerColoring.Length && playerID >= 0) {
            go.GetComponent<Renderer>().material.color = Player.playerColoring[playerID];
        }
    }
}
