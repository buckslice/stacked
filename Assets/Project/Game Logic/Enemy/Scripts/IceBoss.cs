using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceBoss : MonoBehaviour {

    public MandibleControl mandibles;
    public ParticleSystem burrowingParticles;
    public ParticleSystem mouthParticles;

    public Damageable damageable;

    public float iceCircleTime = 20.0f;     // time between phases
    public float iceCircleDuration = 10.0f; // how long phase lasts

    CameraShakeScript camShaker;
    CameraController camController;
    EntityUIGroupHolder healthBar;

    NavMeshAgent agent;
    AudioSource source;
    AudioSource music;

    public Object iceCircleCenterPrefab;
    public Object iceShardPrefab;


    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        camShaker = Camera.main.GetComponent<CameraShakeScript>();
        camController = Camera.main.transform.parent.GetComponent<CameraController>();
        healthBar = GetComponent<EntityUIGroupHolder>(); ;
        //agent.SetDestination(Vector3.zero);

        agent.enabled = false;
        healthBar.SetGroupActive(false);

        GameObject musicGO = GameObject.Find("Music");
        if (musicGO) {
            music = musicGO.GetComponent<AudioSource>();
        }

        source = GetComponent<AudioSource>();

        StartCoroutine(IntroSequence());
    }

    void SetImmune(bool immune) {
        if (immune) {
            damageable.PhysicalVulnerabilityMultiplier.AddModifier(0);
            damageable.MagicalVulnerabilityMultiplier.AddModifier(0);
            healthBar.SetGroupActive(false);
        } else {
            damageable.PhysicalVulnerabilityMultiplier.RemoveModifier(0);
            damageable.MagicalVulnerabilityMultiplier.RemoveModifier(0);
            healthBar.SetGroupActive(true);
        }
    }

    IEnumerator IntroSequence() {

        SetImmune(true);

        yield return Yielders.Get(2.0f);

        camController.SetTargetOverride(new Vector3(-10, 18, -6));

        yield return StartCoroutine(BurrowRoutine(true, 4.0f));

        camController.RemoveTargetOverride();

        yield return Yielders.Get(2.0f);

        SetImmune(false);

        if (music) {
            music.Play();
        }

    }

    float timeSinceLastIceCircle = 0.0f;
    bool iceCircling = false;
    float nextTargetTimer = 2.0f;
    // Update is called once per frame
    void Update() {
        if (timeSinceLastIceCircle < iceCircleTime) {
            timeSinceLastIceCircle += Time.deltaTime;
        }
        if (timeSinceLastIceCircle >= iceCircleTime && !iceCircling && !eatingSomeOne) {
            iceCircling = true;
            StartCoroutine(IceCircleSequence());
        }

        if (!iceCircling && !eatingSomeOne && agent.isOnNavMesh) {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                nextTargetTimer -= Time.deltaTime;
                if (nextTargetTimer < 0.0f) {
                    StartCoroutine(FindNextTarget());
                    nextTargetTimer = 1000.0f;
                }
            } else {

            }
        }

    }

    bool eatingSomeOne = false;
    private void OnTriggerEnter(Collider other) {
        if (!eatingSomeOne && !iceCircling && other.CompareTag(Tags.Player)) {
            eatingSomeOne = true;   // todo: make it let you eat multiple ppl at once
            StartCoroutine(EatSequence(other.gameObject));
        }
    }

    IEnumerator BurrowRoutine(bool up, float burrowTime) {
        if (!up) {
            agent.enabled = false;
        }
        source.Play();
        burrowingParticles.Play();
        camShaker.screenShake(0.3f, burrowTime);
        float t = 0.0f;
        Vector3 start = transform.position;
        Vector3 end = start;
        end.y = up ? 0.0f : -10.0f;
        while (t < burrowTime) {
            transform.position = Vector3.Lerp(start, end, t / burrowTime);
            t += Time.deltaTime;
            yield return null;
        }
        burrowingParticles.Stop();
        source.Stop();
        if (up) {
            agent.enabled = true;
        }
    }

    List<Transform> iceShards = new List<Transform>();
    IEnumerator MoveUpIceShardsRoutine() {
        float t = 1.0f;
        while (t > 0 || iceShards.Count > 0) {   // once list is cleared this routine will break
            t -= Time.deltaTime;
            // move iceshards up every frame
            for (int i = 0; i < iceShards.Count; ++i) {
                Vector3 shardPos = iceShards[i].position;
                shardPos.y += Time.deltaTime * 2.0f;
                if (shardPos.y > 0.0f) {
                    shardPos.y = 0.0f;
                }
                iceShards[i].position = shardPos;
            }
            yield return null;
        }
    }

    IEnumerator MoveDownAndDestroyIceShards() {
        float t = 0.0f;
        while (t < 1.0f) {
            for (int i = 0; i < iceShards.Count; ++i) {
                Vector3 shardPos = iceShards[i].position;
                shardPos.y -= Time.deltaTime * 4.0f;
                iceShards[i].position = shardPos;
            }
            t += Time.deltaTime / 3.0f;
            yield return null;
        }
        for (int i = 0; i < iceShards.Count; ++i) {
            Destroy(iceShards[i].gameObject);
        }
        iceShards.Clear();
    }

    IEnumerator StackLerpRoutine(Stackable stackable, Vector3 lerpTo) {
        while (stackable && !stackable.Stacked) {
            stackable.transform.position = Vector3.Lerp(stackable.transform.position, lerpTo, Time.deltaTime * 10.0f);
            yield return null;
        }
    }

    IEnumerator IceCircleSequence() {
        agent.ResetPath();
        SetImmune(true);

        yield return StartCoroutine(BurrowRoutine(false, 3.0f));

        burrowingParticles.Play();
        mandibles.autoSound = false;

        FindAlivePlayers();
        Player p = alivePlayers[Random.Range(0, alivePlayers.Count)];

        PlayerMovement pm = p.Holder.GetComponentInParent<PlayerMovement>();
        pm.MovementInputEnabled.AddModifier(false);
        Rigidbody rb = pm.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Stackable stackable = pm.GetComponent<Stackable>();
        stackable.RemoveSelf();

        Vector2 randCirc = Random.insideUnitCircle.normalized * 15.0f;
        Vector3 centerSpawn = new Vector3(randCirc.x, 0.0f, randCirc.y);
        Coroutine stackLerp = StartCoroutine(StackLerpRoutine(stackable,centerSpawn));

        //pm.transform.position = new Vector3(randCirc.x, 0.0f, randCirc.y);

        Vector3 prevBossPosition = transform.position;
        Quaternion prevBossRotation = transform.rotation;   // maybe have boss point down to burrow?
        GameObject centerGO = (GameObject)Instantiate(iceCircleCenterPrefab, centerSpawn, Quaternion.identity);

        yield return Yielders.Get(1.0f);

        transform.SetParent(centerGO.transform);
        const float circleRadius = 10.0f;
        transform.localPosition = Vector3.right * circleRadius + Vector3.up * -4.0f;
        transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);

        const int numShards = 12;
        float nextSpawnTime = 0.0f;
        float t = 0.0f;
        StartCoroutine(MoveUpIceShardsRoutine());
        while (t < 1.0f) {
            t += Time.deltaTime / iceCircleDuration;
            if (t > nextSpawnTime) {
                nextSpawnTime += 1.0f / numShards;

                Vector3 spawn = transform.position;
                spawn.y = -10.0f;
                Quaternion rot = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + 90.0f, 0.0f);
                GameObject iceShard = (GameObject)Instantiate(iceShardPrefab, spawn, rot);
                iceShards.Add(iceShard.transform);
                mandibles.PlaySound();
                mandibles.Twitch(8.0f, 0.1f);
                mandibles.nextChange = 0.5f;
            }

            centerGO.transform.rotation = Quaternion.Euler(0.0f, t * (360.0f - 360.0f / numShards), 0.0f);
            yield return null;
        }

        // lerp towards center
        t = 0.0f;
        Vector3 start = transform.position;
        Quaternion startRot = transform.localRotation;
        while (t < 1.0f) {
            transform.position = Vector3.Lerp(start, centerGO.transform.position - centerGO.transform.forward * 4.0f, t);
            transform.localRotation = Quaternion.Slerp(startRot, Quaternion.identity, t);
            t += Time.deltaTime / 2.0f;

            yield return null;
        }

        // get all players inside circle and eat them
        FindAlivePlayers();
        Coroutine chompRoutine = null;
        List<Rigidbody> chomps = new List<Rigidbody>();
        for (int i = 0; i < alivePlayers.Count; ++i) {
            Transform pt = alivePlayers[i].Holder.transform; ;
            Vector3 ap = pt.position;
            ap.y = 0.0f;
            if (Vector3.Distance(transform.position, ap) < circleRadius + 1.0f) {
                Damageable dmgable = pt.GetComponentInChildren<Damageable>();
                dmgable.Damage(1000.0f);    // kill player
                if (chompRoutine == null) {
                    chompRoutine = StartCoroutine(ChompSequence(dmgable));
                }
                chomps.Add(pt.GetComponent<Rigidbody>());
            }
        }
        // wait for chomping to finish
        if (chompRoutine != null) {
            yield return chompRoutine;
        }
        for (int i = 0; i < chomps.Count; ++i) {
            Vector3 throwDir = (chomps[i].transform.position - transform.position).normalized;
            chomps[i].velocity = (throwDir + Vector3.up * 1.0f).normalized * 12.0f;  // throw corpse haha
        }

        StartCoroutine(MoveDownAndDestroyIceShards());

        transform.parent = null;
        Destroy(centerGO);
        //transform.position = prevBossPosition;
        //transform.rotation = prevBossRotation;

        burrowingParticles.Stop();

        StopCoroutine(stackLerp);

        // move somewhere random
        rb.isKinematic = false;
        pm.MovementInputEnabled.RemoveModifier(false);
        SetImmune(false);
        agent.enabled = true;
        mandibles.autoSound = true;
        timeSinceLastIceCircle = 0.0f;
        iceCircling = false;
    }

    IEnumerator EatSequence(GameObject colGO) { // given the players collider gameObject
        agent.ResetPath();   // stop traveling current path (CUZ ITS TIME TO FEAST EYEASSSS)

        PlayerMovement pm = colGO.GetComponentInParent<PlayerMovement>();
        pm.MovementInputEnabled.AddModifier(false);
        pm.transform.SetParent(mouthParticles.transform.parent);
        pm.transform.localPosition = Vector3.up * -0.5f;
        Rigidbody rb = pm.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        yield return StartCoroutine(ChompSequence(colGO.GetComponent<Damageable>()));

        Vector3 v = Random.insideUnitCircle.normalized;
        v = transform.position + new Vector3(v.x, 0.0f, v.y) * 5.0f;
        yield return StartCoroutine(LookAtRoutine(v));

        pm.transform.parent = null;
        // launch player away
        rb.isKinematic = false;
        rb.velocity = (transform.forward + Vector3.up).normalized * 12.0f;
        eatingSomeOne = false;

        yield return Yielders.Get(1.0f);
        mandibles.autoTwitch = true;

        yield return Yielders.Get(1.0f);
        pm.MovementInputEnabled.RemoveModifier(false);

    }

    IEnumerator ChompSequence(Damageable dmgable) {
        mandibles.autoTwitch = false;
        mandibles.autoSound = false;
        mouthParticles.Play();
        // BEGIN CHOMPING
        for (int i = 0; i < 10; ++i) {
            mandibles.Twitch(5.0f, 0.05f);      // close fast
            yield return Yielders.Get(0.05f);
            mandibles.PlaySound();
            mandibles.Twitch(30.0f, 0.2f);      // open slow
            yield return Yielders.Get(0.2f);
            dmgable.Damage(7.0f);
        }
        mandibles.autoSound = true;
        mouthParticles.Stop();
    }

    // find all living players (thought theres helper function for this but couldnt find)
    List<Player> alivePlayers = new List<Player>();
    void FindAlivePlayers() {
        alivePlayers.Clear();
        for (int i = 0; i < Player.Players.Count; ++i) {
            if (!Player.Players[i].dead) {
                alivePlayers.Add(Player.Players[i]);
            }
        }
    }

    IEnumerator FindNextTarget() {
        FindAlivePlayers();
        if (alivePlayers.Count == 0) {
            yield break;
        }

        // find and look at random player
        Player p = alivePlayers[Random.Range(0, alivePlayers.Count)];
        yield return StartCoroutine(FocusRoutine(p.Holder.transform, Random.value + 1.0f));

        // high chance to go after another random player instead (TRICKY)
        if (alivePlayers.Count > 1 && Random.value < 0.7f) {
            alivePlayers.Remove(p);
            p = alivePlayers[Random.Range(0, alivePlayers.Count)];
        }

        // get direction to player (run through them a little)
        Vector3 pos = p.Holder.transform.position;
        Vector3 runThrough = (pos - transform.position).normalized * 10.0f;

        if (agent.enabled) {
            agent.SetDestination(p.Holder.transform.position + runThrough);
        }

        nextTargetTimer = 1.0f;
    }


    // focus on a transforms position for an amount of time
    // turns at a rate degreesPerSec (not sure how well this works)
    IEnumerator FocusRoutine(Transform targ, float time, float degreesPerSec = 360.0f) {
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
    IEnumerator LookAtRoutine(Vector3 targ, float degreesPerSec = 360.0f) {
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


}
