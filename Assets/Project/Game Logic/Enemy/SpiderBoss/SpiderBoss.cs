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

    const float trampleRadius = 5.0f;
    float lazerTimer = 15.0f;

    NavMeshAgent agent;

    float newWalk = 0.0f;
    float timeSinceLook = 0.0f;

    IKLimb[] legs;
    public ParticleSystem[] lazerParticles;
    public Gradient lazerGrad1;
    public Gradient lazerGrad2;
    public AudioClip spiderLaugh;
    public AudioClip shootLazer;

    CameraController cc;
    Coroutine focusRoutine = null;
    Transform model;

    RaycastHit[] hits = new RaycastHit[8];

    State state = State.INTRO;
    enum State {
        INTRO,
        IDLE,
        LOOKING,
        LAZERING,
        CHARGING,
    }


    // Use this for initialization
    protected override void Start() {
        base.Start();

        agent = GetComponent<NavMeshAgent>();

        legs = GetComponentsInChildren<IKLimb>();

        model = transform.Find("Model");

        //SetWalking(true);

        agent.enabled = false;

        cc = Camera.main.transform.parent.GetComponent<CameraController>();
        cc.boss = null;

        SetImmune(true);
        StartCoroutine(IntroSequence());
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
        yield return Yielders.Get(1.5f);
        yield return StartCoroutine(LookAtRoutine(transform.position - transform.forward, 720.0f));
        yield return Yielders.Get(0.5f);
        SetImmune(false);
        yield return Yielders.Get(0.5f);
        agent.enabled = true;
        state = State.IDLE;

        //StartCoroutine(ChangeHeight(8.0f, 1.0f));
    }

    // Update is called once per frame
    void Update() {
        UpdateLegs();

        TrampleNearbyPlayers();

        // if reached end of charge
        if (state == State.CHARGING && agent.isOnNavMesh && agent.remainingDistance <= agent.stoppingDistance) {
            if (focusRoutine != null) {
                StopCoroutine(focusRoutine);
            }
            timeSinceLook = 0.0f;
            newWalk = 0.0f;
            state = State.IDLE;
        }

        lazerTimer -= Time.deltaTime;

        if (state == State.IDLE) {
            if (lazerTimer < 0.0f) {
                state = State.LAZERING;
                StartCoroutine(LazerRoutine());
            } else {
                state = State.LOOKING;
                StartCoroutine(LookRoutine());
            }
        }

    }

    void ShootLazer(int index, Gradient grad) {
        source.clip = shootLazer;
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();

        ParticleSystem ps = lazerParticles[index];
        ParticleSystem.ColorOverLifetimeModule col = ps.colorOverLifetime;
        col.color = grad;
        ps.Stop();
        ps.Play();

        Transform t = ps.transform.parent;

        int count = Physics.SphereCastNonAlloc(t.position, 1.5f, t.forward, hits, 30.0f, playerLayer);
        for (int i = 0; i < count; ++i) {
            hits[i].collider.GetComponent<Damageable>().Damage(30.0f);
        }

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

        for (int i = 0; i < 8; ++i) {
            Vector3 dir = Random.onUnitSphere;
            dir.y = 0.0f;
            dir.Normalize();

            yield return StartCoroutine(LookAtRoutine(transform.position + dir * 5.0f, 180.0f));

            yield return Yielders.Get(0.25f);
            // outer eyes
            ShootLazer(0, lazerGrad1);
            ShootLazer(1, lazerGrad1);
            yield return Yielders.Get(0.25f);
            // inner eyes
            ShootLazer(2, lazerGrad1);
            ShootLazer(3, lazerGrad1);
            yield return Yielders.Get(0.25f);
        }

        // final spin
        for (int i = 0; i < 8; ++i) {
            Vector3 p = transform.position + transform.forward + transform.right;
            yield return StartCoroutine(LookAtRoutine(p, 360.0f));
            ShootLazer(1, lazerGrad2);
            yield return Yielders.Get(0.1f);
            ShootLazer(3, lazerGrad2);
            yield return Yielders.Get(0.1f);
            ShootLazer(2, lazerGrad2);
            yield return Yielders.Get(0.1f);
            ShootLazer(0, lazerGrad2);
            yield return Yielders.Get(0.1f);
        }

        yield return StartCoroutine(ChangeHeight(4.0f, 0.5f));

        state = State.IDLE;
        lazerTimer = Random.value * 5.0f + 15.0f;
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
                    pr.dmg.Damage(20.0f);
                }
            }
        }
    }

    IEnumerator LookRoutine() {
        FindAlivePlayers();
        if (players.Count == 0) {
            yield break;
        }

        PlayerRefs p = GetRandomPlayer();

        focusRoutine = StartCoroutine(FocusRoutine(p.transform, 30.0f, 60.0f));

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
