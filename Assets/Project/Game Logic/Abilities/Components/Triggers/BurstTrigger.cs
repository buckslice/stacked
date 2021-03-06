﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Because I don't want to script 25 activations of deathwave.
/// </summary>
public class BurstTrigger : MonoBehaviour, IUntargetedAbilityTrigger, IRemoteTrigger {

    [SerializeField]
    protected int activationsPerBurst = 3;

    [SerializeField]
    protected float timeBetweenActivations = 1;

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };

    Coroutine burstRoutine;

    public void Trigger() {
        if (burstRoutine != null) {
            StopCoroutine(burstRoutine);
        }
        burstRoutine = StartCoroutine(BurstRoutine());
    }

    public IEnumerator BurstRoutine() {

        float startTime = Time.time;
        for (int i = 1; i < activationsPerBurst; i++) {
            abilityTriggerEvent();

            while (Time.time < startTime + i * timeBetweenActivations) {
                yield return null;
            }
            abilityTriggerEvent();
        }

        burstRoutine = null;
    }
}
