﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderBoss : BossBase {

    public AnimationCurve legHeightCurve;
    public LayerMask legCollisionLayer;
    public float stepCooldown = 0.0f;

    public float stepsPerSecond = 10.0f;
    public LineRenderer webLine;

    const float trampleRadius = 4.0f;

    NavMeshAgent agent;

    float newWalk = 0.0f;
    float timeSinceLook = 0.0f;

    IKLimb[] legs;

    CameraController cc;
    Coroutine focusRoutine = null;

    State state = State.INTRO;
    enum State {
        INTRO,
        RANDOM_WALK,
        LOOKING,
        CHARGING,
    }


    // Use this for initialization
    protected override void Start() {
        base.Start();

        agent = GetComponent<NavMeshAgent>();

        legs = GetComponentsInChildren<IKLimb>();

        //SetWalking(true);

        agent.enabled = false;

        cc = Camera.main.transform.parent.GetComponent<CameraController>();
        cc.boss = null;

        SetImmune(true);
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence() {
        yield return Yielders.Get(2.0f);
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos;
        endPos.y = 0.0f;
        const float descentTime = 3.0f;
        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime * 1.0f / descentTime;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            if (t > 0.5f) {
                cc.boss = transform;
            }
            yield return null;
        }
        cc.boss = transform;
        yield return Yielders.Get(0.5f);
        webLine.gameObject.SetActive(false);
        yield return StartCoroutine(LookAtRoutine(transform.position - transform.forward, 80.0f));

        yield return Yielders.Get(1.5f);
        SetImmune(false);
        yield return Yielders.Get(0.5f);
        agent.enabled = true;
        state = State.RANDOM_WALK;
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
            state = State.RANDOM_WALK;
        }

        if (state == State.RANDOM_WALK) {
            timeSinceLook += Time.deltaTime;
            if (timeSinceLook > 10.0f) {
                state = State.LOOKING;
                StartCoroutine(LookRoutine());
            } else {
                newWalk -= Time.deltaTime;
                if (newWalk < 0.0f) {
                    Vector2 r = Random.insideUnitCircle * 30.0f;
                    agent.destination = new Vector3(r.x, 0.0f, r.y);
                    newWalk = Random.value * 6.0f + 2.0f;
                }
            }
        }

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
                if (pr.pm.MovementInputEnabled.Value) { // if not being knocked back already
                    StartCoroutine(KnockAway(pr));
                    pr.dmg.Damage(10.0f);
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

        // chance to run towards someone else
        if (players.Count > 1 && Random.value < 0.5f) {
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
