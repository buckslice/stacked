using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderBoss : MonoBehaviour {

    public AnimationCurve legHeightCurve;
    public LayerMask legCollisionLayer;

    NavMeshAgent agent;

    float newWalk = 0.0f;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }	


	// Update is called once per frame
	void Update () {
        newWalk -= Time.deltaTime;

        //if(Vector3.SqrMagnitude(agent.destination - transform.position) < 1.0f) {
        //    newWalk = -1.0f;
        //}

        if(newWalk < 0.0f) {
            Vector2 r = Random.insideUnitCircle * 40.0f;
            agent.destination = new Vector3(r.x, 0.0f, r.y);

            newWalk = Random.value * 8.0f + 2.0f;
        }
	}
}
