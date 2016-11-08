using UnityEngine;
using System.Collections;

public class HealingBeamAbility : DurationAbilityAction {

    public Collider col;
    public ParticleSystem beamParticles;
    public ParticleSystem sparkParticles;
    public AudioSource source;

    protected override void Start() {

    }

    protected override void OnDurationBegin() {
        col.enabled = true;
        beamParticles.Play();
        sparkParticles.Play();
        source.Play();
    }

    protected override void OnDurationEnd() {
        col.enabled = false;
        beamParticles.Stop();
        sparkParticles.Stop();
        source.Stop();
    }
}
