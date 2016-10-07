using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class BossAggro : MonoBehaviour {

    /// <summary>
    /// Amount of additional aggro needed to pull aggro.
    /// </summary>
    [SerializeField]
    protected float aggroToSurpass = 1.0f;

    [SerializeField]
    protected AllBoolStat shouldChase = new AllBoolStat(true);
    public AllBoolStat ShouldChase { get { return shouldChase; } }

    NavMeshAgent agent;

    /// <summary>
    /// A float for each player showing their current aggro to boss. The index is the player's playerID.
    /// </summary>
    List<float> aggroTable = new List<float>();

    /// <summary>
    /// PlayerID of player who has most aggro. -1 for no player.
    /// </summary>
    int topAggroPlayer = -1;

    /// <summary>
    /// Time this object was created, for tracking.
    /// </summary>
    float createTime;

    void Awake() {
        Player.playerListResized += Player_playerListResized;
    }

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();

        createTime = Time.time;

        foreach (Health health in GetComponentsInChildren<Health>()) {
            health.onDamage += health_onDamage;
        }

        //ensure aggroTable matches playerList
        if (Player.AllPlayers.Count > 0) {
            Player_playerListResized();
        }

        do {
            topAggroPlayer = Random.Range(0, Player.AllPlayers.Count);
        } while (topAggroPlayer < 0 || Player.AllPlayers[topAggroPlayer] == null); //TODO: refactor to be a linear search for all non-null elements
    }

    void OnDestroy() {
        Player.playerListResized -= Player_playerListResized;
    }

    // Update is called once per frame
    void Update() {

        CheckAggro();

        if (shouldChase) {
            if (topAggroPlayer >= 0) {
                agent.destination = Player.AllPlayers[topAggroPlayer].transform.position;
            }
        } else {
            agent.ResetPath();
        }

    }

    /// <summary>
    /// Delegate to increase the size of our aggro table when the player table expands
    /// </summary>
    void Player_playerListResized() {
        while (aggroTable.Count < Player.AllPlayers.Count) {
            if (Player.AllPlayers[aggroTable.Count] == null) {
                aggroTable.Add(0);
            } else {
                aggroTable.Add(aggroToSurpass * Random.value);
            }
        }

        CheckAggro();
    }

    // this function changes aggro if a player has surpassed current aggro holder by more than the threshold
    void CheckAggro() {
        float topAggro = topAggroPlayer >= 0 ? aggroTable[topAggroPlayer] : 0;

        // find highest aggro in table
        int maxIndex = -1;
        float maxAggro = -1;
        for (int i = 0; i < aggroTable.Count; ++i) {
            if (aggroTable[i] > maxAggro) {
                maxIndex = i;
                maxAggro = aggroTable[i];
            }
        }

        // pull aggro if surpass top players aggro
        if (maxIndex != -1 && maxIndex != topAggroPlayer && maxAggro > topAggro + aggroToSurpass) {
            topAggroPlayer = maxIndex;
        }
    }

    public void SetTaunt(Player taunter) {
        //reset and randomize all existing aggro
        for (int i = 0; i < aggroTable.Count; i++) {
            aggroTable[i] = aggroToSurpass * Random.value;
        }

        topAggroPlayer = taunter.PlayerID;
        aggroTable[topAggroPlayer] = 100;
    }

    /// <summary>
    /// delegate called when we take damage, used to update aggro table
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="playerID"></param>
    void health_onDamage(float amount, int playerID) {
        aggroTable[playerID] += amount;
    }
}
