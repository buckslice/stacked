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

    // Update is called once per frame
    void Update() {
        if (bar) {
            bar.SetPercent(health / maxHealth);
        }
    }

    public void Damage(float amount) {
        health -= amount;
        bar.SetPercent(health / maxHealth);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(health);
        } else {
            health = (float)stream.ReceiveNext();
            bar.SetPercent(health / maxHealth);
        }
    }
}
