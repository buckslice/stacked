using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class RegisteredPlayer : MonoBehaviour {

    static HashSet<RegisteredPlayer> registeredPlayers = new HashSet<RegisteredPlayer>();
    public static HashSet<RegisteredPlayer> RegisteredPlayers { get { return registeredPlayers; } }

    [SerializeField]
    protected int playerID = -1;

    private IPlayerInput input;
    public IPlayerInput inputBindings { get { return input; } set { input = value; } }

    protected virtual void Awake () {
        DontDestroyOnLoad(this.transform.root.gameObject);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        registeredPlayers.Add(this);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        registeredPlayers.Remove(this);
    }

    /// <summary>
    /// A constructor-style initializer.
    /// </summary>
    /// <param name="inputBindings"></param>
    public void Initalize(IPlayerInput inputBindings, int playerID)
    {
        this.inputBindings = inputBindings;
        this.playerID = playerID;
    }

    void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == Tags.Scenes.CharacterSelect)
        {
            CharacterSelectCursorNetworkedData.Main.CreateCharacterSelectCursor(input, (byte)playerID);
        }
    }
}
