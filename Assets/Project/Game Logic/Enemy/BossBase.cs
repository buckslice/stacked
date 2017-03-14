using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base class for bosses with some helper functions
public class BossBase : MonoBehaviour {

    protected List<Player> players = new List<Player>();  // filled and cleared by various functions

    // find all living players (thought theres helper function for this but couldnt find)
    public void FindAlivePlayers() {
        players.Clear();
        for (int i = 0; i < Player.Players.Count; ++i) {
            if (!Player.Players[i].dead) {
                players.Add(Player.Players[i]);
            }
        }
    }

    // gets random player from current player list
    public Player GetRandomPlayer() {
        Player p = players[Random.Range(0, players.Count)];
        PlayerRefs prefs = p.Holder.GetComponent<PlayerRefs>();
        return p;
    }

    // focus on a transforms position for an amount of time
    // turns at a rate degreesPerSec (not sure how well this works)
    public IEnumerator FocusRoutine(Transform targ, float time, float degreesPerSec = 360.0f) {
        Quaternion startRot = transform.rotation;
        float t = 0.0f;
        Vector3 dir;
        while (time > 0.0f) {
            dir = (targ.position - transform.position).normalized;
            float angle = Vector3.Angle(dir, transform.forward);
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0.0f, dir.z));
            transform.rotation = Quaternion.Slerp(startRot, lookRot, t);
            float r = (angle < 1.0f) ? degreesPerSec : degreesPerSec / angle;
            t += r * Time.deltaTime;
            time -= Time.deltaTime;
            yield return null;
        }
        dir = (targ.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0.0f, dir.z));
    }

    // look at a position at a rate of degreesPerSec
    // ends once you are looking at it
    public IEnumerator LookAtRoutine(Vector3 targ, float degreesPerSec = 360.0f) {
        Quaternion startRot = transform.rotation;
        Vector3 dir = (targ - transform.position).normalized;
        float angle = Vector3.Angle(dir, transform.forward);
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0.0f, dir.z));
        float t = 0.0f;
        while (t < 1.0f) {
            transform.rotation = Quaternion.Slerp(startRot, lookRot, t);
            float r = (angle < 1.0f) ? degreesPerSec : degreesPerSec / angle;
            t += r * Time.deltaTime;
            yield return null;
        }
        transform.rotation = lookRot;
    }

    // routine that throws away a player and restores movement once they hit ground
    public IEnumerator KnockAway(PlayerRefs pr, bool random, float power = 12.0f) {
        pr.pm.MovementInputEnabled.AddModifier(false);
        pr.pm.HaltMovement();

        // calculate vector knocking away from boss
        Vector3 knockDir = Vector3.zero;
        if (random) {
            Vector2 rand = Random.insideUnitCircle.normalized;
            knockDir = new Vector3(rand.x, 0.0f, rand.y);
        } else {
            knockDir = (pr.transform.position - transform.position).normalized;
        }

        pr.rb.velocity = (knockDir + Vector3.up).normalized * power;
        yield return Yielders.Get(0.1f);
        while (!pr.gc.isGrounded) {
            yield return null;
        }

        pr.pm.MovementInputEnabled.RemoveModifier(false);

    }

    // routine meant to force a player to stay at target position
    // until they are either picked up or coroutine is killed
    public IEnumerator TrapPlayer(Stackable stackable, Vector3 target) {
        while (stackable.elevationInStack() == 0) { // as soon as they are picked up this disables
            stackable.transform.position = Vector3.Lerp(stackable.transform.position, target, Time.deltaTime * 10.0f);
            yield return null;
        }
    }

    // locks player local position to certain offset
    // minor bug but for some reason players cant rotate while this is happening?
    public IEnumerator LockPlayerLocal(Transform player, Vector3 offset) {
        while (true) {
            player.localPosition = Vector3.Lerp(player.localPosition, offset, Time.deltaTime * 10.0f);
            yield return null;
        }
    }


}
