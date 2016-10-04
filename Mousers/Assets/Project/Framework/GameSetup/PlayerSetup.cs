using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class to hold player data, and to spawn said player after the next scene change.
/// </summary>
public class PlayerSetup : MonoBehaviour {

    [System.Serializable]
    public class PlayerSetupData
    {
        [SerializeField]
        [Tooltip("These abilities will be rebound as a firstAbility")]
        public PlayerSetupNetworkedData.AbilityId[] firstAbilities;

        [SerializeField]
        [Tooltip("These abilities will be rebound as a secondAbility")]
        public PlayerSetupNetworkedData.AbilityId[] secondAbilities;

        [SerializeField]
        [Tooltip("These abilities will retain their default bindings")]
        public PlayerSetupNetworkedData.AbilityId[] abilities;
    }

    [SerializeField]
    protected int playerID = -1;

    [SerializeField]
    protected PlayerSetupData playerData;

    private IPlayerInput input;
    public IPlayerInput inputBindings { set { input = value; } }

    //additional player data goes here

    void Start () { //move to be awake
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        DontDestroyOnLoad(this.transform.root.gameObject);
	}

    /// <summary>
    /// A constructor-style initializer.
    /// </summary>
    /// <param name="inputBindings"></param>
    /// <param name="playerNumber"></param>
    public void Initalize(IPlayerInput inputBindings, int playerNumber)
    {
        this.inputBindings = inputBindings;
        this.playerID = playerNumber;
    }

    void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //TODO: maybe make sure it's the right scene?
        PlayerSetupNetworkedData.Main.CreatePlayer((byte)playerID, input, playerData);
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        Destroy(this.transform.root.gameObject);
        //player was created, our job is done. May want to change this so that the player's spawning data is persisted.
    }
	
	void Update () {
        // temp to just test spawning a boss

        if (Input.GetKeyDown(KeyCode.P)) {
            PhotonNetwork.Instantiate(Tags.Resources.Boss, new Vector3(Random.Range(-40, 40), 0.0f, Random.Range(-40, 40)), Quaternion.identity, 0);
        }
            
	}
}
