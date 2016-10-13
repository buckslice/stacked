using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class to hold boss data, and to spawn said boss after the next scene change.
/// </summary>
public class BossSetup : MonoBehaviour
{
    static BossSetup main;
    public static BossSetup Main { get { return main; } }

    [System.Serializable]
    public class BossSetupData
    {
        [SerializeField]
        public string sceneName;

        [SerializeField]
        public BossSetupNetworkedData.AbilityId[] abilities;
    }

    [SerializeField]
    protected BossSetupData bossData;

    [SerializeField]
    protected Transform spawnPoint;

    [SerializeField]
    protected Transform[] playerSpawnPoints;
    public Transform[] PlayerSpawnPoints { get { return playerSpawnPoints; } }

    protected virtual void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        if (main != null)
        {
            Destroy(Main.transform.root.gameObject);
        }
        main = this;
        DontDestroyOnLoad(this.transform.root.gameObject);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        if (Main == this)
        {
            main = null;
        }
    }

    void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (!PhotonNetwork.isMasterClient)
        {
            return;
        }
        if (arg0.name != bossData.sceneName)
        {
            return;
        }
        BossSetupNetworkedData.Main.CreateBoss(bossData, spawnPoint);
        Destroy(this.transform.root.gameObject);
        //boss was created, our job is done. May want to change this so that the boss's spawning data is persisted.
    }
}
