using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceBoss : BossBase {

    public MandibleControl mandibles;
    public ParticleSystem burrowingParticles;
    public ParticleSystem mouthParticles;

    const float iceCircleTime = 15.0f;     // time between phases
    const float iceCircleDuration = 10.0f; // how long phase lasts

    public GameObject iceCircleCenterPrefab;
    public GameObject iceShardPrefab;
    public GameObject iciclePrefab;
    public GameObject bigIciclePrefab;

    public LayerMask playerLayer;

    CameraShakeScript camShaker;
    Health health;
    NavMeshAgent agent;
    AudioSource source;
    AudioSource music;

    bool shouldIcicles = true; // every other time after ice circle. spawn some icicles

    float timeSinceLastIceCircle = 0.0f;
    float nextTargetTimer = 2.0f;

    State state = State.INTRO;
    enum State {
        INTRO,
        IDLE,
        EATING,
        CIRCLING,
        SEARCHING,
        CHARGING,
    }

    // Use this for initialization
    protected override void Start() {
        base.Start();

        agent = GetComponent<NavMeshAgent>();
        camShaker = Camera.main.GetComponent<CameraShakeScript>();

        agent.enabled = false;

        health = GetComponent<Health>();

        GameObject musicGO = GameObject.Find("Music");
        if (musicGO) {
            music = musicGO.GetComponent<AudioSource>();
        }

        source = GetComponent<AudioSource>();

        SetImmune(true);

        StartCoroutine(IntroSequence());
        //StartCoroutine(ShortIntro());
    }


    IEnumerator IntroSequence() {

        yield return Yielders.Get(2.0f);

        camController.SetTargetOverride(new Vector3(-10, 18, -6));

        yield return StartCoroutine(BurrowRoutine(true, 3.0f)); // 4

        camController.RemoveTargetOverride();

        state = State.IDLE;

        SetImmune(false);

        if (music) {
            music.Play();
        }
    }

    IEnumerator ShortIntro() {
        SetImmune(true);
        yield return StartCoroutine(BurrowRoutine(true, 1.0f));
        state = State.IDLE;
        SetImmune(false);
        if (music) {
            music.Play();
        }
    }

    // Update is called once per frame
    void Update() {
        if (state == State.INTRO) {
            return;
        }

        if (timeSinceLastIceCircle < iceCircleTime) {
            timeSinceLastIceCircle += Time.deltaTime;
        }

        if (state == State.IDLE && timeSinceLastIceCircle >= iceCircleTime) {
            state = State.CIRCLING;
            StartCoroutine(IceCircleSequence());
        }

        if (state == State.CHARGING && agent.isOnNavMesh && agent.remainingDistance <= agent.stoppingDistance) {
            state = State.IDLE;
        }

        if (state == State.IDLE) {
            nextTargetTimer -= Time.deltaTime;
            if (nextTargetTimer < 0.0f) {
                state = State.SEARCHING;
                StartCoroutine(FindNextTarget());
                nextTargetTimer = 1000.0f;
            }
        }

    }

    private void OnTriggerEnter(Collider other) {
        if ((state == State.IDLE || state == State.CHARGING) && other.CompareTag(Tags.Player)) {
            state = State.EATING;
            StartCoroutine(EatSequence(other));
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
    IEnumerator MoveUpShardsRoutine() {
        float t = 1.0f;
        while (t > 0 || iceShards.Count > 0) {   // once list is cleared this routine will break
            t -= Time.deltaTime;
            // move iceshards up every frame
            for (int i = 0; i < iceShards.Count; ++i) {
                Vector3 shardPos = iceShards[i].position;
                shardPos.y += Time.deltaTime * 2.5f;
                if (shardPos.y > -1.0f) {
                    shardPos.y = -1.0f;
                }
                iceShards[i].position = shardPos;
            }
            yield return null;
        }
    }

    // moves down and destroys ice shards
    float[] randomShardSpeeds = new float[32];
    IEnumerator DestroyShardsRoutine() {
        for (int i = 0; i < iceShards.Count; ++i) {
            randomShardSpeeds[i] = Random.Range(4.0f, 10.0f);
        }

        float t = 0.0f;
        while (t < 3.0f) {
            for (int i = 0; i < iceShards.Count; ++i) {
                Vector3 shardPos = iceShards[i].position;
                shardPos.y -= Time.deltaTime * randomShardSpeeds[i];
                iceShards[i].position = shardPos;
            }
            t += Time.deltaTime;
            yield return null;
        }
        for (int i = 0; i < iceShards.Count; ++i) {
            Destroy(iceShards[i].gameObject);
        }
        iceShards.Clear();
    }

    IEnumerator IceCircleSequence() {
        agent.ResetPath();
        SetImmune(true);

        yield return StartCoroutine(BurrowRoutine(false, 3.0f));

        burrowingParticles.Play();
        mandibles.autoSound = false;

        FindAlivePlayers();
        PlayerRefs prefs = GetRandomPlayer();

        prefs.stck.RemoveSelf();
        prefs.pm.MovementInputEnabled.AddModifier(false);
        prefs.pm.HaltMovement();
        prefs.rb.isKinematic = true;

        Vector2 randCirc = Random.insideUnitCircle.normalized * 15.0f;
        Vector3 centerSpawn = new Vector3(randCirc.x, 0.0f, randCirc.y);
        Coroutine trapPlayerRoutine = StartCoroutine(TrapPlayer(prefs.stck, centerSpawn));

        Vector3 prevBossPosition = transform.position;
        Quaternion prevBossRotation = transform.rotation;   // maybe have boss point down to burrow?
        Transform ccenter = Instantiate(iceCircleCenterPrefab, centerSpawn, Quaternion.identity).transform;

        yield return Yielders.Get(1.0f);

        transform.SetParent(ccenter);
        const float circleRadius = 10.0f;
        transform.localPosition = Vector3.right * circleRadius + Vector3.up * -2.5f;    // -4.0 for old model
        transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);

        // raise up shards, surrounding the trapped player
        const int numShards = 12;
        float nextSpawnTime = 0.0f;
        float t = 0.0f;
        Coroutine moveUpShardsRoutine = StartCoroutine(MoveUpShardsRoutine());

        bool everyoneEscaped = false;
        while (t < 1.0f) {
            t += Time.deltaTime / iceCircleDuration;
            if (t > nextSpawnTime) {
                nextSpawnTime += 1.0f / numShards;

                Vector3 spawn = transform.position;
                spawn.y = -10.0f;
                // set ice shard to be 90 degrees from bosses current angle
                Vector3 eulers = new Vector3(0.0f, transform.rotation.eulerAngles.y + 90.0f, 0.0f);
                // add some randomness to each shard
                eulers += new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
                Quaternion rot = Quaternion.Euler(eulers);

                GameObject iceShard = Instantiate(iceShardPrefab, spawn, rot);
                // add some randomness to x and z scale as well
                Vector3 scale = iceShard.transform.localScale;
                scale.x += Random.value;
                scale.z -= Random.value;
                iceShard.transform.localScale = scale;
                iceShards.Add(iceShard.transform);

                mandibles.PlaySound();
                mandibles.Twitch(5.0f, 0.1f);
                mandibles.nextChange = 0.5f;
            }

            // check to see if there is at least one player still in range to be eaten
            // if not then quit this sequence early
            FindAlivePlayersNear(ccenter.position, circleRadius + 1.0f, 6.0f);
            if (players.Count == 0) {
                everyoneEscaped = true;
                StopCoroutine(moveUpShardsRoutine);
                StartCoroutine(DestroyShardsRoutine());
                break;
            }
            // rotate cicrle center (and thereby the boss)
            ccenter.rotation = Quaternion.Euler(0.0f, t * (360.0f - 360.0f / numShards), 0.0f);
            yield return null;
        }

        // lerp towards circle center
        t = 0.0f;
        Vector3 start = transform.position;
        Quaternion startRot = transform.localRotation;
        float lerpTime = 1.0f;
        if (!everyoneEscaped) {
            lerpTime = 2.0f;
            camShaker.screenShake(0.3f, lerpTime); // HERE COMES GRUBBY
        }
        while (t < 1.0f) {
            transform.position = Vector3.Lerp(start, ccenter.position - ccenter.forward * 4.0f, t);
            transform.localRotation = Quaternion.Slerp(startRot, Quaternion.identity, t);
            t += Time.deltaTime / lerpTime;

            yield return null;
        }

        if (!everyoneEscaped) {
            // find all players inside circle and eat them
            FindAlivePlayersNear(ccenter.position, circleRadius + 1.0f);
            if (players.Count > 0) {
                for (int i = 0; i < players.Count; ++i) {
                    players[i].dmg.Damage(1000.0f);
                }
                // start and wait for chomping
                yield return StartCoroutine(ChompRoutine());

                // throw all players who were chomped away randomly
                for (int i = 0; i < players.Count; ++i) {
                    Vector3 dir = Vector3.zero;
                    Vector3 ppos = players[i].transform.position;
                    // throw players close to center forward from boss
                    if ((ppos - ccenter.position).sqrMagnitude < 1.0f) {
                        dir = transform.forward + Vector3.up;
                    } else { // throw them away from center
                        dir = (ppos - ccenter.position).normalized + Vector3.up;
                    }
                    players[i].rb.velocity = dir.normalized * 12.0f;
                }
            }

            StopCoroutine(moveUpShardsRoutine);
            StartCoroutine(DestroyShardsRoutine());
        }

        transform.parent = null;
        Destroy(ccenter.gameObject);

        burrowingParticles.Stop();
        StopCoroutine(trapPlayerRoutine);

        if (prefs.stck.elevationInStack() == 0) {   // only set to not kinematic if not stacked
            prefs.rb.isKinematic = false;
        }
        prefs.pm.MovementInputEnabled.RemoveModifier(false);
        SetImmune(false);
        agent.enabled = true;
        mandibles.autoSound = true;

        // every other time spawn some icicles afterwards
        if (shouldIcicles) {
            for (int i = 0; i < 2; ++i) {
                Vector3 pos = Random.onUnitSphere * 30.0f;
                pos.y = 20.0f;
                Quaternion rot = Quaternion.Euler(0.0f, Random.Range(0, 90.0f), 0.0f);
                GameObject go = Instantiate(iciclePrefab, pos, rot);
                yield return Yielders.Get(1.0f);
            }
            //If below half health, summon a big icicle
            if (health.health / health.maxHealth < 0.5f) {
                Vector3 pos = Random.onUnitSphere * 30.0f;
                pos.y = 20.0f;
                Quaternion rot = Quaternion.Euler(0.0f, Random.Range(0, 90.0f), 0.0f);
                GameObject go = Instantiate(bigIciclePrefab, pos, rot);
            }
        }
        //Spawns icicles every ice circle when commented out. Let's see how this plays.
        //shouldIcicles = !shouldIcicles;

        timeSinceLastIceCircle = 0.0f;
        state = State.IDLE;
    }

    IEnumerator EatSequence(Collider eatenPlayerCol) { // given the players collider gameObject
        agent.ResetPath();   // stop traveling current path (CUZ ITS TIME TO FEAST EYEASSSS)

        PlayerRefs prefs = eatenPlayerCol.GetComponentInParent<PlayerRefs>();

        // disable and freeze player
        prefs.pm.MovementInputEnabled.AddModifier(false);
        prefs.pm.HaltMovement();

        prefs.transform.SetParent(mouthParticles.transform.parent, true);
        prefs.transform.localPosition = Vector3.up * -0.5f;
        prefs.stck.RemoveSelf();
        prefs.rb.isKinematic = true;

        // find other nearby players and knock them away
        FindAlivePlayersNear(transform.position, 8.0f);
        for (int i = 0; i < players.Count; ++i) {
            PlayerRefs pr = players[i];
            if (prefs != pr) {    // skip player being eaten
                if (pr.stck.elevationInStack() == 0) {  // only knock away bottom of stack
                    StartCoroutine(KnockAway(pr));
                }
            }
        }

        Coroutine lockPlayerRoutine = StartCoroutine(LockPlayerLocal(prefs.transform, Vector3.up * -0.5f));

        yield return StartCoroutine(ChompRoutine(eatenPlayerCol.GetComponent<Damageable>()));

        StopCoroutine(lockPlayerRoutine);

        // look at random position before throwing away eaten person
        Vector3 v = Random.insideUnitCircle.normalized;
        v = transform.position + new Vector3(v.x, 0.0f, v.y) * 5.0f;
        yield return StartCoroutine(LookAtRoutine(v));

        prefs.transform.parent = null;

        // launch player away
        prefs.rb.isKinematic = false;
        prefs.rb.velocity = (transform.forward + Vector3.up).normalized * 12.0f;
        state = State.IDLE;

        yield return Yielders.Get(1.0f);
        mandibles.autoTwitch = true;
        yield return Yielders.Get(1.0f);

        prefs.pm.MovementInputEnabled.RemoveModifier(false);

    }

    IEnumerator ChompRoutine(Damageable dmg = null) {
        mandibles.autoTwitch = false;
        mandibles.autoSound = false;
        mouthParticles.Play();
        // BEGIN CHOMPING
        for (int i = 0; i < 10; ++i) {
            mandibles.Twitch(0.0f, 0.05f);      // close fast
            yield return Yielders.Get(0.05f);
            mandibles.PlaySound();
            mandibles.Twitch(40.0f, 0.2f);      // open slow
            yield return Yielders.Get(0.2f);
            if (dmg) {  // ok to pass in null just for chomping effect
                dmg.Damage(7.0f);
                //dmg.Damage(11.0f);
            }
        }
        mandibles.autoSound = true;
        mouthParticles.Stop();
    }

    // fills player list with people close enough to point
    // used in ice circle function
    void FindAlivePlayersNear(Vector3 point, float rad, float maxYDiff = 8.0f) {
        FindAlivePlayers();
        for (int i = 0; i < players.Count; ++i) {
            Vector3 playerPos = players[i].transform.position;
            if (Mathf.Abs(point.y - playerPos.y) > maxYDiff) {
                players.RemoveAt(i--);
                continue;
            }
            point.y = playerPos.y = 0.0f;
            if ((point - playerPos).sqrMagnitude > rad * rad) {
                players.RemoveAt(i--);
            }
        }
    }

    IEnumerator FindNextTarget() {
        FindAlivePlayers();
        if (players.Count == 0) {
            yield break;
        }

        // find and look at random player
        PlayerRefs p = GetRandomPlayer();
        yield return StartCoroutine(FocusRoutine(p.transform, Random.value + 1.0f));

        // high chance to go after another random player instead (TRICKY)
        if (players.Count > 1 && Random.value < 0.7f) {
            players.Remove(p);
            p = GetRandomPlayer();
        }

        // get direction to player (run through them a little)
        Vector3 pos = p.transform.position;
        Vector3 runThrough = (pos - transform.position).normalized * 10.0f;

        if (agent.enabled) {
            agent.SetDestination(p.transform.position + runThrough);
        }

        nextTargetTimer = 1.0f;

        state = State.CHARGING;
    }

}
