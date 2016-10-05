using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class Boss : MonoBehaviour {

    List<GameObject> players;
    List<float> aggroTable; // a float for each player showing their current aggro to boss
    int topAggroPlayer;     // index of player who has most aggro
    const float aggroToSurpass = 1.0f; // amount of additional aggro needed to pull aggro

    NavMeshAgent agent;

    //temporary boss health
    public float health = 100;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();

        // find list of players (TODO REMOVE THIS AND USE A GLOBAL SYSTEM TO GET PLAYER LIST)
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.Player));
        int numPlayers = players.Count;
        Debug.Assert(numPlayers > 0);

        // give everyone zero aggro
        aggroTable = new List<float>(numPlayers);
        for (int i = 0; i < numPlayers; ++i) {
            aggroTable.Add(0.0f);
        }

        topAggroPlayer = Random.Range(0, numPlayers);    // give random player aggro
    }

    // Update is called once per frame
    void Update() {
        CheckAggro();

        ChaseAggroHolder();

        //temporary death system
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    // this function changes aggro if a player has surpassed current aggro holder by more than the threshold
    void CheckAggro() {
        float topAggro = aggroTable[topAggroPlayer];

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
        agent.destination = players[topAggroPlayer].transform.position;
    }

    public void SetTaunt(GameObject taunter) {
        topAggroPlayer = players.IndexOf(taunter);
        aggroTable[topAggroPlayer] = 100;
    }

    void OnCollisionEnter() {
        // update aggro table based on damage taken from each player
    }
}
