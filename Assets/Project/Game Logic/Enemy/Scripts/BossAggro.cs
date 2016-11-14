using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class BossAggro : MonoBehaviour, IMovement, IRotation {
    [SerializeField]
    protected AllBoolStat shouldChase = new AllBoolStat(true);

    [SerializeField]
    protected AllBoolStat shouldMove = new AllBoolStat(true);

    public AllBoolStat ShouldChase { get { return shouldChase; } }
    public AllBoolStat ControlEnabled { get { return shouldChase; } }
    public AllBoolStat MovementInputEnabled { get { return shouldMove; } }
    public AllBoolStat RotationInputEnabled { get { return shouldChase; } }

    NavMeshAgent agent;
    Rigidbody rigid;
    Animator animator;

    /// <summary>
    /// A float for each player showing their current aggro to boss. The index is the player's playerID.
    /// </summary>
    Dictionary<int, float> aggroTable = new Dictionary<int, float>();

    /// <summary>
    /// PlayerID of player who is currently the bosses primary target
    /// </summary>
    int aggroHolder = -1;

    /// <summary>
    /// Amount of additional aggro needed to pull aggro from the aggroHolder
    /// </summary>
    float aggroToSurpass = 10.0f;

    /// <summary>
    /// Time this object was created, for tracking.
    /// </summary>
    //float createTime; // disabled until needed to avoid errors

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        //createTime = Time.time;

        foreach (Health health in GetComponentsInChildren<Health>()) {
            health.onDamage += health_onDamage;
        }

        if (Player.Players.Count > 0) {
            aggroHolder = Player.randomPlayerID();
        }


    }

    // Update is called once per frame
    void Update() {
        CheckAggro();

        if (shouldChase) {
            agent.enabled = true;
            agent.updateRotation = true;
            agent.updatePosition = shouldMove;

            if (aggroHolder >= 0) {
                Player target = Player.GetPlayerByID(aggroHolder);

                if (target == null) {
                    //if the player died
                    aggroTable.Remove(aggroHolder);
                    aggroHolder = GetPlayerWithMostAggro();
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
        if (animator != null) { //TODO: seperate script
            animator.SetBool(Tags.AnimatorParams.Controlled, shouldChase);
        }
    }

    // this function changes aggro if a player has surpassed current aggro holder by more than the threshold
    void CheckAggro() {
        int top = GetPlayerWithMostAggro();
        if (top == aggroHolder) {   // current aggro holder has most aggro so nothing to change here
            return;
        }

        if (Player.GetPlayerByID(aggroHolder).dead || GetAggro(top) > GetAggro(aggroHolder) + aggroToSurpass) {
            aggroHolder = top;  // a player has surpassed current aggro holder by the threshold, so pull aggro
        }
    }

    int GetPlayerWithMostAggro() {
        int id = -1;
        float aggro = -1.0f;
        foreach (Player p in Player.Players) {
            if (p.dead) { continue; }
            float a = GetAggro(p.PlayerID);
            if (a > aggro) {
                aggro = a;
                id = p.PlayerID;
            }
        }
        return id;
    }

    float GetAggro(int playerID) {
        if(playerID < 0) {
            return 0.0f;
        }
        float value;
        if (!aggroTable.TryGetValue(playerID, out value)) {
            //does not exist in data structure; initialize it.
            aggroTable[playerID] = value;
        }
        return value;
    }

    public void SetTaunt(Player taunter) {
        aggroHolder = taunter.PlayerID;
        aggroTable[aggroHolder] = GetAggro(GetPlayerWithMostAggro()) + aggroToSurpass * 2.0f;
    }

    /// <summary>
    /// delegate called when we take damage, used to update aggro table
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="playerID"></param>
    void health_onDamage(float amount, int playerID) {
        aggroTable[playerID] = GetAggro(playerID) + amount;
    }

    public void HaltMovement() {
        rigid.velocity = Vector3.zero; 
        if (agent.enabled) {
            agent.ResetPath();
        }
    }
    public void SetVelocity(Vector3 worldDirectionNormalized) { rigid.velocity = worldDirectionNormalized * agent.speed; }

    public Vector3 CurrentMovement() {
        return agent.desiredVelocity;
    }
}
