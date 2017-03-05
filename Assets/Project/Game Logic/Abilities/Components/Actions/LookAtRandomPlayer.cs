using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtRandomPlayer : MonoBehaviour, IRotation {

    AllBoolStat rotationInputEnabled = new AllBoolStat(true);
    public AllBoolStat RotationInputEnabled { get { return rotationInputEnabled; } }

    public Quaternion CurrentRotation() {
        throw new NotImplementedException();
    }
    public void MoveRotation(Quaternion rotation) {
        throw new NotImplementedException();
    }
    public bool SetCurrentRotationOverride(IRotationOverride rotationOverride) {
        throw new NotImplementedException();
    }

    Rigidbody rigid;
    Transform target;
    float timeTillSwitch = 0.0f;

    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody>();
    }

    List<Transform> candidates = new List<Transform>();

    // Update is called once per frame
    void Update() {
        if (!rotationInputEnabled) {
            return;
        }

        timeTillSwitch -= Time.deltaTime;
        if (timeTillSwitch < 0.0f) {
            candidates.Clear();
            for (int i = 0; i < Player.Players.Count; ++i) {
                if (!Player.Players[i].dead) {
                    candidates.Add(Player.Players[i].Holder.transform);
                }
            }

            if (candidates.Count> 1) {
                candidates.Remove(target);  // remove curr target so will always change
            }

            target = candidates[UnityEngine.Random.Range(0, candidates.Count)];

            timeTillSwitch = UnityEngine.Random.Range(2.0f, 10.0f);
        }


        Quaternion rot = rigid.rotation;
        rot = Quaternion.Slerp(rot, Quaternion.LookRotation((target.position - transform.position).normalized), Time.deltaTime);
        rigid.MoveRotation(rot);
        
    }
}
