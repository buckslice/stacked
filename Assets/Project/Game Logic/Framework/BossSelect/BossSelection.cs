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

    GameObject instantiatedBossSetup = null;
    public bool Ready { get { return instantiatedBossSetup != null; } }

    IPlayerInputHolder input;
    public IPlayerInputHolder Input { get { return input; } }

    void Awake() {
        input = GetComponent<IPlayerInputHolder>();
    }

    void Start() {
        transform.root.GetComponentInChildren<ReadyChecker>().AddPlayer(this);
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
            return true;
        }
    }
}
