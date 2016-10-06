using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Will be refactored.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class DamageOnHit : MonoBehaviour, ISpawnable {

    Rigidbody rigid;

    [SerializeField]
    protected float damage = 1;

    [SerializeField]
    protected float speed = 1;

    public void Awake() {
        rigid = GetComponent<Rigidbody>();
    }

    public void Spawn() {
        rigid.velocity = 5 * transform.forward;
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform.root.CompareTag(Tags.Boss)) {
            Damageable bossTarget = other.GetComponent<Damageable>();

            if (bossTarget != null) {
                //TODO: use player reference
                bossTarget.Damage(damage);
                SimplePool.Despawn(this.transform.root.gameObject);
            }
        }
    }
}
