﻿using UnityEngine;
using System.Collections;

// beginning of player AI (mainly to be use for camera testing but could be developed further)
[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour, IMovement {

    /// <summary>
    /// If disabled, this will not make any modifications at all to the player's position or velocity.
    /// </summary>
    [SerializeField]
    protected AllBoolStat controlEnabled = new AllBoolStat(true);
    public AllBoolStat ControlEnabled { get { return controlEnabled; } }

    [SerializeField]
    public AllBoolStat movementInputEnabled = new AllBoolStat(true);
    public AllBoolStat MovementInputEnabled { get { return movementInputEnabled; } }

    [SerializeField]
    public AllBoolStat rotationInputEnabled = new AllBoolStat(true);
    public AllBoolStat RotationInputEnabled { get { return rotationInputEnabled; } }

    public float speed = 5.0f;

    NavMeshAgent agent;
    float timer = 100.0f;

    // Use this for initialization
    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        float sqrDist = Vector3.SqrMagnitude(transform.position - agent.destination);
        if(timer > 5.0f || sqrDist < 1.0f) {
            timer = 0.0f;
            agent.destination = GetRandomPath();
        }
    }

    Vector3 GetRandomPath() {
        float x = Random.Range(-45.0f, 45.0f);
        float z = Random.Range(-45.0f, 45.0f);
        return new Vector3(x, 0.0f, z);
    }

    public void HaltMovement() {
        agent.ResetPath();
        foreach (IMovementOverride movementOverride in GetComponentsInChildren<IMovementOverride>()) {
            movementOverride.Disable();
        }
    }

    public void SetVelocity(Vector3 worldDirectionNormalized) {
        agent.velocity = worldDirectionNormalized * agent.speed;
    }

    public Vector3 CurrentMovement() {
        return agent.velocity;
    }

}