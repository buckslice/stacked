using UnityEngine;
using System.Collections;

public class IKLimb : MonoBehaviour {
    public Transform upperArm, forearm, hand;
    public Transform elbowTarget;
    public Vector3 target;

    public bool IsEnabled, debug;

    public float transition = 1.0f;

    public bool idleOptimization = false;

    public enum HandRotations {
        KeepLocalRotation,
        KeepGlobalRotation,
        UseTargetRotation
    };
    public HandRotations handRotationPolicy = HandRotations.KeepLocalRotation;

    private Quaternion upperArmStartRotation, forearmStartRotation, handStartRotation;
    private Vector3 elbowTargetRelativeStartPosition;

    //helper GOs that are reused every frame
    private GameObject upperArmAxisCorrection, forearmAxisCorrection, handAxisCorrection;

    //hold last positions so recalculation is only done if needed
    private Vector3 lastUpperArmPosition, lastTargetPosition, lastElbowTargetPosition;

    float stepTime = -1.0f;
    float castCooldown = 0.0f;
    float targetLerpTime = 0.0f;
    Vector3 targetTarget;
    Vector3 targetStart;
    Transform parent;
    bool stepping = false;

    //Calculate ikAngle variable.
    float upperArmLength;
    float forearmLength;
    float armLength;
    float hypotenuse;

    SpiderBoss spider;

    void Start() {
        parent = transform.root;
        spider = parent.GetComponent<SpiderBoss>();
        targetTarget = target;

        upperArmStartRotation = upperArm.rotation;
        forearmStartRotation = forearm.rotation;
        handStartRotation = hand.rotation;
        //targetRelativeStartPosition = target.position - upperArm.position;
        elbowTargetRelativeStartPosition = elbowTarget.position - upperArm.position;

        //Calculate ikAngle variable.
        upperArmLength = Vector3.Distance(upperArm.position, forearm.position);
        forearmLength = Vector3.Distance(forearm.position, hand.position);
        armLength = upperArmLength + forearmLength;
        hypotenuse = upperArmLength;

        //create helper GOs
        upperArmAxisCorrection = new GameObject("upperArmAxisCorrection");
        forearmAxisCorrection = new GameObject("forearmAxisCorrection");
        handAxisCorrection = new GameObject("handAxisCorrection");

        //set helper hierarchy
        upperArmAxisCorrection.transform.parent = transform;
        forearmAxisCorrection.transform.parent = upperArmAxisCorrection.transform;
        handAxisCorrection.transform.parent = forearmAxisCorrection.transform;

        //guarantee first-frame update
        lastUpperArmPosition = upperArm.position + 5 * Vector3.up;

    }

    void LateUpdate() {
        if (!IsEnabled) {
            return;
        }


        bool shouldCast = false;
        float dist = Vector3.Distance(upperArm.position, target);
        if (armLength - dist < 1.0f) {
            shouldCast = true;
        } else {
            dist = Vector3.Distance(elbowTarget.position, forearm.transform.position);
            if (dist > 5.0f) {
                shouldCast = true;
            }
        }

        //Vector3 euler = upperArm.transform.localRotation.eulerAngles;
        //if (euler.y < 20.0f || euler.y > 100.0f) {
        //    shouldCast = true;
        //}

        stepTime -= Time.deltaTime;
        if ((shouldCast || stepTime < 0.0f) && !stepping) {
            RaycastHit hit;
            if (Physics.Raycast(elbowTarget.position, Vector3.down + parent.forward * 0.5f, out hit, 100.0f, spider.legCollisionLayer.value)) {
                if ((hit.point - targetTarget).sqrMagnitude < 1.0f) {
                    // do nothing because close enough
                } else {
                    targetTarget = hit.point;
                    targetStart = target;
                    targetLerpTime = 0.0f;
                    castCooldown = 0.5f;
                    stepTime = Random.value * 0.5f + 0.5f;
                    stepping = true;
                }
            }
        }

        // lift up leg and place to new target
        targetLerpTime += Time.deltaTime * 4.0f;
        if (targetLerpTime < 1.0f) {
            target = Vector3.Lerp(targetStart, targetTarget, targetLerpTime);
            target += Vector3.up * spider.legHeightCurve.Evaluate(targetLerpTime) * 4.0f;
        } else {
            target = targetTarget;
            stepping = false;
        }

        CalculateIK();
    }

    void CalculateIK() {
        if (
            idleOptimization
                &&
            lastUpperArmPosition == upperArm.position
                &&
            lastTargetPosition == target
                &&
            lastElbowTargetPosition == elbowTarget.position
        ) {
            if (debug) {
                Debug.DrawLine(forearm.position, elbowTarget.position, Color.yellow);
                Debug.DrawLine(upperArm.position, target, Color.red);
            }

            return;
        }

        lastUpperArmPosition = upperArm.position;
        lastTargetPosition = target;
        lastElbowTargetPosition = elbowTarget.position;

        float targetDistance = Vector3.Distance(upperArm.position, target);

        targetDistance = Mathf.Min(targetDistance, armLength - 0.001f); //Do not allow target distance be further away than the arm's length.

        float adjacent = (hypotenuse * hypotenuse - forearmLength * forearmLength + targetDistance * targetDistance) / (2 * targetDistance);
        float ikAngle = Mathf.Acos(adjacent / hypotenuse) * Mathf.Rad2Deg;

        if (float.IsNaN(ikAngle)) {
            return;
        }

        //Store pre-ik info.
        Transform upperArmParent = upperArm.parent;
        Transform forearmParent = forearm.parent;
        //Transform handParent = hand.parent;

        Vector3 upperArmScale = upperArm.localScale;
        Vector3 forearmScale = forearm.localScale;
        //Vector3 handScale = hand.localScale;
        Vector3 upperArmLocalPosition = upperArm.localPosition;
        Vector3 forearmLocalPosition = forearm.localPosition;
        //Vector3 handLocalPosition = hand.localPosition;

        Quaternion upperArmRotation = upperArm.rotation;
        Quaternion forearmRotation = forearm.rotation;
        //Quaternion handRotation = hand.rotation;
        //Quaternion handLocalRotation = hand.localRotation;

        //Reset arm.
        upperArm.rotation = upperArmStartRotation;
        forearm.rotation = forearmStartRotation;
        //hand.rotation = handStartRotation;

        //Vector3 p = forearm.position;
        //p.y = transform.position.y + 4.0f;
        //elbowTarget.position = p;

        //Work with temporaty game objects and align & parent them to the arm.
        transform.position = upperArm.position;
        transform.LookAt(target, elbowTarget.position - transform.position);

        upperArmAxisCorrection.transform.position = upperArm.position;
        upperArmAxisCorrection.transform.LookAt(forearm.position, upperArm.up);
        upperArm.parent = upperArmAxisCorrection.transform;

        forearmAxisCorrection.transform.position = forearm.position;
        forearmAxisCorrection.transform.LookAt(hand.position, forearm.up);
        forearm.parent = forearmAxisCorrection.transform;

        //handAxisCorrection.transform.position = hand.position;
        //hand.parent = handAxisCorrection.transform;

        //Reset targets.
        //elbowTarget.position = elbowTargetPosition;

        //Apply rotation for temporary game objects.
        upperArmAxisCorrection.transform.LookAt(target, elbowTarget.position - upperArmAxisCorrection.transform.position);

        Quaternion newLocal = Quaternion.Euler(upperArmAxisCorrection.transform.localRotation.eulerAngles - new Vector3(ikAngle, 0, 0));
        upperArmAxisCorrection.transform.localRotation = newLocal;

        forearmAxisCorrection.transform.LookAt(target, elbowTarget.position - upperArmAxisCorrection.transform.position);
        //handAxisCorrection.transform.rotation = target.rotation;

        //Restore limbs.
        upperArm.parent = upperArmParent;
        forearm.parent = forearmParent;
        //hand.parent = handParent;
        upperArm.localScale = upperArmScale;
        forearm.localScale = forearmScale;
        //hand.localScale = handScale;
        upperArm.localPosition = upperArmLocalPosition;
        forearm.localPosition = forearmLocalPosition;
        //hand.localPosition = handLocalPosition;

        //Transition.
        transition = Mathf.Clamp01(transition);
        upperArm.rotation = Quaternion.Slerp(upperArmRotation, upperArm.rotation, transition);
        forearm.rotation = Quaternion.Slerp(forearmRotation, forearm.rotation, transition);
        //hand.rotation = Quaternion.Slerp(handRotation, hand.rotation, transition);

        //switch (handRotationPolicy) {
        //    case HandRotations.KeepLocalRotation:
        //        hand.localRotation = handLocalRotation;

        //        break;
        //    case HandRotations.KeepGlobalRotation:
        //        hand.rotation = handRotation;

        //        break;
        //    case HandRotations.UseTargetRotation:
        //        hand.rotation = target.rotation;

        //        break;
        //}

        //Debug.
        if (debug) {
            Debug.DrawLine(forearm.position, elbowTarget.position, Color.yellow);
            Debug.DrawLine(upperArm.position, target, Color.red);

            //Debug.Log("[IK Limb] adjacent: " + adjacent);
        }
    }
}
