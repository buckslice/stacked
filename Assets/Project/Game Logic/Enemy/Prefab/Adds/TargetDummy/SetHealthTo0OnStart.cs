using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHealthTo0OnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Health health = GetComponentInParent<Health>();
        health.SetHealth(0, 100f);
	}
}
