using UnityEngine;
using System.Collections;

public class DamageCollider : MonoBehaviour {
    [SerializeField]
    protected int damage = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnCollisionEnter(Collision other) {
        Damageable damageable = other.collider.transform.root.GetComponentInChildren<Damageable>();
        if (damageable != null) {
            damageable.Damage(damage);
        }
    }
}
