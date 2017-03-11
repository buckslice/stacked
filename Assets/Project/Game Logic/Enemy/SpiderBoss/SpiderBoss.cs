using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderBoss : MonoBehaviour {

    public AnimationCurve legHeightCurve;
    public LayerMask legCollisionLayer;
    public float stepCooldown = 0.0f;

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
                Vector2 r = Random.insideUnitCircle * 40.0f;
                agent.destination = new Vector3(r.x, 0.0f, r.y);
                newWalk = Random.value * 8.0f + 2.0f;
            }
        }

	}

    IEnumerator LookRoutine() {
        BossHelper.FindAlivePlayers();
        Player p = BossHelper.GetRandomPlayer();
        yield return StartCoroutine(BossHelper.FocusRoutine(transform, p.Holder.transform, 5.0f));
        timeSinceLook = 0.0f;
        newWalk = 0.0f;
        state = State.RANDOM_WALK;
    }

    void CheckLegs() {
        stepCooldown -= Time.deltaTime;
        if (stepCooldown < 0.0f) {
            float maxDist = 1.0f;
            int maxIndex = -1;
            for (int i = 0; i < legs.Length; ++i) {
                if (legs[i].stepping) {
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
                    stepCooldown = 0.1f;
                }
            }
        }
    }

}
