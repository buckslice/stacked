﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceBoss : MonoBehaviour {

    public MandibleControl mandibles;
    public ParticleSystem burrowingParticles;
    public ParticleSystem mouthParticles;

    public Damageable damageable;

    const float iceCircleTime = 20.0f;     // time between phases
    const float iceCircleDuration = 10.0f; // how long phase lasts

    public GameObject iceCircleCenterPrefab;
    public GameObject iceShardPrefab;
    public GameObject iciclePrefab;

    public LayerMask playerLayer;

    CameraShakeScript camShaker;
    CameraController camController;
    EntityUIGroupHolder healthBar;
    NavMeshAgent agent;
    AudioSource source;
    AudioSource music;

    bool shouldIcicles = true; // every other time after ice circle. spawn some icicles

    float timeSinceLastIceCircle = 0.0f;
    float nextTargetTimer = 2.0f;

    State state = State.INTRO;

    Collider[] overLaps = new Collider[16];  // for overlaps sphere checks
    List<Player> players = new List<Player>();  // filled and cleared by various functions
    List<PlayerRefs> playerRefs = new List<PlayerRefs>();   // another helper list

    enum State {
        INTRO,
        IDLE,
        EATING,
        CIRCLING,
        SEARCHING,
        CHARGING,
    }

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        camShaker = Camera.main.GetComponent<CameraShakeScript>();
        camController = Camera.main.transform.parent.GetComponent<CameraController>();
        healthBar = GetComponent<EntityUIGroupHolder>(); ;

        agent.enabled = false;
        healthBar.SetGroupActive(false);

        GameObject musicGO = GameObject.Find("Music");
        if (musicGO) {
            music = musicGO.GetComponent<AudioSource>();
        }

        source = GetComponent<AudioSource>();

        StartCoroutine(IntroSequence());
        //StartCoroutine(ShortIntro());
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
        Player p = players[Random.Range(0, players.Count)];
        PlayerRefs prefs = p.Holder.GetComponent<PlayerRefs>();

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
                playerRefs.Clear();
                for (int i = 0; i < players.Count; ++i) {
                    PlayerRefs pr = players[i].Holder.GetComponent<PlayerRefs>();
                    pr.dmg.Damage(1000.0f);
                    playerRefs.Add(pr);
                }
                // start and wait for chomping
                yield return StartCoroutine(ChompRoutine());

                // throw all players who were chomped away randomly
                for (int i = 0; i < playerRefs.Count; ++i) {
                    Vector3 dir = Vector3.zero;
                    Vector3 ppos = playerRefs[i].transform.position;
                    // throw players close to center forward from boss
                    if ((ppos - ccenter.position).sqrMagnitude < 1.0f) {
                        dir = transform.forward + Vector3.up;
                    } else { // throw them away from center
                        dir = (ppos - ccenter.position).normalized + Vector3.up;
                    }
                    playerRefs[i].rb.velocity = dir.normalized * 12.0f;
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
        }
        shouldIcicles = !shouldIcicles;

        timeSinceLastIceCircle = 0.0f;
        state = State.IDLE;
    }

    // routine that throws away a player and restores movement once they hit ground
    IEnumerator KnockAway(PlayerRefs pr, bool random, float power = 12.0f) {
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
            if (prefs.holder != players[i].Holder) {    // skip player being eaten
                PlayerRefs pr = players[i].Holder.GetComponent<PlayerRefs>();
                if (pr.stck.elevationInStack() == 0) {  // only knock away bottom of stack
                    StartCoroutine(KnockAway(pr, false));
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

    // find all living players (thought theres helper function for this but couldnt find)
    void FindAlivePlayers() {
        players.Clear();
        for (int i = 0; i < Player.Players.Count; ++i) {
            if (!Player.Players[i].dead) {
                players.Add(Player.Players[i]);
            }
        }
    }

    // fills player list with people close enough to point
    // used in ice circle function
    void FindAlivePlayersNear(Vector3 point, float rad, float maxYDiff = 8.0f) {
        FindAlivePlayers();
        for (int i = 0; i < players.Count; ++i) {
            Vector3 playerPos = players[i].Holder.transform.position;
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
        Player p = players[Random.Range(0, players.Count)];
        yield return StartCoroutine(FocusRoutine(p.Holder.transform, Random.value + 1.0f));

        // high chance to go after another random player instead (TRICKY)
        if (players.Count > 1 && Random.value < 0.7f) {
            players.Remove(p);
            p = players[Random.Range(0, players.Count)];
        }

        // get direction to player (run through them a little)
        Vector3 pos = p.Holder.transform.position;
        Vector3 runThrough = (pos - transform.position).normalized * 10.0f;

        if (agent.enabled) {
            agent.SetDestination(p.Holder.transform.position + runThrough);
        }

        nextTargetTimer = 1.0f;

        state = State.CHARGING;
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

    // routine meant to force a player to stay at target position
    // until they are either picked up or coroutine is killed
    IEnumerator TrapPlayer(Stackable stackable, Vector3 target) {
        while (stackable.elevationInStack() == 0) { // as soon as they are picked up this disables
            stackable.transform.position = Vector3.Lerp(stackable.transform.position, target, Time.deltaTime * 10.0f);
            yield return null;
        }
    }

    // locks player local position to certain offset
    // minor bug but for some reason players cant rotate while this is happening?
    IEnumerator LockPlayerLocal(Transform player, Vector3 offset) {
        while (true) {
            player.localPosition = Vector3.Lerp(player.localPosition, offset, Time.deltaTime * 10.0f);
            yield return null;
        }
    }


}