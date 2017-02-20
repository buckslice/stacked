using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceBoss : MonoBehaviour {

    public MandibleControl mandibles;
    public ParticleSystem burrowingParticles;
    public ParticleSystem iceBreathParticles;

    public Damageable damageable;

    CameraShakeScript camShaker;
    CameraController camController;

    NavMeshAgent agent;
    AudioSource source;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        camShaker = Camera.main.GetComponent<CameraShakeScript>();
        camController = Camera.main.transform.parent.GetComponent<CameraController>();
        //agent.SetDestination(Vector3.zero);

        agent.enabled = false;

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

        EntityUIGroupHolder egh = GetComponent<EntityUIGroupHolder>();
        egh.SetGroupActive(false);

        SetImmune(true);

        yield return new WaitForSeconds(2.0f);

        camController.SetTargetOverride(new Vector3(-10, 18, -6));
        source.Play();

        burrowingParticles.Play();
        float burrowTime = 5.0f;
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

        agent.enabled = true;
        burrowingParticles.Stop();

        camController.RemoveTargetOverride();
        egh.SetGroupActive(true);

        source.Stop();

        yield return new WaitForSeconds(2.0f);

        SetImmune(false);

        ChaseRandomPlayer();

    }


    void ChaseRandomPlayer() {
        int len = Player.Players.Count;
        Player p = Player.Players[Random.Range(0, len)];

        agent.SetDestination(p.Holder.transform.position);
    }


    // Update is called once per frame
    void Update() {



    }



}
