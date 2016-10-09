using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class VoidZoneAbility : DurationAbilityAction {

    public Object zonePrefab;   // prob temp till more robust system is added for spawning zone effects

    public int zonesToSpawn = 5;
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
        float r = (float)spawnedZones / zonesToSpawn;
        if(lerpProgress > r) {
            //Debug.Log("SpawnZone! " + Time.time);

            float x = Random.Range(-40.0f, 40.0f);
            float z = Random.Range(-40.0f, 40.0f);
            Vector3 pos = new Vector3(x, 0.0f, z);
            x = Random.Range(5.0f, 20.0f);
            z = Random.Range(5.0f, 20.0f);
            Vector3 size = new Vector3(x, 1.0f, z);

            GameObject go = (GameObject)Instantiate(zonePrefab);
            go.GetComponent<Zone>().Setup(pos, size);

            spawnedZones++;
        }
    }

    protected override void OnDurationInterrupted() {
        base.OnDurationInterrupted();
        OnDurationEnd();
    }
}
