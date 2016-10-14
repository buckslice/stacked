using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class InvulnerableHealth : Health {

    public override float Damage(float amount) {
        //we take no damage!
        return 0;
    }
}
