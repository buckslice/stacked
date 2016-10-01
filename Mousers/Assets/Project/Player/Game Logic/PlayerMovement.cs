using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(IPlayerInputHolder))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    protected float speed = 6;

    [SerializeField]
    protected float rotationSpeedDegrees = 360;

    [SerializeField]
    protected float acceleration = 50;

    [SerializeField]
    protected double bufferDelaySecs = 0.2f;
    public double BufferDelaySecs { get { return bufferDelaySecs; } }

    Rigidbody rigid;
    PhotonView view;
    IPlayerInputHolder input;

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
        view = GetComponent<PhotonView>();
        input = GetComponent<IPlayerInputHolder>();

        //set up tracked data to match to our current position and rotation
        previousTargetPosition = new TimestampedData<Vector3>(PhotonNetwork.time - 1, rigid.position);
        nextTargetPosition = new TimestampedData<Vector3>(PhotonNetwork.time, rigid.position);
        previousTargetRotation = new TimestampedData<float>(PhotonNetwork.time - 1, this.rotation);
        nextTargetRotation = new TimestampedData<float>(PhotonNetwork.time, this.rotation);

        if (!view.isMine)
        {
            Destroy(rigid);
            rigid = null;
        }
    }

    void Update()
    {
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
    /// Updates our current position based off of player input.
    /// </summary>
    void UpdatePositionInput()
    {
        Vector3 velocity = rigid.velocity;
        //we do not control vertical movement through this script. Negate it so it doesn't affect normalization or MoveTowards.
        velocity.y = 0;
        Vector2 movementInput = input.movementDirection;
        Vector3 directionInput = input.rotationDirection;

        Vector3 targetXZ;

        if (directionInput.sqrMagnitude == 0)
        {
            //no aiming input, use camera direction
            targetXZ = speed * movementInput.ConvertFromInputToWorld();
        }
        else
        {
            //aiming input exists, use input direction as forward
            Quaternion inputDirection = Quaternion.LookRotation(directionInput);
            targetXZ = inputDirection * Vector3.right * movementInput.x + inputDirection * Vector3.forward * movementInput.y;
            targetXZ *= speed;
        }

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

        rigid.MoveRotation(Quaternion.RotateTowards(rigid.rotation, targetRotation, rotationSpeedDegrees * Time.deltaTime));
    }

    /// <summary>
    /// Updates our current position based off of data received from the network.
    /// </summary>
    void UpdatePositionNetworked()
    {
        while (bufferedTargetPositions.Count != 0 && nextTargetPosition.outputTime < PhotonNetwork.time)
        {
            //we have new data we can use
            previousTargetPosition = nextTargetPosition;
            nextTargetPosition = bufferedTargetPositions.Dequeue();
        }

        float lerpValue = Mathd.InverseLerp(previousTargetPosition.outputTime, nextTargetPosition.outputTime, PhotonNetwork.time);
        Vector3 targetPosition = Vector3.LerpUnclamped(previousTargetPosition, nextTargetPosition, lerpValue);

        //limit our movement each frame to our max speed (with respect to the different dimensions). This prevents the jittery appearance of extrapolated data.
        Vector3 currentPositionXZ = transform.position;
        currentPositionXZ.y = 0;
        Vector3 targetPositionXZ = targetPosition;
        targetPositionXZ.y = 0;

        Vector3 newPosition = Vector3.MoveTowards(currentPositionXZ, targetPositionXZ, Time.deltaTime * speed);

        //interpolate the y axis without limitation
        newPosition.y = targetPosition.y;

        transform.position = newPosition;
    }

    /// <summary>
    /// Updates our current rotation based off of data received from the network.
    /// </summary>
    void UpdateRotationNetworked()
    {
        while (bufferedTargetRotations.Count != 0 && nextTargetRotation.outputTime < PhotonNetwork.time)
        {
            //we have new data we can use
            previousTargetRotation = nextTargetRotation;
            nextTargetRotation = bufferedTargetRotations.Dequeue();
        }

        float lerpValue = Mathd.InverseLerp(previousTargetRotation.outputTime, nextTargetRotation.outputTime, PhotonNetwork.time);
        Quaternion targetRotation = Quaternion.AngleAxis(Mathf.LerpAngle(previousTargetRotation, nextTargetRotation, lerpValue), Vector3.up);

        //limit our movement each frame to our max rotation speed. This prevents the jittery appearance of extrapolated data.
        Quaternion newRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeedDegrees);

        transform.localRotation = newRotation;
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
}