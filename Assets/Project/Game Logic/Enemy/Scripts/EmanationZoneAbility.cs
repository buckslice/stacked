using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class EmanationZoneAbility : DurationAbilityAction {

    public Object zonePrefab;   // prob temp till more robust system is added for spawning zone effects

    public int extraZonesToSpawn = 5;   // spawns one on each player and some extra
    private int spawnedZones = 0;

    BossAggro boss;

    protected override void Start() {
        base.Start();
        boss = GetComponentInParent<BossAggro>();
        Assert.IsNotNull(boss);
    }

    protected override void OnDurationBegin() {
        boss.ShouldChase.AddModifier(false);
        spawnedZones = 0;
    }

    protected override void OnDurationEnd() {
        boss.ShouldChase.RemoveModifier(false);
    }

    protected override void OnDurationTick(float lerpProgress) {
        int numPlayers = Player.Players.Count;
        numPlayers = 0;
        int totalZones = numPlayers + extraZonesToSpawn;

        float p = (float)spawnedZones / totalZones;
        if (lerpProgress > p) {

            Vector3 targetPos;
            // first spawn a zone on each player
            if (spawnedZones < numPlayers) {
                targetPos = Player.Players[spawnedZones].Holder.transform.position;
                targetPos.y = 0.0f;
            } else {    // then spawn some extra random zones
                float x = Random.Range(-40.0f, 40.0f);
                float z = Random.Range(-40.0f, 40.0f);
                targetPos = new Vector3(x, 0.0f, z);
            }

            GameObject go = (GameObject)Instantiate(zonePrefab);
            go.GetComponent<Zone>().Setup(targetPos, Vector3.one);

            spawnedZones++;
        }
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
