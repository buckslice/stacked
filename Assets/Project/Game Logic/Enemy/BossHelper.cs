using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// helper class for general boss functions
// TODO: port more code from iceboss and make iceboss use this too
public static class BossHelper {

    // focus on a transforms position for an amount of time
    // turns at a rate degreesPerSec (not sure how well this works)
    public static IEnumerator FocusRoutine(Transform boss, Transform targ, float time, float degreesPerSec = 360.0f) {
        Quaternion startRot = boss.rotation;
        float t = 0.0f;
        Vector3 dir;
        while (time > 0.0f) {
            dir = (targ.position - boss.position).normalized;
            float angle = Vector3.Angle(dir, boss.forward);
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0.0f, dir.z));
            boss.rotation = Quaternion.Slerp(startRot, lookRot, t);
            float r = (angle < 1.0f) ? degreesPerSec : degreesPerSec / angle;
            t += r * Time.deltaTime;
            time -= Time.deltaTime;
            yield return null;
        }
        dir = (targ.position - boss.position).normalized;
        boss.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0.0f, dir.z));
    }

    public static List<Player> players = new List<Player>();

    // gets random player from current player list
    public static Player GetRandomPlayer() {
        Player p = players[Random.Range(0, players.Count)];
        PlayerRefs prefs = p.Holder.GetComponent<PlayerRefs>();
        return p;
    }

    // find all living players (thought theres helper function for this but couldnt find)
    public static void FindAlivePlayers() {
        players.Clear();
        for (int i = 0; i < Player.Players.Count; ++i) {
            if (!Player.Players[i].dead) {
                players.Add(Player.Players[i]);
            }
        }
    }


}
