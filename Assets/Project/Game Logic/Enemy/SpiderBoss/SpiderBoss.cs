using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderBoss : BossBase {

    public AnimationCurve legHeightCurve;
    public LayerMask legCollisionLayer;
    public float stepCooldown = 0.0f;

    public float stepsPerSecond = 10.0f;
    public LineRenderer webLine;
    public Light spotLight;
    public ParticleSystem spinParticles;
    public ParticleSystem[] lazerParticles;
    public ParticleSystem[] eyeParticles;
    public Gradient lazerGrad1;
    public Gradient lazerGrad2;
    public Gradient normalEyes;
    public Gradient crazyEyes;
    public AudioClip spiderLaugh;
    public AudioClip shootLazer;

    float trampleRadius = 5.0f;
    float trampleDamage = 20.0f;
    int phase = 0;

    IKLimb[] legs;

    CameraController cc;
    Coroutine focusRoutine = null;
    Transform model;
    bool overridingLegs = true;

    RaycastHit[] hits = new RaycastHit[8];

    State state = State.INTRO;
    enum State {
        INTRO,
        INTERIM,    // phase between abilities
        LOOKING,
        LAZERING,
        SPINNING,
        AIRBORNE,
        CHARGING,   //  dont really need this anymore but leaving just incase
        PSYCHOTIC,
    }


    // Use this for initialization
    protected override void Start() {
        base.Start();

        legs = GetComponentsInChildren<IKLimb>();

        model = transform.Find("Model");

        //SetWalking(true);

        agent.enabled = false;

        cc = Camera.main.transform.parent.GetComponent<CameraController>();
        cc.boss = null;

        SetImmune(true);
        StartCoroutine(IntroSequence());
        //StartCoroutine(ShortIntro());
    }

    IEnumerator IntroSequence() {
        yield return Yielders.Get(1.0f);
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos;
        endPos.y = 0.0f;
        const float descentTime = 3.0f;
        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime * 1.0f / descentTime;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            if (t > 0.5f) {
                if (!source.isPlaying) {
                    source.clip = spiderLaugh;
                    source.Play();
                }
                cc.boss = transform;
            }
            yield return null;
        }
        cc.boss = transform;
        webLine.gameObject.SetActive(false);
        SetOverride(false);
        yield return Yielders.Get(1.5f);
        yield return StartCoroutine(LookAtRoutine(transform.position - transform.forward, 720.0f));
        yield return Yielders.Get(0.5f);
        SetImmune(false);
        if (music) {
            music.Play();
        }
        yield return Yielders.Get(0.5f);
        agent.enabled = true;
        state = State.INTERIM;
    }

    IEnumerator ShortIntro() {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos;
        endPos.y = 0.0f;
        const float descentTime = 0.5f;
        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime * 1.0f / descentTime;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            if (t > 0.5f) {
                if (!source.isPlaying) {
                    source.clip = spiderLaugh;
                    source.Play();
                }
                cc.boss = transform;
            }
            yield return null;
        }
        cc.boss = transform;
        webLine.gameObject.SetActive(false);
        SetOverride(false);
        yield return StartCoroutine(LookAtRoutine(transform.position - transform.forward, 720.0f));
        SetImmune(false);
        agent.enabled = true;
        state = State.INTERIM;
    }

    void SetOverride(bool b) {
        overridingLegs = b;
        if (!overridingLegs) {
            for (int i = 0; i < legs.Length; ++i) {
                legs[i].RemoveTargetOverride();
                legs[i].TakeStep();
            }
        }
    }

    // Update is called once per frame
    void Update() {

        if (!overridingLegs) {
            UpdateLegs();
        } else {
            // make legs stick straight out when overriding
            for (int i = 0; i < legs.Length; ++i) {
                legs[i].SetTargetOverride(model.position + legs[i].transform.parent.right * 20.0f);
            }
        }

        // trample players in all situations except when flying
        if (state != State.AIRBORNE) {
            TrampleNearbyPlayers();
        }

        if (state == State.INTERIM) {
            switch (phase) {
                case 0:
                    state = State.LOOKING;
                    StartCoroutine(LookAndChargeRoutine(Random.Range(8.0f, 12.0f)));
                    break;
                case 1:
                    state = State.LAZERING;
                    StartCoroutine(LazerRoutine());
                    break;
                case 2:
                    state = State.PSYCHOTIC;
                    StartCoroutine(PsychoticRunning(Random.Range(12.0f, 15.0f)));
                    break;
                case 3:
                    state = State.SPINNING;
                    StartCoroutine(SpinRoutine());
                    break;
                default:
                    Debug.LogWarning("Spiderboss in unknown phase: " + phase);
                    phase = 0;
                    break;
            }
            phase++;
            if(phase > 3) {
                phase = 0;
            }
        }

    }

    // given array of particles, change all their colors over time
    void ChangeParticlesColorOverTime(ParticleSystem[] p, Gradient grad) {
        for(int i = 0; i < p.Length; ++i) {
            ParticleSystem.ColorOverLifetimeModule col = p[i].colorOverLifetime;
            col.color = grad;
        }
    }

    void ShootLazer(int index) {
        source.clip = shootLazer;
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();

        ParticleSystem ps = lazerParticles[index];
        ps.Stop();
        ps.Play();

        Transform t = ps.transform.parent;

        int count = Physics.SphereCastNonAlloc(t.position, 1.5f, t.forward, hits, 30.0f, playerLayer);
        for (int i = 0; i < count; ++i) {
            hits[i].collider.GetComponent<Damageable>().Damage(30.0f);
        }

    }

    IEnumerator LazerRoutine() {
        // walk to center of map
        agent.destination = Vector3.zero;
        yield return Yielders.Get(0.5f);
        // wait until spider is at center
        while (!agent.isOnNavMesh || agent.remainingDistance > agent.stoppingDistance) {
            yield return null;
        }
        // crouch down
        yield return StartCoroutine(ChangeHeight(1.5f, 0.5f));

        ChangeParticlesColorOverTime(lazerParticles, lazerGrad1);

        for (int i = 0; i < 8; ++i) {
            Vector3 dir = Random.onUnitSphere;
            dir.y = 0.0f;
            dir.Normalize();

            yield return StartCoroutine(LookAtRoutine(transform.position + dir * 5.0f, 180.0f));

            yield return Yielders.Get(0.25f);

            // outer eyes
            ShootLazer(0);
            ShootLazer(1);
            yield return Yielders.Get(0.25f);
            // inner eyes
            ShootLazer(2);
            ShootLazer(3);
            yield return Yielders.Get(0.25f);
        }

        ChangeParticlesColorOverTime(eyeParticles, lazerGrad2);
        yield return Yielders.Get(1.0f);
        ChangeParticlesColorOverTime(lazerParticles, lazerGrad2);

        // final spin
        for (int i = 0; i < 8; ++i) {
            Vector3 p = transform.position + transform.forward + transform.right;
            yield return StartCoroutine(LookAtRoutine(p, 360.0f));
            ShootLazer(1);
            yield return Yielders.Get(0.1f);
            ShootLazer(3);
            yield return Yielders.Get(0.1f);
            ShootLazer(2);
            yield return Yielders.Get(0.1f);
            ShootLazer(0);
            yield return Yielders.Get(0.1f);
        }

        yield return StartCoroutine(ChangeHeight(4.0f, 0.5f));
        ChangeParticlesColorOverTime(eyeParticles, normalEyes);
        yield return Yielders.Get(1.0f);    // short pause at end of phase
        state = State.INTERIM;
    }


    IEnumerator SpinRoutine() {
        webLine.gameObject.SetActive(true);
        SetOverride(true);
        cc.boss = null; // make camera stop following boss
        state = State.AIRBORNE;
        spotLight.gameObject.SetActive(true);
        yield return StartCoroutine(ChangeHeight(30.0f, 2.0f));
        webLine.gameObject.SetActive(false);
        FindAlivePlayers();
        if (players.Count == 0) {
            yield break;
        }
        // follow random player while in air
        PlayerRefs p = GetRandomPlayer();
        agent.destination = p.transform.position;
        agent.autoBraking = false;
        yield return Yielders.Get(0.5f);
        float t = 0.0f;
        while (t < 5.0f) {
            t += Time.deltaTime;
            if (agent.remainingDistance < agent.stoppingDistance) {
                break;
            }
            agent.destination = p.transform.position;
            yield return null;
        }
        agent.autoBraking = true;
        agent.ResetPath();
        Color c = spotLight.color;  //save current color
        spotLight.color = new Color(1.0f, 0.4f, 0.0f);
        spotLight.range = 160.0f;
        yield return Yielders.Get(1.0f);    // wait for a sec before dropping
        yield return StartCoroutine(ChangeHeight(4.0f, 1.0f));  // drop down on player
        camShaker.screenShake(1.0f, 1.0f); // slam dunk
        cc.boss = transform;
        spotLight.gameObject.SetActive(false);
        spotLight.color = c;    // restore original color
        spotLight.range = 80.0f;
        trampleDamage = 50.0f;
        // start spinning, following the same player still
        state = State.SPINNING;
        float initSpeed = agent.speed;
        float initTrample = trampleRadius;
        agent.speed = 2.0f;
        float rotSpeed = 100.0f;
        trampleRadius = 12.0f;
        t = 0.0f;
        agent.angularSpeed = 0.0f;
        spinParticles.Play();
        bool switched = false;
        while (t < 10.0f) {
            if (t > 0.5f) {  // set trample damage back to normal value
                trampleDamage = 20.0f;
            }
            
            if(players.Count > 1) { // if can switch targets
                if (p.player.dead) {    // if player died then switch
                    players.Remove(p);
                    p = GetRandomPlayer();
                }

                if (!switched && t > 5.0f) {    // halfway thru switch too
                    players.Remove(p);
                    p = GetRandomPlayer();
                    switched = true;
                }
            }
            // slowly increase speed and spin rate
            t += Time.deltaTime;
            agent.speed += Time.deltaTime;
            agent.destination = p.transform.position;
            rotSpeed += Time.deltaTime * 30.0f;
            transform.Rotate(0.0f, rotSpeed * Time.deltaTime, 0.0f);
            yield return null;
        }
        agent.angularSpeed = 720.0f;
        agent.ResetPath();
        spinParticles.Stop();

        agent.speed = initSpeed;
        trampleRadius = initTrample;

        SetOverride(false);
        yield return Yielders.Get(1.0f);    // short pause at end of phase
        state = State.INTERIM;
    }

    // lerps model to target height over time
    IEnumerator ChangeHeight(float height, float time) {
        Vector3 start = model.localPosition;
        Vector3 end = new Vector3(0.0f, height, 0.0f);
        float t = 0.0f;
        while (t < time) {
            model.localPosition = Vector3.Lerp(start, end, t / time);
            t += Time.deltaTime;
            yield return null;
        }
        model.localPosition = end;
    }

    // check if near enough to any player and if so knockback and damage
    void TrampleNearbyPlayers() {
        FindAlivePlayers();
        Vector3 me = transform.position;
        me.y = 0.0f;
        for (int i = 0; i < players.Count; ++i) {
            PlayerRefs pr = players[i];
            Vector3 p = pr.transform.position;
            p.y = 0.0f;
            // sqr distance check is cheaper and same result
            if ((p - me).sqrMagnitude < trampleRadius * trampleRadius) {
                if (!pr.knocked) { // if not being knocked back already
                    pr.knocked = true;
                    StartCoroutine(KnockAway(pr));
                    pr.dmg.Damage(trampleDamage);
                }
            }
        }
    }

    IEnumerator LookAndChargeRoutine(float duration) {
        float timeAtStart = Time.time;

        while (Time.time - duration < timeAtStart) {

            FindAlivePlayers();
            if (players.Count == 0) {
                yield break;
            }

            PlayerRefs p = GetRandomPlayer();

            focusRoutine = StartCoroutine(FocusRoutine(p.transform, 30.0f, 180.0f));

            yield return Yielders.Get(1.0f);    // wait for a bit then either charge this player or another

            // chance to run towards someone else (while still focusing first player
            if (players.Count > 1 && Random.value > 0.8f) {
                players.Remove(p);
                p = GetRandomPlayer();
            }

            // get direction to player (run through them a little)
            Vector3 pos = p.transform.position;
            Vector3 runThrough = (pos - transform.position).normalized * 10.0f;

            if (agent.enabled) {
                agent.SetDestination(p.transform.position + runThrough);
            }

            state = State.CHARGING;
            yield return Yielders.Get(0.5f);    // give agent time to calculate destination
            // wait until get to destination
            while (!agent.isOnNavMesh || agent.remainingDistance > agent.stoppingDistance) {
                yield return null;
            }

            // if reached end of charge then cancel focus and repeat
            if (focusRoutine != null) {
                StopCoroutine(focusRoutine);
            }
        }

        state = State.INTERIM;
    }

    IEnumerator PsychoticRunning(float duration) {
        //todo run to bugs instead
        ChangeStepHeight(false);
        ChangeParticlesColorOverTime(eyeParticles, crazyEyes);
        StartCoroutine(ChangeHeight(8.0f, 0.5f));
        float origSpeed = agent.speed;
        agent.speed = 25.0f;
        float t = 0.0f;
        float newTime = 0.0f;
        while (t < duration) {
            newTime -= Time.deltaTime;
            if (newTime < 0.0f) {
                Vector3 d = Random.insideUnitSphere;
                d.y = 0.0f;
                d.Normalize();
                agent.destination = d * 30.0f;
                newTime = Random.Range(4.0f, 6.0f);
            }
            t += Time.deltaTime;
            yield return null;
        }
        agent.ResetPath();
        agent.speed = origSpeed;
        ChangeParticlesColorOverTime(eyeParticles, normalEyes);
        StartCoroutine(ChangeHeight(4.0f, 0.5f));
        ChangeStepHeight(true);
        yield return Yielders.Get(2.0f);    // short pause at end of phase
        state = State.INTERIM;
    }

    // updates legs and chooses which one to step next
    // todo: force alternating between sides each step
    // also could make sure you dont ever step legs of same index in a row
    void UpdateLegs() {
        stepCooldown -= Time.deltaTime;
        if (stepCooldown < 0.0f) {
            float maxDist = 1.0f;
            int maxIndex = -1;
            for (int i = 0; i < legs.Length; ++i) {
                if (legs[i].stepping) { // skip legs that are currently stepping
                    continue;
                }
                // find leg with highest offset from its resting point
                float sqrDist = legs[i].GetSqrDistFromResting();
                if (sqrDist > maxDist) {
                    maxIndex = i;
                    maxDist = sqrDist;
                }
            }

            if (maxIndex != -1) {
                if (legs[maxIndex].TakeStep()) {
                    stepCooldown = 1.0f / stepsPerSecond;
                }
            }
        }
    }

    void StepAllLegs() {
        for (int i = 0; i < legs.Length; ++i) {
            legs[i].TakeStep();
        }
    }

    void ChangeStepHeight(bool normal) {
        for (int i = 0; i < legs.Length; ++i) {
            legs[i].normalStepHeight = normal;
        }
    }

    public void SetWalking(bool walking) {
        if (walking) {
            stepsPerSecond = 5.0f;
            agent.speed = 4.0f;
            agent.angularSpeed = 90.0f;
        } else {    // running
            stepsPerSecond = 15.0f;
            agent.speed = 15.0f;
            agent.angularSpeed = 720.0f;
        }
    }

}
