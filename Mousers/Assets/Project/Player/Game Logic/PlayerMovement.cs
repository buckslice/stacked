using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(IPlayerInput))]
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


    PhotonView view;
    IPlayerInput input;

    /// <summary>
    /// Photon timestamp of the most recent data, used to warn if data is being received out of order.
    /// </summary>
    double latestData = 0;
    Vector3 velocity = Vector3.zero;

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
        view = GetComponent<PhotonView>();
        input = GetComponent<IPlayerInput>();

        //set up tracked data to match to our current position and rotation
        previousTargetPosition = new TimestampedData<Vector3>(PhotonNetwork.time - 1, this.transform.position);
        nextTargetPosition = new TimestampedData<Vector3>(PhotonNetwork.time, this.transform.position);
        previousTargetRotation = new TimestampedData<float>(PhotonNetwork.time - 1, this.rotation);
        nextTargetRotation = new TimestampedData<float>(PhotonNetwork.time, this.rotation);
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
            UpdateUsingInput();
        }

    }

    float rotation { get { return this.transform.rotation.eulerAngles.y; } }

    /// <summary>
    /// Update the position and rotation of this object by obtaining and processing player input.
    /// </summary>
    void UpdateUsingInput()
    {
        Vector3 movementInput = input.movementDirection;

        Vector3 velocityXZ = velocity;
        velocityXZ.y = 0;
        Vector3 movementXZ = Vector3.MoveTowards(velocityXZ, movementInput * speed, Time.deltaTime * acceleration);

        //update our velocity
        velocity.x = movementXZ.x;
        velocity.z = movementXZ.z;

        //update position
        transform.position += Time.deltaTime * velocity;

        //update rotation
        Quaternion targetRotation = input.rotationDirection;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeedDegrees * Time.deltaTime);
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
        Quaternion newRotation = Quaternion.AngleAxis(Mathf.LerpAngle(previousTargetRotation, nextTargetRotation, lerpValue), Vector3.up);

        transform.localRotation = newRotation;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        TimestampedData<Vector3> positionData;
        TimestampedData<float> rotationData;

        if (stream.isWriting)
        {
            positionData = new TimestampedData<Vector3>(info.timestamp, transform.position);
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