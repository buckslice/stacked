using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour {

    public ParticleSystem ps;

    float rateScale;

    ParticleSystem.EmissionModule psem;
    bool hitGround = false;
    bool dying = false;
    float vel = 0.0f;
    float pulseTime = 1.0f;

    List<PlayerSpeedMod> players = new List<PlayerSpeedMod>();
    CameraShakeScript camShaker;

    // Use this for initialization
    void Start() {
        psem = ps.emission;
        ParticleSystem.ShapeModule pssm = ps.shape;
        float r = pssm.radius;
        // save initial relation between emmision rate and area of emitter
        rateScale = psem.rateOverTime.constant / (Mathf.PI * r * r);

        for (int i = 0; i < Player.Players.Count; ++i) {
            players.Add(Player.Players[i].Holder.GetComponent<PlayerSpeedMod>());
        }
        camShaker = Camera.main.GetComponent<CameraShakeScript>();
    }

    void Update() {
        if (!hitGround) {
            vel += 15.0f * Time.deltaTime;
            Vector3 pos = transform.position - Vector3.up * vel * Time.deltaTime;
            if (pos.y < 0.0f) {
                pos.y = 0.0f;
                hitGround = true;
                ps.Play();
                camShaker.screenShake(0.75f, 0.5f);
            }
            transform.position = pos;
        } else if (dying) {
            Vector3 pos = transform.position - Vector3.up * 2.0f * Time.deltaTime;
            if (pos.y < -20.0f) {
                Destroy(gameObject);
            }
            transform.position = pos;
        } else {
            // slowly increase size of damage/slow zone
            Transform t = ps.transform.parent;
            Vector3 s = t.localScale;
            s += Vector3.one * Time.deltaTime * 0.5f;
            const float maxRad = 20.0f;
            if (s.x > maxRad) {
                s = Vector3.one * maxRad;
            }
            t.localScale = s;

            psem.rateOverTime = Mathf.PI * s.x * s.x * rateScale;

            // pulse out a slowing field that also does a little bit of damage
            pulseTime -= Time.deltaTime;
            if (pulseTime < 0.0f) {
                // check player list for all nearby
                for (int i = 0; i < players.Count; ++i) {
                    Vector3 p = players[i].transform.position;
                    p.y = 0.0f;
                    float r = s.x - 1.0f;
                    if ((transform.position - p).sqrMagnitude < r * r) {
                        players[i].SetSlowed();
                    }
                }

                pulseTime = 1.0f;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (!dying && other.CompareTag(Tags.Player)) {
            dying = true;
        }
    }
}
