﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class RegisteredPlayer : MonoBehaviour, IPlayerID {

    static HashSet<RegisteredPlayer> registeredPlayers = new HashSet<RegisteredPlayer>();
    public static HashSet<RegisteredPlayer> RegisteredPlayers { get { return registeredPlayers; } }

    [SerializeField]
    protected int playerID = -1;
    public int PlayerID { get { return playerID; } }

    public bool locallyControlled { get; private set; }

    private IPlayerInput input;
    public IPlayerInput inputBindings { get { return input; } set { input = value; } }

    protected virtual void Awake () {
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
    public void Initalize(IPlayerInput inputBindings, int playerID, bool locallyControlled = true) {
        this.inputBindings = inputBindings;
        inputBindings.Initialize(this);
        this.playerID = playerID;
        this.locallyControlled = locallyControlled;

        if (locallyControlled) {
            DontDestroyOnLoad(this.transform.root.gameObject);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        registeredPlayers.Add(this);
    }

    protected void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == Tags.Scenes.CharacterSelect)
        {
            CharacterSelectCursorNetworkedData.Main.CreateCharacterSelectCursor(input, (byte)playerID);
        }
    }
}
