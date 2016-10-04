using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RegisteredPlayer : MonoBehaviour {

    [SerializeField]
    protected int playerID;

    private IPlayerInput input;
    public IPlayerInput inputBindings { get { return input; } set { input = value; } }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.transform.root.gameObject);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
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
