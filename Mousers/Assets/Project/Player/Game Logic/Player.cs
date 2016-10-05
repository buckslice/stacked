using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Identifies the object as a player
/// </summary>
public class Player : MonoBehaviour {

    static Dictionary<int, Player> allPlayers = new Dictionary<int, Player>();

    void OnDestroy()
    {
        allPlayers.Remove(playerID);
    }

    [SerializeField]
    protected int playerID = -1;

    public int PlayerID { get { return playerID; } }

    /// <summary>
    /// Constructor-like function to set up this class.
    /// </summary>
    /// <param name="playerID"></param>
    public void Initialize(int playerID)
    {
        if (playerID != -1)
        {
            Debug.LogError("Already initialized.");
            return;
        }

        this.playerID = playerID;
        Assert.IsFalse(allPlayers.ContainsKey(playerID), "Duplicate PlayerIDs");
        allPlayers[playerID] = this;
    }
}
