using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileMotion : ProjectileLifetimeAction {

    [SerializeField]
    protected float speed = 5;

    Rigidbody rigid;

    protected override void OnProjectileCreated() {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = speed * transform.forward;
    }

    protected override void OnProjectileDestroyed() { }
}
