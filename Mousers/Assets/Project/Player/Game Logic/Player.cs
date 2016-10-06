using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Identifies the object as a player
/// </summary>
public class Player : MonoBehaviour {

    public delegate void PlayerListResized();

    static List<Player> allPlayers = new List<Player>();
    public static List<Player> AllPlayers { get { return allPlayers; } }

    public static event PlayerListResized playerListResized = delegate { };

    void OnDestroy()
    {
        allPlayers[playerID] = null;
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

        //ensure that our list-based lookup dictionary is large enough
        if(playerID >= allPlayers.Count)
        {
            do
            {
                allPlayers.Add(null);
            } 
            while (playerID >= allPlayers.Count);

            playerListResized();
        }

        Assert.IsTrue(allPlayers[playerID] == null, "Duplicate PlayerID " + playerID);
        allPlayers[playerID] = this;
    }

    /// <summary>
    /// Returns the first empty spot in the mapping of playerIDs
    /// </summary>
    /// <returns></returns>
    public static int getFirstFreePlayerID()
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
}
