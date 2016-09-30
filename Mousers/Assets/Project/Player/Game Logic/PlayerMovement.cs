using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PhotonView))]
public class PlayerMovement : MonoBehaviour
{
    PhotonView view;
    Vector3 movementInput;
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

    //Queue<TimestampedData<FPSRotation>> bufferedTargetRotations = new Queue<TimestampedData<FPSRotation>>();

    /// <summary>
    /// The rotation we are using as the start point of interpolation.
    /// </summary>
    //TimestampedData<FPSRotation> previousTargetRotation;
    /// <summary>
    /// The rotation we are using as the endpoint of interpolation.
    /// </summary>
    //TimestampedData<FPSRotation> nextTargetRotation;

    //Stack<TimestampedData<FPSRotation>> rotationHistory = new Stack<TimestampedData<FPSRotation>>();

    [SerializeField]
    protected float speed = 3;

    [SerializeField]
    protected float acceleration = 200;

    [SerializeField]
    protected double bufferDelaySecs = 0.2f;
    public double BufferDelaySecs { get { return bufferDelaySecs; } }

    // Use this for initialization
    void Awake()
    {
        view = GetComponent<PhotonView>();
        previousTargetPosition = new TimestampedData<Vector3>(PhotonNetwork.time - 1, this.transform.position);
        nextTargetPosition = new TimestampedData<Vector3>(PhotonNetwork.time, this.transform.position);
        //previousTargetRotation = new TimestampedData<FPSRotation>(PhotonNetwork.time - 1, cameraRotator.rotation);
        //nextTargetRotation = new TimestampedData<FPSRotation>(PhotonNetwork.time, cameraRotator.rotation);
    }

    void Update()
    {
        if (!view.isMine)
        {
            UpdatePositionNetworked();
            //UpdateRotationNetworked();
        }
        else
        {
            Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //pull from input script

            Vector3 velocityXZ = velocity;
            velocityXZ.y = 0;
            Vector3 movementXZ = Vector3.MoveTowards(velocityXZ, movementInput * speed, Time.deltaTime * acceleration);

            //update our velocity
            velocity.x = movementXZ.x;
            velocity.z = movementXZ.z;

            //TODO: rotation

            //update position
            transform.position += Time.deltaTime * velocity;
        }

    }

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

        //limit our movement each frame to our max speed (with respect to the different dimensions). This prevents the jittery appearance of extrapolated data
        Vector3 currentPositionXZ = transform.position;
        currentPositionXZ.y = 0;
        Vector3 targetPositionXZ = targetPosition;
        targetPositionXZ.y = 0;

        Vector3 newPosition = Vector3.MoveTowards(currentPositionXZ, targetPositionXZ, Time.deltaTime * speed);

        float currentPositionY = transform.position.y;
        float targetPositionY = targetPosition.y;
        newPosition.y = Mathf.MoveTowards(currentPositionY, targetPositionY, 0);

        transform.position = newPosition;
    }

    /*
    void UpdateRotationNetworked()
    {
        while (bufferedTargetRotations.Count != 0 && nextTargetRotation.outputTime < PhotonNetwork.time)
        {
            //we have new data we can use
            previousTargetRotation = nextTargetRotation;
            nextTargetRotation = bufferedTargetRotations.Dequeue();
        }

        float lerpValue = Mathd.InverseLerp(previousTargetRotation.outputTime, nextTargetRotation.outputTime, PhotonNetwork.time);
        FPSRotation newRotation = Quaternion.SlerpUnclamped((FPSRotation)previousTargetRotation, (FPSRotation)nextTargetRotation, lerpValue);

        transform.localRotation = Quaternion.Euler(0, newRotation.yaw, 0);
        cameraRotator.localRotation = Quaternion.Euler(newRotation.pitch, 0, 0);
    }
     * */

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        TimestampedData<Vector3> positionData;
        //TimestampedData<FPSRotation> rotationData;

        if (stream.isWriting)
        {
            positionData = new TimestampedData<Vector3>(info.timestamp, transform.position);
            //rotationData = new TimestampedData<FPSRotation>(info.timestamp, cameraRotator.rotation);

            // We own this player: send the others our data
            stream.SendNext(positionData.data);
            //stream.SendNext(rotationData.data.pitch);
            //stream.SendNext(rotationData.data.yaw);
        }
        else
        {
            // Network player, receive data
            double recieveTime = info.timestamp;
            if (recieveTime <= latestData)
            {
                Debug.LogError("out of order");
            }
            else
            {
                latestData = recieveTime;
            }
            double outputTime = recieveTime + bufferDelaySecs; //This is the time at which we will display the player at this position.

            Vector3 position = (Vector3)stream.ReceiveNext();
            //FPSRotation rotation = new FPSRotation((float)stream.ReceiveNext(), (float)stream.ReceiveNext());

            positionData = new TimestampedData<Vector3>(outputTime, position);
            //rotationData = new TimestampedData<FPSRotation>(outputTime, rotation);

            //Debug.Log(bufferedTargetPositions.Count);

            bufferedTargetPositions.Enqueue(positionData);
            //bufferedTargetRotations.Enqueue(rotationData);
        }
    }
}