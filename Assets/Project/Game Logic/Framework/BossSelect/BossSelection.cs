using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class BossSelection : MonoBehaviour, ISelection {
    
    GameObject instantiatedBossSetup = null;
    public bool Ready { get { return instantiatedBossSetup != null; } }

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
            return true;
        }
    }
}
