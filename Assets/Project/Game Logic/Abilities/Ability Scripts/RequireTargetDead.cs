using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RequireTargetDead : TargetedAbilityConstraint {

    public override void Activate(GameObject context) { }

    public override bool isAbilityActivatible(GameObject target) {
        if (target.GetComponent<EnemyDeadState>()!= null) {
            return true;
        }
        if (!enabled) { return true; }
        IDamageHolder holder = target.GetComponentInParent<IDamageHolder>();
        if (holder == null) {
            return false;
        }
        Player player = holder.GetRootDamageTracker() as Player;
        if (player == null) {
            return false;
        }
        return player.dead;
    }
}
