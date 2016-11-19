using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// right now just for seeing where dead players are but 
// could be extended to work for identifying any sort of important thing happening offscreen
public class OffscreenTracker : MonoBehaviour {

    public GameObject deadPlayerMarker;

    List<Image> markers = new List<Image>();
    Camera cam;

    // Use this for initialization
    void Start() {
        cam = Camera.main;

        // spawn a marker for each player
        for (int i = 0; i < Player.Players.Count; ++i) {
            markers.Add(((GameObject)Instantiate(deadPlayerMarker, transform)).GetComponent<Image>());
            if (i >= 0 && i < 4) {
                markers[i].color = Player.playerColoring[Player.Players[i].PlayerID];
            }
            markers[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < Player.Players.Count; ++i) {
            if (Player.Players[i].dead) {
                // problem if camera is further forward than position
                Vector3 playerPos = Player.Players[i].Holder.transform.position;
                Vector2 screenPos = cam.WorldToScreenPoint(playerPos);
                // if position is behind camera then invert the screen coordinates
                if (Vector3.Dot(cam.transform.forward, (playerPos - cam.transform.position).normalized) < 0) {
                    screenPos *= -1.0f;
                }

                if (screenPos.x < 0 || screenPos.x > cam.pixelWidth ||
                    screenPos.y < 0 || screenPos.y > cam.pixelHeight) {
                    markers[i].gameObject.SetActive(true);
                    markers[i].transform.position = screenPos;
                } else {
                    markers[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
