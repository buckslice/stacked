using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text))]
public class ReadyChecker : MonoBehaviour {

    private enum State {
        NOTREADY,
        READY,
        COUNTINGDOWN
    }

    [SerializeField]
    protected string levelToLoad;
    public string LevelToLoad { get { return levelToLoad; } set { levelToLoad = value; } }

    List<ISelection> players = new List<ISelection>();
    State state = State.NOTREADY;
    public float countDownTime = 3.0f;
    float timer;

    Text text;

    public void AddPlayer(ISelection player) {
        players.Add(player);
    }

    // Use this for initialization
    void Start() {
        timer = countDownTime;
        text = GetComponent<Text>();
        if (!PhotonNetwork.isMasterClient) {
            //only master client can change scenes.
            //TODO: add some way to show the countdown on all clients
            this.enabled = false;
        }
    }

    public bool ArePlayersReady() {
        return state != State.NOTREADY;
    }

    void checkNotReady() {
        if (!AllPlayersReady()) {
            state = State.NOTREADY;
            timer = countDownTime;
            text.text = "";
        }
    }

    // Update is called once per frame
    void Update() {
        switch (state) {
            case State.NOTREADY:
                if (AllPlayersReady()) {
                    state = State.READY;
                }
                break;

            case State.READY:
                text.text = "Press Start to Begin!";
                text.fontSize = 50;
                transform.localScale = Vector3.one;
                foreach (ISelection player in players) {
                    if (player.Input.getSubmitDown || player.Input.getStartDown) {
                        state = State.COUNTINGDOWN;
                        break;
                    }
                }

                checkNotReady();
                break;

            case State.COUNTINGDOWN:
                timer -= Time.deltaTime;
                //countdown complete
                if (timer <= 0.0f) {
                    SceneManager.LoadScene(levelToLoad);
                    return;
                }

                transform.localScale = Vector3.one * (1.0f + (timer - (int)timer) * 2.5f);
                text.text = "" + (int)(timer + 1.0f);
                text.fontSize = 180;

                //skip countdown
                foreach (ISelection player in players) {
                    if (player.Input.getAbility1Down || player.Input.getAbility2Down || player.Input.getBasicAttackDown) {
                        SceneManager.LoadScene(levelToLoad);
                        return;
                    }
                }

                checkNotReady();
                break;
        }
    }

    bool AllPlayersReady() {
        if (players.Count == 0) {
            return false;
        }
        for (int i = 0; i < players.Count; ++i) {
            if (!players[i].Ready) {
                return false;
            }
        }
        return true;
    }
}
