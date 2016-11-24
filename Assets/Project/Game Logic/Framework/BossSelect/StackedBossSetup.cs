using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class StackedBossSetup : AbstractBossSetup {

    [SerializeField]
    protected BossSetupData[] bossData;
    public override BossSetupData BossData { get { return bossData[0]; } }

    public override void InstantiateBoss() {

        GameObject[] instantiatedBosses = new GameObject[bossData.Length];

        for (int i = 0; i < bossData.Length; i++) {
            instantiatedBosses[i] = BossSetupNetworkedData.Main.CreateBoss(bossData[i], spawnPoint);
        }

        Callback.FireForUpdate(() => {
            for (int i = 1; i < instantiatedBosses.Length; i++) {
                instantiatedBosses[i].GetComponentInChildren<SetupTrigger>().Trigger(instantiatedBosses[i - 1]);
            }
        }, this);

        //Destruction now handled in game-end screens
    }
}
