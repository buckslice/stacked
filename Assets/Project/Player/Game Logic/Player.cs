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

    static List<Player> playersList = new List<Player>();
    static Dictionary<int, int> playerIndices = new Dictionary<int, int>(); // maps playerID to player's index in the list
    public static List<Player> Players { get { return playersList; } }
    public static Dictionary<int, int> PlayersIndices { get { return playerIndices; } }
    static int nextOpenPlayerIndex = 0;

    public Player(int ID, IDamageHolder holder) : base(holder) {
        Assert.IsFalse(playerIndices.ContainsKey(ID));
        Assert.IsTrue(playerID < 256, "Too many players for byte networked IDs");
        playerID = ID;
        int playersListIndex = playersList.Count;
        playersList.Insert(playersListIndex, this);
        playerIndices[playerID] = playersListIndex;
    }

    public Player(IDamageHolder holder) : this(nextOpenPlayerIndex, holder) { }

    public void Destroy() {
        Assert.IsTrue(playerID >= 0);
        int playersListIndex = playerIndices[playerID];
        playersList.RemoveAt(playersListIndex);
        playerIndices.Remove(playerID);

        this.playerID = -1;
    }

    public static int randomPlayerID() {
        int index = Random.Range(0, playersList.Count);
        return playersList[index].playerID;
    }

    public static bool ContainsPlayerID(int playerID) {
        return playerIndices.ContainsKey(playerID);
    }

    public static Player GetPlayerByID(int playerID) {
        return playersList[playerIndices[playerID]];
    }
}
