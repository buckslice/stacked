﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class BossAggro : MonoBehaviour, IMovement {

    /// <summary>
    /// Amount of additional aggro needed to pull aggro.
    /// </summary>
    [SerializeField]
    protected float aggroToSurpass = 1.0f;

    [SerializeField]
    protected AllBoolStat shouldChase = new AllBoolStat(true);
    public AllBoolStat ShouldChase { get { return shouldChase; } }
    public AllBoolStat ControlEnabled { get { return shouldChase; } }
    public AllBoolStat MovementInputEnabled { get { return shouldChase; } }

    NavMeshAgent agent;
    Rigidbody rigid;

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
    //float createTime; // disabled until needed to avoid errors

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();

        //createTime = Time.time;

        foreach (Health health in GetComponentsInChildren<Health>()) {
            health.onDamage += health_onDamage;
        }

        if (Player.Players.Count > 0) {
            topAggroPlayer = Player.randomPlayerID();
        }


    }

    // Update is called once per frame
    void Update() {

        CheckAggro();

        if (shouldChase) {
            agent.enabled = true;
            agent.updateRotation = true;
            agent.updatePosition = true;
            if (topAggroPlayer >= 0) {
                Player target = Player.GetPlayerByID(topAggroPlayer);

                if (target == null) {
                    //if the player died
                    aggroTable.Remove(topAggroPlayer);
                    topAggroPlayer = -1;
                    return;
                }

                agent.destination = target.Holder.transform.position;
            }
        } else {
            if (agent.enabled) {
                agent.ResetPath();
            }
            agent.enabled = false;
            agent.updateRotation = false;
            agent.updatePosition = false;
        }
    }

    // this function changes aggro if a player has surpassed current aggro holder by more than the threshold
    void CheckAggro() {
        float topAggro = topAggroPlayer >= 0 ? getAggro(topAggroPlayer) : 0;

        // find highest aggro in table
        KeyValuePair<int, float> maxAggro = new KeyValuePair<int, float>(-1, -1);
        foreach (KeyValuePair<int, float> aggroEntry in aggroTable) {
            if (aggroEntry.Value > maxAggro.Value) {
                maxAggro = aggroEntry;
            }
        }

        // pull aggro if surpass top players aggro
        if (maxAggro.Key != -1 && maxAggro.Key != topAggroPlayer && maxAggro.Value > topAggro + aggroToSurpass) {
            topAggroPlayer = maxAggro.Key;
        }
    }

    float getAggro(int playerID) {
        if (!aggroTable.ContainsKey(playerID)) {
            //does not exist in data structure; initialize it.
            float result = aggroToSurpass * Random.value;
            aggroTable[playerID] = result;
            return result;
        }

        //else it does exist
        return aggroTable[topAggroPlayer];
    }

    public void SetTaunt(Player taunter) {
        //reset and randomize all existing aggro
        List<int> playerIDs = new List<int>(aggroTable.Keys); //duplicate to not modify the collection we are iterating over
        foreach (int playerID in playerIDs) {
            aggroTable[playerID] = aggroToSurpass * Random.value;
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
        aggroTable[playerID] = getAggro(playerID) + amount;
    }

    public void haltMovement() {
        rigid.velocity = Vector3.zero; 
        if (agent.enabled) {
            agent.ResetPath();
        }
    }
    public void setVelocity(Vector3 worldDirectionNormalized) { rigid.velocity = worldDirectionNormalized * agent.speed; }

}
