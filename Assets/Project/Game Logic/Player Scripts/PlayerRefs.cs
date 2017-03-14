using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// helper class with reference to all useful player components
// used to avoid tons of get component calls
public class PlayerRefs : MonoBehaviour {

    public PlayerMovement pm;
    public Rigidbody rb;
    public Collider col;
    public Damageable dmg;
    public Stackable stck;
    public GroundedChecker gc;
    public DamageHolder holder;

}
