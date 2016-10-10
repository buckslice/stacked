using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public interface IDamageHolder {
    IDamageTracker DamageTracker { get; }
}

public interface IDamageTracker : IDamageHolder {
    float DamageDealt { get; }
    void AddDamageDealt(float damage);
}

/// <summary>
/// Identifies the object as a player
/// </summary>
public class Player : MonoBehaviour, IDamageTracker {

    public delegate void PlayerListResized();

    static List<Player> allPlayers = new List<Player>();
    static Dictionary<int, int> playerIndices = new Dictionary<int, int>(); // maps playerID to index in player list
    public static List<Player> AllPlayers { get { return allPlayers; } }

    public static event PlayerListResized playerListResized = delegate { };

    void OnDestroy()
    {
        allPlayers.RemoveAt(playerIndices[playerID]);   // remove player
        playerIndices.Remove(playerID); // remove dictionary entry
    }

    [SerializeField]
    protected int playerID = -1;

    public int PlayerID { get { return playerID; } }

    public virtual bool isPlayer() { return true; }

    [ReadOnly]
    [SerializeField]
    protected float damageDealt = 0;
    public float DamageDealt { get { return damageDealt; } }
    public virtual void AddDamageDealt(float damage) { damageDealt += damage; }

    /// <summary>
    /// Constructor-like function to set up this class.
    /// </summary>
    /// <param name="playerID"></param>
    public void Initialize(int newPlayerID)
    {
        Assert.IsTrue(newPlayerID < 256, "Too many players for byte networked IDs");

        if (playerID != -1)
        {
            Debug.LogErrorFormat(this, "Already initialized to {0}", playerID);
            return;
        }

        this.playerID = newPlayerID;

        // make sure dictionary doesn't have playerID
        Assert.IsTrue(!playerIndices.ContainsKey(playerID), "Duplicate PlayerID " + playerID);

        allPlayers.Add(this);   // add player to list
        playerIndices[newPlayerID] = allPlayers.Count-1;    // add index to dictionary
    }

    public static Player GetPlayerByID(int playerID) {
        return allPlayers[playerIndices[playerID]];

        // could alternatively just forget dictionary and do a O(n) search each time 
        // since will only ever have a few number of players
        //for(int i = 0; i < allPlayers.Count; ++i) {
        //    if(allPlayers[i].playerID == playerID) {
        //        return allPlayers[i];
        //    }
        //}
        //return null;
    }

    /// <summary>
    /// Returns the first empty spot in the mapping of playerIDs
    /// </summary>
    /// <returns></returns>
    public static int GetFirstFreePlayerID()
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i] == null)
            {
                return i;
            }
        }
        return allPlayers.Count;
    }

    public IDamageTracker DamageTracker { get { return this; } }
}
