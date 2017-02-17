using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IPlayerInputHolder))]
[RequireComponent(typeof(IPlayerInputHolder))]
public class BossSelection : MonoBehaviour, ISelection {

    [SerializeField]
    protected Text textPrompt;

    private bool ready = false;
    public bool Ready { get { return ready; } }

    IPlayerInputHolder input;
    public IPlayerInputHolder Input { get { return input; } }
    ReadyChecker readyChecker;
    private BossSceneHolder bossSceneHolder;

    void Awake() {
        input = GetComponent<IPlayerInputHolder>();
        Assert.IsNotNull(input);
    }

    void Start() {
        readyChecker = transform.root.GetComponentInChildren<ReadyChecker>();
        readyChecker.AddPlayer(this);

        bossSceneHolder = GameObject.FindObjectOfType<BossSceneHolder>();

        Callback.FireForUpdate(UpdatePrompt, this);
    }

    public bool CanSelect() {
        return !Ready;
    }

    public bool Select(ISelectable selectable) {
        if (Ready) {
            return false;
        }

        BossSelectable bossSelectable = selectable as BossSelectable;
        if (bossSelectable == null) {
            return false;
        }

        if(bossSelectable.gameObject.name == "BackButton") {
            readyChecker.LevelToLoad = "PlayerRegistration";
        } else {
            bossSceneHolder.bossToLoad = bossSelectable.gameObject.name;
        }

        ready = true;
        UpdatePrompt();
        return true;
    }

    public bool CanDeselect() {
        return Ready;
    }

    public bool Deselect() {
        if (!ready) {
            return false;
        } else {
            ready = false;
            UpdatePrompt();
            return true;
        }
    }

    void UpdatePrompt() {

        if (!PhotonNetwork.isMasterClient) {
            textPrompt.text = "";
            return;
        }

        if (Ready) {
            textPrompt.text = "";
        }else{
            textPrompt.text = string.Format("Press {0} to select", input.submitName);
        }
    }
}