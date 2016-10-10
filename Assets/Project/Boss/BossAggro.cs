using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    Dictionary<int, float> aggroTable = new Dictionary<int, float>();

    /// <summary>
    /// PlayerID of player who has most aggro. -1 for no player.
    /// </summary>
    int topAggroPlayer = -1;

    /// <summary>
    /// Time this object was created, for tracking.
    /// </summary>
    float createTime;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();

        createTime = Time.time;

        foreach (Health health in GetComponentsInChildren<Health>()) {
            health.onDamage += health_onDamage;
        }

        if(Player.Players.Count > 0)
        {
            topAggroPlayer = Enumerable.ToList(Player.Players.Keys)[Random.Range(0, Player.Players.Count)];
        }

        
    }

    // Update is called once per frame
    void Update() {

        CheckAggro();

        if (shouldChase) {
            if (topAggroPlayer >= 0) {
                agent.destination = Player.Players[topAggroPlayer].Holder.transform.position;
            }
        } else {
            agent.ResetPath();
        }
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
