using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField]
    private float health = 100f;
    private float maxHealth;

    public HealthBar bar;

    void Awake() {
        maxHealth = health;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (bar) {
            bar.SetPercent(health / maxHealth);
        }

        //// this is temp!!!!! for testing...
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    health -= Random.Range(5.0f, 50.0f);
        //    if(health < 0.0f) {
        //        health = maxHealth;
        //    }
        //}
	}

    public void Damage(float amount)
    {
        health -= amount;
    }
}
