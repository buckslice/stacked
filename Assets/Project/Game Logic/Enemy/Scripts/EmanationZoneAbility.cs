using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class EmanationZoneAbility : DurationAbilityAction {

    public Object zonePrefab;   // prob temp till more robust system is added for spawning zone effects
    public int zonesToSpawn = 5;   // spawns one on each player and some extra

    int spawnedZones = 0;
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
        float p = (float)spawnedZones / zonesToSpawn;
        if (lerpProgress > p) {
            Vector3 targetPos;
            //float x = Random.Range(-40.0f, 40.0f);
            //float z = Random.Range(-40.0f, 40.0f);
            float x = transform.position.x;
            float z = transform.position.z;
            targetPos = new Vector3(x, 0.0f, z);
            GameObject go = (GameObject)Instantiate(zonePrefab);
            Zone zone = go.GetComponent<Zone>();
            zone.discWidth = Random.Range(2.0f, 8.0f);
            zone.emanationSpeed = Random.Range(3.0f, 10.0f);
            zone.Setup(targetPos);

            spawnedZones++;
        }
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
