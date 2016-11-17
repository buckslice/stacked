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
    public class PlayerSetupData {
        [SerializeField]
        [Tooltip("These abilities will be rebound as a basic attack")]
        public PlayerSetupNetworkedData.AbilityId[] basicAttacks = new PlayerSetupNetworkedData.AbilityId[] { PlayerSetupNetworkedData.AbilityId.GRAB };

        [SerializeField]
        [Tooltip("These abilities will be rebound as a firstAbility")]
        public PlayerSetupNetworkedData.AbilityId[] firstAbilities;

        [SerializeField]
        [Tooltip("These abilities will be rebound as a secondAbility")]
        public PlayerSetupNetworkedData.AbilityId[] secondAbilities;

        [SerializeField]
        [Tooltip("These abilities will retain their default bindings")]
        public PlayerSetupNetworkedData.AbilityId[] abilities = new PlayerSetupNetworkedData.AbilityId[] { PlayerSetupNetworkedData.AbilityId.JUMP, PlayerSetupNetworkedData.AbilityId.FIREBALL, PlayerSetupNetworkedData.AbilityId.REVIVE };

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
    public PlayerSetupData playerData;

    //private IPlayerInput input = null;
    //public IPlayerInput inputBindings { set { input = value; } }

    public IPlayerInput inputBindings { set; private get; }

    public bool AIPlayer = false;

    //additional player data goes here

    protected virtual void Awake() {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        DontDestroyOnLoad(this.transform.root.gameObject);
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    /// <summary>
    /// A constructor-style initializer.
    /// </summary>
    /// <param name="inputBindings"></param>
    /// <param name="playerNumber"></param>
    public void Initalize(IPlayerInput inputBindings, int playerNumber) {
        this.inputBindings = inputBindings;
        this.playerID = playerNumber;
    }

    void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
        //maybe make sure it's the right scene (or at least not a menu scene)
        if (arg0.name == Tags.Scenes.BossSelect || arg0.name == Tags.Scenes.CharacterSelect || arg0.name == Tags.Scenes.MainMenu
            || arg0.name == Tags.Scenes.Options || arg0.name == Tags.Scenes.PlayerRegistration || arg0.name == Tags.Scenes.VictoryPopup
            || arg0.name == Tags.Scenes.DefeatPopup) { return; }

        if (AIPlayer) {
            PlayerSetupNetworkedData.Main.CreateAIPlayer((byte)playerID, playerData);
        } else {
            PlayerSetupNetworkedData.Main.CreatePlayer((byte)playerID, inputBindings, playerData);
        }

        //Destruction now handled in game-end screens
    }

    public static void DestroyAllPlayerSetups() {
        foreach (PlayerSetup setup in GameObject.FindObjectsOfType<PlayerSetup>()) {
            Destroy(setup.transform.root.gameObject);
        }
    }
}
