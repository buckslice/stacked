using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;



/// <summary>
/// Identifies the object as a player
/// </summary>
[System.Serializable]
public class Player : AbstractDamageTracker {

    [SerializeField]
    protected int playerID;
    public int PlayerID { get { return playerID; } }

    static Dictionary<int, Player> players = new Dictionary<int, Player>(); // maps playerID to player
    public static Dictionary<int, Player> Players { get { return players; } }
    static int nextOpenPlayerIndex = 0;

    public Player(int ID, IDamageHolder holder) : base(holder) {
        Assert.IsFalse(players.ContainsKey(ID));
        Assert.IsTrue(playerID < 256, "Too many players for byte networked IDs");
        playerID = ID;
        players[playerID] = this;
        while (players.ContainsKey(nextOpenPlayerIndex)) {
            nextOpenPlayerIndex++;
        }
    }

    public Player(IDamageHolder holder) : this(nextOpenPlayerIndex, holder) { }

    public void Destroy() {
        Assert.IsTrue(playerID >= 0);
        players.Remove(playerID);   // remove player
        nextOpenPlayerIndex = Mathf.Min(nextOpenPlayerIndex, playerID);
        this.playerID = -1;
    }

    public static Player GetPlayerByID(int playerID) {
        return players[playerID];
    }
}
