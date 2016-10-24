using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    [SerializeField]
    protected float timeToLive = 1;

    float dieTime;

	// Use this for initialization
	void Start () {
        dieTime = Time.time + timeToLive;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > dieTime)
        {
            Destroy(transform.root.gameObject);
        }
	}
}
