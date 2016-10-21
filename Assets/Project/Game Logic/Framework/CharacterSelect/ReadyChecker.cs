using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadyChecker : MonoBehaviour {

    private string levelToLoad;

    List<PlayerCursor> players = new List<PlayerCursor>();
    bool countingDown = false;
    const float startTime = 2.99f;
    float countDownTime = startTime;

    Text text;

    public void AddPlayer(PlayerCursor player) {
        players.Add(player);
    }

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        Assert.IsNotNull(BossSetup.Main);
        levelToLoad = BossSetup.Main.BossData.sceneName;
	}
	
	// Update is called once per frame
	void Update () {
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
            foreach (PlayerCursor player in players) {
                if (player.Input.getAbility1Down || player.Input.getAbility2Down || player.Input.getBasicAttackDown) {
                    SceneManager.LoadScene(levelToLoad);
                    return;
                }
            }
        }
	}

    bool AllPlayersReady() {
        if(players.Count == 0) {
            return false;
        }
        for (int i = 0; i < players.Count; ++i) {
            if (!players[i].ready) {
                return false;
            }
        }
        return true;
    }
}
