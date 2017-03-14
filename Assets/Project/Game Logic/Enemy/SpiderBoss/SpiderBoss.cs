using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderBoss : BossBase {

    public AnimationCurve legHeightCurve;
    public LayerMask legCollisionLayer;
    public float stepCooldown = 0.0f;

    public float stepsPerSecond = 10.0f;

    NavMeshAgent agent;

    float newWalk = 0.0f;
    float timeSinceLook = 0.0f;

    IKLimb[] legs;


    State state = State.RANDOM_WALK;
    enum State {
        RANDOM_WALK,
        LOOKING,
    }


    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();

        legs = GetComponentsInChildren<IKLimb>();

        //SetWalking(true);
    }	

    public void SetWalking(bool walking) {
        if (walking) {
            stepsPerSecond = 5.0f;
            agent.speed = 4.0f;
            agent.angularSpeed = 90.0f;
        } else {    // running
            stepsPerSecond = 15.0f;
            agent.speed = 15.0f;
            agent.angularSpeed = 720.0f;
        }
    }


	// Update is called once per frame
	void Update () {
        CheckLegs();

        timeSinceLook += Time.deltaTime;
        if(state != State.LOOKING && timeSinceLook > 10.0f) {
            state = State.LOOKING;
            StartCoroutine(LookRoutine());
        }

        if(state == State.RANDOM_WALK) {
            newWalk -= Time.deltaTime;
            if (newWalk < 0.0f) {
                Vector2 r = Random.insideUnitCircle * 30.0f;
                agent.destination = new Vector3(r.x, 0.0f, r.y);
                newWalk = Random.value * 6.0f + 2.0f;
            }
        }

	}

    IEnumerator LookRoutine() {
        FindAlivePlayers();
        Player p = GetRandomPlayer();
        yield return StartCoroutine(FocusRoutine(p.Holder.transform, 5.0f));
        timeSinceLook = 0.0f;
        newWalk = 0.0f;
        state = State.RANDOM_WALK;
    }

    // updates legs and chooses which one to step next
    // todo: force alternating between sides each step
    // also could make sure you dont ever step legs of same index in a row
    void CheckLegs() {
        stepCooldown -= Time.deltaTime;
        if (stepCooldown < 0.0f) {
            float maxDist = 1.0f;
            int maxIndex = -1;
            for (int i = 0; i < legs.Length; ++i) {
                if (legs[i].stepping) { // skip legs that are currently stepping
                    continue;
                }
                // find leg with highest offset from its resting point
                float sqrDist = legs[i].GetSqrDistFromResting();
                if (sqrDist > maxDist) {
                    maxIndex = i;
                    maxDist = sqrDist;
                }
            }

            if (maxIndex != -1) {
                if (legs[maxIndex].TakeStep()) {
                    stepCooldown = 1.0f / stepsPerSecond;
                }
            }
        }
    }

}
