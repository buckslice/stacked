using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class Boss : MonoBehaviour {

    List<float> aggroTable = new List<float>();
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
