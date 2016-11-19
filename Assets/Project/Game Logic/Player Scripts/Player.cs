using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface for anything which contains a PlayerID
/// </summary>
public interface IPlayerID {
    int PlayerID { get; }
}

/// <summary>
/// Identifies the object as a player
/// </summary>
[System.Serializable]
public class Player : AbstractDamageTracker, IPlayerID {

    [SerializeField]
    protected int playerID;
    public int PlayerID { get { return playerID; } }

    public bool dead = false;

    static List<Player> playersList = new List<Player>();
    static Dictionary<int, int> playerIndices = new Dictionary<int, int>(); // maps playerID to player's index in the list
    public static List<Player> Players { get { return playersList; } }
    public static Dictionary<int, int> PlayersIndices { get { return playerIndices; } }
    static int nextOpenPlayerIndex = 0;

    static Dictionary<int, HashSet<int>> playersOnElevation = new Dictionary<int, HashSet<int>>();
    public static ICollection<int> PlayersOnElevation(int elevation) { ensureElevation(elevation); return playersOnElevation[elevation]; }

    Stackable stackable;
    int stackElevation = 0;

    public Player(int ID, IDamageHolder holder) : base(holder) {
        Assert.IsFalse(playerIndices.ContainsKey(ID), ID.ToString());
        Assert.IsTrue(playerID < 256, "Too many players for byte networked IDs");
        playerID = ID;

        int playersListIndex = playersList.Count;
        playersList.Insert(playersListIndex, this);
        playerIndices[playerID] = playersListIndex;

        while (playerIndices.ContainsKey(nextOpenPlayerIndex)) {
            nextOpenPlayerIndex++;
        }

        //initialize ourselves in player elevation data structures
        stackable = (holder as MonoBehaviour).GetComponent<Stackable>();
        stackable.changeEvent += Stackable_changeEvent;
        stackElevation = 0; //initial elevation is on the ground
        ensureElevation(stackElevation);
        playersOnElevation[stackElevation].Add(playerID);
    }

    public Player(IDamageHolder holder) : this(nextOpenPlayerIndex, holder) { }

    public override void Destroy() {
        Assert.IsTrue(playerID >= 0);
        int playersListIndex = playerIndices[playerID];
        playersList.RemoveAt(playersListIndex);
        playerIndices.Remove(playerID);
        List<int> keys = new List<int>(playerIndices.Keys);
        foreach (int player in keys) {
            if (playerIndices[player] > playersListIndex) {
                playerIndices[player]--;
            }
        }

        nextOpenPlayerIndex = Mathf.Min(nextOpenPlayerIndex, playerID);

        this.playerID = -1;
    }

    static void ensureElevation(int elevation) {
        if(!playersOnElevation.ContainsKey(elevation)) {
            playersOnElevation[elevation] = new HashSet<int>();
        }
    }

    private void Stackable_changeEvent() {
        int newElevation = stackable.elevationInStack();
        if(newElevation != stackElevation) {
            //move our elevation
            ensureElevation(newElevation);
            playersOnElevation[stackElevation].Remove(playerID);
            playersOnElevation[newElevation].Add(playerID);

            stackElevation = newElevation;
        }
    }

    public static int randomPlayerID() {
        int index = Random.Range(0, playersList.Count);
        return playersList[index].playerID;
    }

    public static bool ContainsPlayerID(int playerID) {
        return playerIndices.ContainsKey(playerID);
    }

    public static Player GetPlayerByID(int playerID) {
        if (playerIndices.ContainsKey(playerID)) {
            return playersList[playerIndices[playerID]];
        } else {
            return null;
        }
    }

    public static bool AllPlayersDead() {
        foreach(Player player in playersList) {
            if (!player.dead) {
                return false;
            }
        }

        return true;
    }

    public static readonly Color[] playerColoring = { Color.red, Color.blue, Color.green, Color.yellow };
}
