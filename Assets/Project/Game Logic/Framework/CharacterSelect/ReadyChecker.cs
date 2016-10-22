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
    const float startTime = 2.99f;
    float countDownTime = startTime;

    Text text;

    public void AddPlayer(ISelection player) {
        players.Add(player);
    }

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}

    void checkNotReady() {
        if (!AllPlayersReady()) {
            state = State.NOTREADY;
            countDownTime = startTime;
            text.text = "";
        }
    }
	
	// Update is called once per frame
	void Update () {
        switch (state) {
            case State.NOTREADY:
                if (AllPlayersReady()) {
                    state = State.READY;
                }
                break;

            case State.READY:
                foreach (ISelection player in players) {
                    if (player.Input.getSubmitDown || player.Input.getStartDown) {
                        state = State.COUNTINGDOWN;
                        break;
                    }
                }

                checkNotReady();
                break;

            case State.COUNTINGDOWN:

                countDownTime -= Time.deltaTime;
                int intTime = (int)countDownTime;
                transform.localScale = Vector3.one * (1.0f + (countDownTime - intTime) * 2.5f);
                text.text = "" + (intTime + 1);

                //countdown complete
                if (countDownTime <= 0.0f) {
                    SceneManager.LoadScene(levelToLoad);
                    return;
                }

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
        /*
        if (!countingDown && AllPlayersReady()) {
            countingDown = true;
        }else if(countingDown && !AllPlayersReady()) {
            countingDown = false;
            countDownTime = startTime;
            text.text = "";
        }else if (countingDown) {
            countDownTime -= Time.deltaTime;
            int intTime = (int)countDownTime;
            transform.localScale = Vector3.one * (1.0f + (countDownTime - intTime)*2.5f);
            text.text = "" + (intTime + 1);

            //countdown complete
            if(countDownTime <= 0.0f) {
                SceneManager.LoadScene(levelToLoad);
                return;
            }

            //skip countdown
            foreach (ISelection player in players) {
                if (player.Input.getAbility1Down || player.Input.getAbility2Down || player.Input.getBasicAttackDown) {
                    SceneManager.LoadScene(levelToLoad);
                    return;
                }
            }
        }
         * */
	}

    bool AllPlayersReady() {
        if(players.Count == 0) {
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
