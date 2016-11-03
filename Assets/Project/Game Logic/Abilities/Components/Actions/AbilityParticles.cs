using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class AbilityParticles : AbstractAbilityAction {

    [SerializeField]
    ParticleSystem targetParticles;

    public override bool Activate(PhotonStream stream) {
        targetParticles.Play();
        return true;
    }
}
