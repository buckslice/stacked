using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RegisteredPlayer : MonoBehaviour {

    [SerializeField]
    protected int playerNumber;

    [SerializeField]
    private string cursorPrefabName = Tags.Resources.Cursor;

    private IPlayerInput input;
    public IPlayerInput inputBindings { set { input = value; } }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.transform.root.gameObject);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    /// <summary>
    /// A constructor-style initializer.
    /// </summary>
    /// <param name="inputBindings"></param>
    public void Initalize(IPlayerInput inputBindings, int playerNumber)
    {
        this.inputBindings = inputBindings;
        this.playerNumber = playerNumber;
    }

    void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == Tags.Scenes.CharacterSelect)
        {
            CreateCharacterSelectCursor();
        }
    }

    private void CreateCharacterSelectCursor()
    {
        GameObject cursor = PhotonNetwork.Instantiate(cursorPrefabName, Vector3.zero, Quaternion.identity, 0); //TODO: change spawn point based on player number
        cursor.GetComponent<PlayerInputHolder>().heldInput = input;
        cursor.GetComponent<CharacterSelectCursor>().Initialize(playerNumber);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
