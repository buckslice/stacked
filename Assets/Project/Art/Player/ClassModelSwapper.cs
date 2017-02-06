using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassModelSwapper : MonoBehaviour {

    public Object bearPrefab;
    public Object birdPrefab;
    public Object foxPrefab;
    public Object turtlePrefab;

    public Transform bodyParent;

    public void SwapModel(PlayerClass playerClass) {

        bodyParent.GetChild(0).gameObject.SetActive(false);

        switch (playerClass) {
            case PlayerClass.BEAR:
                Instantiate(bearPrefab, bodyParent, false);
                break;
            case PlayerClass.BIRD:
                Instantiate(birdPrefab, bodyParent, false);
                break;
            case PlayerClass.FOX:
                Instantiate(foxPrefab, bodyParent, false);
                break;
            case PlayerClass.TURTLE:
                Instantiate(turtlePrefab, bodyParent, false);
                break;
            default:
                break;

        }
    }
}
