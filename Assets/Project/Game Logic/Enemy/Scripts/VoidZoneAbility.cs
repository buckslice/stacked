using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class VoidZoneAbility : DurationAbilityAction {

    public Object zonePrefab;   // prob temp till more robust system is added for spawning zone effects

    public int extraZonesToSpawn = 5;   // spawns one on each player and some extra
    private int spawnedZones = 0;

    BossAggro boss;

    Transform bossArms;
    ParticleSystem[] armParticles = null;
    Coroutine armsAnimRoutine = null;

    protected override void Start() {
        base.Start();
        boss = GetComponentInParent<BossAggro>();
        Assert.IsNotNull(boss);
        bossArms = boss.transform.Find("Arms");
        if (bossArms) {
            armParticles = bossArms.GetComponentsInChildren<ParticleSystem>();
        }
    }

    protected override void OnDurationBegin() {
        boss.ShouldChase.AddModifier(false);
        spawnedZones = 0;

        if (armsAnimRoutine != null) {
            StopCoroutine(armsAnimRoutine);
        }
        //armsAnimRoutine = StartCoroutine(armAnim(true));
    }

    protected override void OnDurationEnd() {
        boss.ShouldChase.RemoveModifier(false);

        if (armsAnimRoutine != null) {
            StopCoroutine(armsAnimRoutine);
        }
        //armsAnimRoutine = StartCoroutine(armAnim(false));
    }

    protected override void OnDurationTick(float lerpProgress) {

        int numPlayers = Player.Players.Count;
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

    IEnumerator armAnim(bool raise) {
        if (bossArms == null || armParticles == null) {
            yield break;
        }

        // if hands going down then stop particles before arms move
        if (!raise) {
            for (int i = 0; i < armParticles.Length; ++i) {
                armParticles[i].Stop();
            }
        }

        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime;
            Vector3 rot = bossArms.rotation.eulerAngles;
            rot.x = Mathf.Lerp(raise ? 0.0f : -90.0f, raise ? -90.0f : 0.0f, t);
            bossArms.rotation = Quaternion.Euler(rot);
            yield return null;
        }

        // if hands going up then start particles after arms move
        if (raise) {
            for (int i = 0; i < armParticles.Length; ++i) {
                armParticles[i].Play();
            }
        }

    }
}
