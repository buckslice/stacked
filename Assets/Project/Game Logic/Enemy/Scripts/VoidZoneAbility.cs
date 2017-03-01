using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class VoidZoneAbility : DurationAbilityAction {

    public Object zonePrefab;   // prob temp till more robust system is added for spawning zone effects

    public int extraZonesToSpawn = 5;   // spawns one on each player and some extra
    private int spawnedZones = 0;

    BossAggro boss;

    protected override void Start() {
        base.Start();
        boss = GetComponentInParent<BossAggro>();
    }

    protected override void OnDurationBegin() {
        if (boss) {
            boss.ShouldChase.AddModifier(false);
        }
        spawnedZones = 0;
    }

    protected override void OnDurationEnd() {
        if (boss) {
            boss.ShouldChase.RemoveModifier(false);
        }
    }

    protected override void OnDurationTick(float lerpProgress) {

        int numPlayers = Player.Players.Count;
        int totalZones = numPlayers + extraZonesToSpawn;

        float p = (float)spawnedZones / totalZones;
        if (lerpProgress > p) {

            Vector3 targetPos;
            // spawn a zone on each alive player
            if (spawnedZones < numPlayers && !Player.Players[spawnedZones].dead) {
                targetPos = Player.Players[spawnedZones].Holder.transform.position;
                targetPos.y = 0.0f;
            } else {    // then spawn some extra random zones
                float x = Random.Range(-40.0f, 40.0f);
                float z = Random.Range(-40.0f, 40.0f);
                targetPos = new Vector3(x, 0.0f, z);
            }

            float r = Random.Range(5.0f, 20.0f);
            Vector3 size = new Vector3(r, 1.0f, r);

            GameObject go = (GameObject)Instantiate(zonePrefab);
            go.GetComponent<Zone>().Setup(transform.position + Vector3.up * 3.0f, size, targetPos);

            spawnedZones++;
        }
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }

}
