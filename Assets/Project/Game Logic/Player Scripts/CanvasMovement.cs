using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(IPlayerInputHolder))]
public class CanvasMovement : MonoBehaviour {
    [SerializeField]
    protected float speed = 6;

    [SerializeField]
    protected double bufferDelaySecs = 0.2f;
    public double BufferDelaySecs { get { return bufferDelaySecs; } }

    Queue<TimestampedData<Vector2>> bufferedTargetPositions = new Queue<TimestampedData<Vector2>>();

    /// <summary>
    /// The position we are using as the start point of interpolation.
    /// </summary>
    TimestampedData<Vector2> previousTargetPosition;
    /// <summary>
    /// The position we are using as the endpoint of interpolation.
    /// </summary>
    TimestampedData<Vector2> nextTargetPosition;

    /// <summary>
    /// If disabled, this will not make any modifications at all to the player's position or velocity. Only applies to players using input.
    /// </summary>
    [SerializeField]
    public bool controlEnabled = true;

    RectTransform rectTransform;
    PhotonView view;
    IPlayerInputHolder input;

    // Use this for initialization
    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        view = GetComponent<PhotonView>();
        input = GetComponent<IPlayerInputHolder>();

        //set up tracked data to match to our current position and rotation
        previousTargetPosition = new TimestampedData<Vector2>(PhotonNetwork.time - 1, transform.position);
        nextTargetPosition = new TimestampedData<Vector2>(PhotonNetwork.time, transform.position);
    }

    // Update is called once per frame
    void Update() {
        if (!controlEnabled) {return;}

        if (!view.isMine) {
            UpdatePositionNetworked();
        } else {
            UpdatePositionInput(); transform.position += (Vector3)input.movementDirection * speed * Screen.width / 800.0f;
        }
    }

    /// <summary>
    /// Updates our current position based off of player input.
    /// </summary>
    void UpdatePositionInput() {
        Vector2 newPosition = rectTransform.anchoredPosition3D + (Vector3)input.movementDirection * speed * Screen.width / 800.0f;

        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.x, 0, Screen.height);

        rectTransform.anchoredPosition = newPosition;
    }

    /// <summary>
    /// Updates our current position based off of data received from the network.
    /// </summary>
    void UpdatePositionNetworked() {
        while (bufferedTargetPositions.Count != 0 && nextTargetPosition.outputTime < PhotonNetwork.time) {
            //we have new data we can use
            previousTargetPosition = nextTargetPosition;
            nextTargetPosition = bufferedTargetPositions.Dequeue();
        }

        float lerpValue = Mathd.InverseLerp(previousTargetPosition.outputTime, nextTargetPosition.outputTime, PhotonNetwork.time);
        Vector2 targetPosition = Vector2.LerpUnclamped(previousTargetPosition, nextTargetPosition, lerpValue);

        //limit our movement each frame to our max speed (with respect to the different dimensions). This prevents the jittery appearance of extrapolated data.

        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

        transform.position = newPosition;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        TimestampedData<Vector2> positionData;

        if (stream.isWriting) {
            positionData = new TimestampedData<Vector2>(info.timestamp, transform.position);

            // We own this player: send the others our data
            stream.SendNext(positionData.data);
        } else {
            // Network player, receive data
            double recieveTime = info.timestamp;
            double outputTime = recieveTime + bufferDelaySecs; //This is the time at which we will display the player at this position.

            Vector2 position = (Vector2)stream.ReceiveNext();

            positionData = new TimestampedData<Vector2>(outputTime, position);

            //Debug.Log(bufferedTargetPositions.Count);

            bufferedTargetPositions.Enqueue(positionData);
        }
    }
}
