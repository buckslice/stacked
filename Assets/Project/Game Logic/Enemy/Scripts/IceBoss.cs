using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceBoss : MonoBehaviour {

    public MandibleControl mandibles;
    public ParticleSystem burrowingParticles;
    public ParticleSystem mouthParticles;

    public Damageable damageable;

    CameraShakeScript camShaker;
    CameraController camController;

    NavMeshAgent agent;
    AudioSource source;
    AudioSource music;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        camShaker = Camera.main.GetComponent<CameraShakeScript>();
        camController = Camera.main.transform.parent.GetComponent<CameraController>();
        //agent.SetDestination(Vector3.zero);

        agent.enabled = false;

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
        } else {
            damageable.PhysicalVulnerabilityMultiplier.RemoveModifier(0);
            damageable.MagicalVulnerabilityMultiplier.RemoveModifier(0);
        }
    }

    IEnumerator IntroSequence() {

        EntityUIGroupHolder healthBar = GetComponent<EntityUIGroupHolder>();
        healthBar.SetGroupActive(false);

        SetImmune(true);

        yield return Yielders.Get(2.0f);

        camController.SetTargetOverride(new Vector3(-10, 18, -6));
        source.Play();

        burrowingParticles.Play();
        float burrowTime = 4.0f;
        camShaker.screenShake(0.3f, burrowTime);

        float t = 0.0f;
        Vector3 start = transform.position;
        Vector3 end = start;
        end.y = 0.0f;
        while (t < burrowTime) {
            transform.position = Vector3.Lerp(start, end, t / burrowTime);
            t += Time.deltaTime;
            yield return null;
        }

        burrowingParticles.Stop();

        camController.RemoveTargetOverride();
        healthBar.SetGroupActive(true);

        source.Stop();

        yield return Yielders.Get(2.0f);
        agent.enabled = true;
        if (music) {
            music.Play();
        }

        SetImmune(false);

    }


    float nextTargetTimer = 0.0f;
    // Update is called once per frame
    void Update() {
        if (eatingSomeOne) {
            // leaving this structure here for now
        } else {
            if (agent.isOnNavMesh) {
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
    }

    bool eatingSomeOne = false;
    private void OnTriggerEnter(Collider other) {
        if (!eatingSomeOne && other.CompareTag(Tags.Player)) {
            eatingSomeOne = true;   // todo: make it let you eat multiple ppl at once
            StartCoroutine(EatSequence(other.gameObject));
        }
    }

    IEnumerator EatSequence(GameObject playerGO) {
        agent.ResetPath();   // stop traveling current path (CUZ ITS TIME TO FEAST EYEASSSS)


        PlayerMovement pm = playerGO.GetComponentInParent<PlayerMovement>();
        pm.MovementInputEnabled.AddModifier(false);
        pm.transform.SetParent(mouthParticles.transform.parent);
        pm.transform.localPosition = Vector3.up * -0.5f;
        Rigidbody rb = pm.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        Damageable dam = playerGO.GetComponent<Damageable>();

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
            dam.Damage(7.0f);
        }
        mandibles.autoSound = true;
        mouthParticles.Stop();

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

    List<Player> players = new List<Player>();
    IEnumerator FindNextTarget() {
        players.Clear();
        // find all living players (thought theres helper function for this but couldnt find)
        for (int i = 0; i < Player.Players.Count; ++i) {
            if (!Player.Players[i].dead) {
                players.Add(Player.Players[i]);
            }
        }
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

        agent.SetDestination(p.Holder.transform.position + runThrough);

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
