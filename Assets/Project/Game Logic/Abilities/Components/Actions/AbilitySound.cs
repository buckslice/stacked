using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AbilitySound : AbstractAbilityAction
{
    AudioSource target;
    void Awake()
    {
        target = GetComponent<AudioSource>();
    }

    public override bool Activate(PhotonStream stream) {
        target.Play();
        return true;
    }
}
