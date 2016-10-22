using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BossSelectable : MonoBehaviour, ISelectable {

    [SerializeField]
    protected Text titleText;

    [SerializeField]
    protected BossSetupNetworkedData.BossID bossID;
    public BossSetupNetworkedData.BossID BossID { get { return bossID; } }

    void Start() {
        titleText.text = BossSetupNetworkedData.Main.bossDataPrefabs[(byte)bossID].GetComponent<BossSetup>().BossData.sceneName;
    }
}
