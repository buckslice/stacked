using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IPlayerInputHolder))]
[RequireComponent(typeof(IPlayerInputHolder))]
public class BossSelection : MonoBehaviour, ISelection {

    [SerializeField]
    protected Image readyIndicator;

    [SerializeField]
    protected Text textPrompt;

    GameObject instantiatedBossSetup = null;
    public bool Ready { get { return instantiatedBossSetup != null; } }

    IPlayerInputHolder input;
    public IPlayerInputHolder Input { get { return input; } }

    void Awake() {
        input = GetComponent<IPlayerInputHolder>();
        Assert.IsNotNull(input);
    }

    void Start() {
        transform.root.GetComponentInChildren<ReadyChecker>().AddPlayer(this);
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

        instantiatedBossSetup = Instantiate(BossSetupNetworkedData.Main.bossDataPrefabs[(byte)bossSelectable.BossID]) as GameObject;
        readyIndicator.enabled = true;
        UpdatePrompt();
        return true;
    }

    public bool CanDeselect() { 
        return Ready; 
    }

    public bool Deselect() {
        if (instantiatedBossSetup == null) {
            return false;
        } else {
            Destroy(instantiatedBossSetup);
            instantiatedBossSetup = null; 
            readyIndicator.enabled = false;
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
            textPrompt.text = string.Format("Press {0} to proceed", input.startName);
        } else {
            textPrompt.text = string.Format("Press {0} to select", input.submitName);
        }
    }
}