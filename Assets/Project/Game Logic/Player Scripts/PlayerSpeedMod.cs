using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to change players speed for certain time and play a particle system associated with it
public class PlayerSpeedMod : MonoBehaviour {

    public GameObject particles;    // these are optional

    PlayerRefs refs;
    float speedMod = 0.5f;
    float slowTime = -1.0f;
    float damageTime = 0.0f;

    // Use this for initialization
    void Start() {
        refs = GetComponent<PlayerRefs>();
        if (particles) {
            particles.SetActive(false);
        }
	}

    public void SetSlowed() {
        if(!particles.activeSelf) {
            refs.pm.Speed.AddModifier(speedMod);
            particles.SetActive(true);
            damageTime = 0.0f;
        }
        slowTime = 1.0f;
    }

	
	// Update is called once per frame
	void Update () {
        if (!particles.activeSelf) {
            return;
        }
        slowTime -= Time.deltaTime;
        if(slowTime <= 0.0f) {
            refs.pm.Speed.RemoveModifier(speedMod);
            particles.SetActive(false);
            damageTime = 0.0f;
        } else {
            damageTime -= Time.deltaTime;
            if(damageTime < 0.0f) {
                refs.dmg.Damage(2.0f);
                damageTime = 1.0f;
            }
        }
	}
}
