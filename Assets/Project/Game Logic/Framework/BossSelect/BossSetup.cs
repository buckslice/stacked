using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IBossSetup {
    BossSetup.BossSetupData BossData {
        get;
    }

    Transform[] PlayerSpawnPoints {
        get;
    }
}

public abstract class AbstractBossSetup : MonoBehaviour, IBossSetup {

    [System.Serializable]
    public class BossSetupData {
        [SerializeField]
        public BossSetupNetworkedData.BossID bossID;

        [SerializeField]
        public string sceneName;

        [SerializeField]
        public string bossName = "Boss";

        [SerializeField]
        public BossSetupNetworkedData.AbilityId[] abilities;
    }

    static IBossSetup main;
    public static IBossSetup Main { get { return main; } }

    public abstract BossSetupData BossData { get; }

    [SerializeField]
    protected Transform spawnPoint;

    [SerializeField]
    protected Transform[] playerSpawnPoints;
    public Transform[] PlayerSpawnPoints { get { return playerSpawnPoints; } }

    protected virtual void Awake() {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        if (main == null) {
            main = this;
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        if (this == (Object)Main) {
            main = null;
        }
    }

    protected void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {

        if (!PhotonNetwork.isMasterClient) {
            return;
        }
        if (arg0.name != BossData.sceneName) {
            return;
        }

        Debug.Log("Loading Boss");

        InstantiateBoss();
    }

    public abstract void InstantiateBoss();

    public static void DestroyAllBossSetups() {
        if (main != null) {
            Destroy((Main as Object as MonoBehaviour).transform.root.gameObject);
        }
    }
}

/// <summary>
/// Class to hold boss data, and to spawn said boss after the next scene change.
/// </summary>
public class BossSetup : AbstractBossSetup, IBossSetup {

    [SerializeField]
    protected BossSetupData bossData;
    public override BossSetupData BossData { get { return bossData; } }

    public override void InstantiateBoss()
    {
        BossSetupNetworkedData.Main.CreateBoss(bossData, spawnPoint);
        //Destruction now handled in game-end screens
    }
}
