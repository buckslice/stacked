using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class BossAggro : MonoBehaviour {

    List<float> aggroTable = new List<float>(); // a float for each player showing their current aggro to boss. The index is the player's playerID.
    int topAggroPlayer = -1;     // playerID of player who has most aggro. -1 for no player.
    const float aggroToSurpass = 1.0f; // amount of additional aggro needed to pull aggro

    NavMeshAgent agent;

    float timer = 0.0f; // tracking time since creation

    void Awake() {
        Player.playerListResized += Player_playerListResized;
    }

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();

        foreach (Health health in GetComponentsInChildren<Health>()) {
            health.onDamage += health_onDamage;
        }

        if (Player.AllPlayers.Count > 0) {
            Player_playerListResized();
        }

        topAggroPlayer = Random.Range(0, Player.AllPlayers.Count);
    }

    void OnDestroy() {
        Player.playerListResized -= Player_playerListResized;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        CheckAggro();

        ChaseAggroHolder();

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

    void ChaseAggroHolder() {
        //Vector3 targetPos;
        //if (aggroTable[topAggroPlayer] > 0) {
        //    targetPos = players[topAggroPlayer].transform.position;
        //} else {
        //    targetPos = gameObject.transform.position;
        //}
        //agent.destination = targetPos;
        if (topAggroPlayer >= 0) {
            agent.destination = Player.AllPlayers[topAggroPlayer].transform.position;
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

    //delegate called when we take damage, used to update aggro
    void health_onDamage(float amount, int playerID) {
        aggroTable[playerID] += amount;
    }
}
