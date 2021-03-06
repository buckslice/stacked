﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

/// <summary>
/// Denotes a script as movement.
/// </summary>
public interface IMovement {
    AllBoolStat ControlEnabled { get; }
    AllBoolStat MovementInputEnabled { get; }
    void HaltMovement();
    void SetVelocity(Vector3 worldDirectionNormalized);
    void MovePosition(Vector3 worldPosition);

    bool SetCurrentMovementOverride(IMovementOverride movementOverride);

    /// <summary>
    /// Returns the current movement direction.
    /// </summary>
    /// <returns></returns>
    Vector3 CurrentMovement();
}

/// <summary>
/// Child interface to avoid having to modify speed on bossAggro for now
/// </summary>
public interface IMovementSpeed : IMovement {
    MultiplierFloatStat Speed { get; }
}

    /// <summary>
    /// Denotes a script which overrides an IMovement. New IMovementOverrides will disable previous ones.
    /// TODO: maybe move MovementOverride to inherit from AbstractStatus
    /// </summary>
    public interface IMovementOverride {
    bool Disable();
}

public interface IRotation {
    AllBoolStat RotationInputEnabled { get; }
    bool SetCurrentRotationOverride(IRotationOverride rotationOverride);
    void MoveRotation(Quaternion rotation);

    /// <summary>
    /// Returns the current forward direction.
    /// </summary>
    /// <returns></returns>
    Quaternion CurrentRotation();
}

/// <summary>
/// Denotes a script which overrides an IRotation. New IRotationOverride will disable previous ones.
/// </summary>
public interface IRotationOverride {
    bool Disable();
}

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(IPlayerInputHolder))]
public class PlayerMovement : MonoBehaviour, IMovement, IMovementSpeed, IRotation {

    [SerializeField]
    protected MultiplierFloatStat speed = new MultiplierFloatStat(6);
    public MultiplierFloatStat Speed { get { return speed; } }

    [SerializeField]
    protected MultiplierFloatStat rotationSpeedDegrees = new MultiplierFloatStat(360);

    [SerializeField]
    protected MultiplierFloatStat acceleration = new MultiplierFloatStat(50);

    [SerializeField]
    protected double bufferDelaySecs = 0.2f;
    public double BufferDelaySecs { get { return bufferDelaySecs; } }

    /// <summary>
    /// If disabled, this will not make any modifications at all to the player's position or velocity.
    /// </summary>
    [SerializeField]
    protected AllBoolStat controlEnabled = new AllBoolStat(true);
    public AllBoolStat ControlEnabled { get { Debug.LogWarning("I (Derek) am depreciating ControlEnabled in favor of MovementInputEnabled and RotationInputEnabled.", this); return controlEnabled; } }

    [SerializeField]
    protected AllBoolStat movementInputEnabled = new AllBoolStat(true);
    public AllBoolStat MovementInputEnabled { get { return movementInputEnabled; } }

    [SerializeField]
    protected AllBoolStat rotationInputEnabled = new AllBoolStat(true);
    public AllBoolStat RotationInputEnabled { get { return rotationInputEnabled; } }

    Rigidbody rigid;
    PhotonView view;
    IPlayerInputHolder input;

    IMovementOverride currentMovementOverride = null;
    IRotationOverride currentRotationOverride = null;

    /// <summary>
    /// Photon timestamp of the most recent data, used to warn if data is being received out of order.
    /// </summary>
    double latestData = 0;

    Queue<TimestampedData<Vector3>> bufferedTargetPositions = new Queue<TimestampedData<Vector3>>();

    /// <summary>
    /// The position we are using as the start point of interpolation.
    /// </summary>
    TimestampedData<Vector3> previousTargetPosition;
    /// <summary>
    /// The position we are using as the endpoint of interpolation.
    /// </summary>
    TimestampedData<Vector3> nextTargetPosition;

    Queue<TimestampedData<float>> bufferedTargetRotations = new Queue<TimestampedData<float>>();

    /// <summary>
    /// The rotation we are using as the start point of interpolation. The float is the angle in degrees around the Y-axis.
    /// </summary>
    TimestampedData<float> previousTargetRotation;
    /// <summary>
    /// The rotation we are using as the endpoint of interpolation. The float is the angle in degrees around the Y-axis.
    /// </summary>
    TimestampedData<float> nextTargetRotation;

    // Use this for initialization
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
        view = GetComponent<PhotonView>();
        input = GetComponent<IPlayerInputHolder>();

        //set up tracked data to match to our current position and rotation
        previousTargetPosition = new TimestampedData<Vector3>(PhotonNetwork.time - 1, rigid.position);
        nextTargetPosition = new TimestampedData<Vector3>(PhotonNetwork.time, rigid.position);
        previousTargetRotation = new TimestampedData<float>(PhotonNetwork.time - 1, this.rotation);
        nextTargetRotation = new TimestampedData<float>(PhotonNetwork.time, this.rotation);
    }

    void Update()
    {
        if (!controlEnabled)
        {
            return;
        }

        if (!view.isMine)
        {
            UpdatePositionNetworked();
            UpdateRotationNetworked();
        }
        else
        {
            UpdatePositionInput();
            UpdateRotationInput();
        }

    }

    float rotation { get { return rigid.rotation.eulerAngles.y; } }

    /// <summary>
    /// Causes the player to stop all movement, instead of the short slowdown that occurs when input goes to zero
    /// </summary>
    public void HaltMovement()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        foreach (IMovementOverride movementOverride in GetComponentsInChildren<IMovementOverride>()) {
            movementOverride.Disable();
        }
    }

    public void SetVelocity(Vector3 worldDirectionNormalized)
    {
        Assert.AreApproximatelyEqual(worldDirectionNormalized.y, 0);
        rigid.velocity = speed * worldDirectionNormalized;
    }

    public void MovePosition(Vector3 worldPosition) {
        rigid.MovePosition(worldPosition);
    }

    public Vector3 CurrentMovement() {
        if (!view.isMine) {
            return nextTargetPosition.data - previousTargetPosition.data;

        } else if (controlEnabled) {
            if (movementInputEnabled) {
                return input.movementDirection.ConvertFromInputToWorld();
            } else {
                return rigid.velocity;
            }

        } else {
            return Vector3.zero;
        }
    }

    Quaternion IRotation.CurrentRotation() {
        return rigid.rotation;
    }

    void IRotation.MoveRotation(Quaternion rotation) {
        rigid.MoveRotation(rotation);
    }

    /// <summary>
    /// Updates our current position based off of player input.
    /// </summary>
    void UpdatePositionInput()
    {
        if (!movementInputEnabled)
        {
            //rigid.velocity = Vector3.MoveTowards(rigid.velocity, Vector3.zero, Time.deltaTime * acceleration);
            return;
        }
        Vector3 velocity = rigid.velocity;
        //we do not control vertical movement through this script. Negate it so it doesn't affect normalization or MoveTowards.
        velocity.y = 0;
        Vector2 movementInput = input.movementDirection;

        Vector3 targetXZ;

        //Move in camera direction
        targetXZ = speed * movementInput.ConvertFromInputToWorld();

        Assert.AreApproximatelyEqual(targetXZ.y, 0);
        velocity = Vector3.MoveTowards(velocity, targetXZ, Time.deltaTime * acceleration); //TODO: look into using AddForce, with the ignore-mass acceleration force-mode?

        //we do not control vertical movement through this script; reset to the rigidbody-controlled value.
        velocity.y = rigid.velocity.y;

        //update actual velocity
        rigid.velocity = velocity;
    }

    /// <summary>
    /// Updates our current rotation based off of player input.
    /// </summary>
    void UpdateRotationInput()
    {
        if (!RotationInputEnabled)
        {
            return;
        }
        Vector3 rotationInput = input.rotationDirection;
        Quaternion targetRotation;
        if (rotationInput.sqrMagnitude != 0)
        {
            //use rotation input
            targetRotation = Quaternion.LookRotation(rotationInput);
        }
        else
        {
            //use movement input as rotation input
            Vector2 movementInput = input.movementDirection;
            if(movementInput.sqrMagnitude == 0)
            {
                //no input at all, do nothing
                return;
            }
            //convert from screen space to world space
            Vector3 targetDirection = movementInput.ConvertFromInputToWorld();
            Assert.AreApproximatelyEqual(targetDirection.y, 0);
            targetRotation = Quaternion.LookRotation(targetDirection);
        }

        rigid.MoveRotation(targetRotation);
        rigid.rotation = targetRotation;
    }

    /// <summary>
    /// Updates our current position based off of data received from the network.
    /// </summary>
    void UpdatePositionNetworked()
    {
        if (!MovementInputEnabled) { return; }

        while (bufferedTargetPositions.Count != 0 && nextTargetPosition.outputTime < PhotonNetwork.time)
        {
            //we have new data we can use
            previousTargetPosition = nextTargetPosition;
            nextTargetPosition = bufferedTargetPositions.Dequeue();
        }

        float lerpValue = Mathd.InverseLerp(previousTargetPosition.outputTime, nextTargetPosition.outputTime, PhotonNetwork.time);
        Vector3 targetPosition = Vector3.LerpUnclamped(previousTargetPosition, nextTargetPosition, lerpValue);

        //limit our movement each frame to our max speed (with respect to the different dimensions). This prevents the jittery appearance of extrapolated data.
        Vector3 currentPositionXZ = rigid.position;
        currentPositionXZ.y = 0;
        Vector3 targetPositionXZ = targetPosition;
        targetPositionXZ.y = 0;

        Vector3 newPosition = Vector3.MoveTowards(currentPositionXZ, targetPositionXZ, Time.deltaTime * speed);

        //interpolate the y axis without limitation
        newPosition.y = targetPosition.y;

        rigid.MovePosition(newPosition);
    }

    /// <summary>
    /// Updates our current rotation based off of data received from the network.
    /// </summary>
    void UpdateRotationNetworked()
    {
        if (!RotationInputEnabled) { return; }

        while (bufferedTargetRotations.Count != 0 && nextTargetRotation.outputTime < PhotonNetwork.time)
        {
            //we have new data we can use
            previousTargetRotation = nextTargetRotation;
            nextTargetRotation = bufferedTargetRotations.Dequeue();
        }

        float lerpValue = Mathd.InverseLerp(previousTargetRotation.outputTime, nextTargetRotation.outputTime, PhotonNetwork.time);
        Quaternion targetRotation = Quaternion.AngleAxis(Mathf.LerpAngle(previousTargetRotation, nextTargetRotation, lerpValue), Vector3.up);

        //limit our movement each frame to our max rotation speed. This prevents the jittery appearance of extrapolated data.
        Quaternion newRotation = Quaternion.RotateTowards(rigid.rotation, targetRotation, Time.deltaTime * rotationSpeedDegrees);

        rigid.MoveRotation(newRotation);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        TimestampedData<Vector3> positionData;
        TimestampedData<float> rotationData;

        if (stream.isWriting)
        {
            positionData = new TimestampedData<Vector3>(info.timestamp, rigid.position);
            rotationData = new TimestampedData<float>(info.timestamp, this.rotation);

            // We own this player: send the others our data
            stream.SendNext(positionData.data);
            stream.SendNext(rotationData.data);
        }
        else
        {
            // Network player, receive data
            double recieveTime = info.timestamp;
            if (recieveTime <= latestData)
            {
                Debug.LogWarning("received data is out of order");
            }
            else
            {
                latestData = recieveTime;
            }
            double outputTime = recieveTime + bufferDelaySecs; //This is the time at which we will display the player at this position.

            Vector3 position = (Vector3)stream.ReceiveNext();
            float rotation = (float)stream.ReceiveNext();

            positionData = new TimestampedData<Vector3>(outputTime, position);
            rotationData = new TimestampedData<float>(outputTime, rotation);

            //Debug.Log(bufferedTargetPositions.Count);

            bufferedTargetPositions.Enqueue(positionData);
            bufferedTargetRotations.Enqueue(rotationData);
        }
    }

    bool IMovement.SetCurrentMovementOverride(IMovementOverride movementOverride) {
        bool result = currentMovementOverride != null;
        if (result) {
            currentMovementOverride.Disable();
        }

        currentMovementOverride = movementOverride;
        return result;
    }

    bool IRotation.SetCurrentRotationOverride(IRotationOverride rotationOverride) {
        bool result = currentRotationOverride != null;
        if (result) {
            currentRotationOverride.Disable();
        }

        currentRotationOverride = rotationOverride;
        return result;
    }
}