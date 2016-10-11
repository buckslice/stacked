using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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

        public byte[] toByteArray() {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream()) {
                bf.Serialize(ms, this);
                return ms.ToArray();
            }
        }

        public static PlayerSetupData fromByteArray(byte[] serializedData) {
            using (MemoryStream memStream = new MemoryStream()) {
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(serializedData, 0, serializedData.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                PlayerSetupData obj = (PlayerSetupData)binForm.Deserialize(memStream);
                return obj;
            }
        }
    }

    [SerializeField]
    protected int playerID = -1;

    [SerializeField]
    protected PlayerSetupData playerData;

    private IPlayerInput input;
    public IPlayerInput inputBindings { set { input = value; } }

    //additional player data goes here

    protected virtual void Awake () {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        DontDestroyOnLoad(this.transform.root.gameObject);
	}

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
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
        Destroy(this.transform.root.gameObject);
        //player was created, our job is done. May want to change this so that the player's spawning data is persisted.
    }

}
