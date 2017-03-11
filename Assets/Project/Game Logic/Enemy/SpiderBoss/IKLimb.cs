using UnityEngine;
using System.Collections;

public class IKLimb : MonoBehaviour {
    public Transform upperArm, forearm, hand;
    public Transform elbowTarget;
    public Vector3 target;

    public bool IsEnabled, debug;

    public float transition = 1.0f;

    public bool idleOptimization = false;

    private Quaternion upperArmStartRotation, forearmStartRotation, handStartRotation;
    private Vector3 elbowTargetRelativeStartPosition;

    //helper GOs that are reused every frame
    private GameObject upperArmAxisCorrection, forearmAxisCorrection;

    //hold last positions so recalculation is only done if needed
    private Vector3 lastUpperArmPosition, lastTargetPosition, lastElbowTargetPosition;

    float targetLerpTime = 0.0f;
    Vector3 targetTarget;
    Vector3 targetStart;
    Transform parent;
    public bool stepping = false;

    //Calculate ikAngle variable.
    float upperArmLength;
    float forearmLength;
    float armLength;
    float hypotenuse;

    public int legIndex;   // [0,3] 0 being frontmost, each side
    SpiderBoss spider;
    Vector3 lastElbowOffset;

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

        //set helper hierarchy
        upperArmAxisCorrection.transform.parent = transform;
        forearmAxisCorrection.transform.parent = upperArmAxisCorrection.transform;

        //guarantee first-frame update
        lastUpperArmPosition = upperArm.position + 5 * Vector3.up;

        if (RaycastStep(out hit)) {
            target = hit.point;
            lastElbowOffset = hit.point - elbowTarget.position;
        }
    }

    void LateUpdate() {
        if (!IsEnabled) {
            return;
        }

        CheckStep();

        CalculateIK();
    }

    void CheckStep() {
        Ray r = GetStepRay();
        Debug.DrawRay(r.origin, r.direction * 10.0f, Color.magenta);

        // for sure need to take step if leg is too far away
        if (!stepping && Vector3.Distance(upperArm.position, targetTarget) > armLength - 0.1f) {
            TakeStep();
        }

        // left leg to new target and lift it up
        targetLerpTime += Time.deltaTime * 4.0f;
        if (targetLerpTime < 1.0f) {
            target = Vector3.Lerp(targetStart, targetTarget, targetLerpTime);
            target += Vector3.up * spider.legHeightCurve.Evaluate(targetLerpTime) * 4.0f;
        } else {
            target = targetTarget;
            stepping = false;
        }
    }

    public bool TakeStep() {
        if (RaycastStep(out hit)) {
            targetTarget = hit.point;
            lastElbowOffset = hit.point - elbowTarget.position;
            targetStart = target;
            targetLerpTime = 0.0f;
            stepping = true;
            return true;
        }
        return false;
    }

    public float GetSqrDistFromResting() {
        return Vector3.SqrMagnitude(elbowTarget.position + lastElbowOffset - targetTarget);
    }

    RaycastHit hit;
    bool RaycastStep(out RaycastHit hit) {
        return Physics.Raycast(GetStepRay(), out hit, 100.0f, spider.legCollisionLayer);
    }

    Ray GetStepRay() {
        Vector3 offset = Vector3.zero;
        if (legIndex == 0) {
            offset = parent.forward * 0.75f;
        } else if (legIndex == 1) {
            offset = parent.forward * 0.4f;
        } else if (legIndex == 1) {
            offset = parent.forward * 0.15f;
        } else if (legIndex == 3) {
            offset = -parent.forward * 0.25f;
        }
        return new Ray(elbowTarget.position, (Vector3.down + offset).normalized);
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

        Vector3 upperArmScale = upperArm.localScale;
        Vector3 forearmScale = forearm.localScale;
        Vector3 upperArmLocalPosition = upperArm.localPosition;
        Vector3 forearmLocalPosition = forearm.localPosition;

        Quaternion upperArmRotation = upperArm.rotation;
        Quaternion forearmRotation = forearm.rotation;

        //Reset arm.
        upperArm.rotation = upperArmStartRotation;
        forearm.rotation = forearmStartRotation;

        //Work with temporaty game objects and align & parent them to the arm.
        transform.position = upperArm.position;
        transform.LookAt(target, elbowTarget.position - transform.position);

        upperArmAxisCorrection.transform.position = upperArm.position;
        upperArmAxisCorrection.transform.LookAt(forearm.position, upperArm.up);
        upperArm.parent = upperArmAxisCorrection.transform;

        forearmAxisCorrection.transform.position = forearm.position;
        forearmAxisCorrection.transform.LookAt(hand.position, forearm.up);
        forearm.parent = forearmAxisCorrection.transform;

        //Apply rotation for temporary game objects.
        upperArmAxisCorrection.transform.LookAt(target, elbowTarget.position - upperArmAxisCorrection.transform.position);

        Quaternion newLocal = Quaternion.Euler(upperArmAxisCorrection.transform.localRotation.eulerAngles - new Vector3(ikAngle, 0, 0));
        upperArmAxisCorrection.transform.localRotation = newLocal;

        forearmAxisCorrection.transform.LookAt(target, elbowTarget.position - upperArmAxisCorrection.transform.position);

        //Restore limbs.
        upperArm.parent = upperArmParent;
        forearm.parent = forearmParent;
        upperArm.localScale = upperArmScale;
        forearm.localScale = forearmScale;
        upperArm.localPosition = upperArmLocalPosition;
        forearm.localPosition = forearmLocalPosition;

        //Transition.
        transition = Mathf.Clamp01(transition);
        upperArm.rotation = Quaternion.Slerp(upperArmRotation, upperArm.rotation, transition);
        forearm.rotation = Quaternion.Slerp(forearmRotation, forearm.rotation, transition);

        //Debug.
        if (debug) {
            Debug.DrawLine(forearm.position, elbowTarget.position, Color.yellow);
            Debug.DrawLine(upperArm.position, target, Color.red);

            //Debug.Log("[IK Limb] adjacent: " + adjacent);
        }
    }
}
