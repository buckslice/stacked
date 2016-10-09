using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(IPlayerInputHolder))]
public class CharacterSelectCursor : MonoBehaviour {
    [SerializeField]
    protected float speed = 6;

    [SerializeField]
    protected float acceleration = 50;

    [SerializeField]
    public int playerNumber = -1;

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

    Rigidbody2D rigid;
    PhotonView view;
    IPlayerInputHolder input;
    GameObject currentSelection;
    public GameObject CurrentSelection { get { return currentSelection; } }

    // Use this for initialization
    void Awake () {
        rigid = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        input = GetComponent<IPlayerInputHolder>();
        currentSelection = null;

        //set up tracked data to match to our current position and rotation
        previousTargetPosition = new TimestampedData<Vector2>(PhotonNetwork.time - 1, rigid.position);
        nextTargetPosition = new TimestampedData<Vector2>(PhotonNetwork.time, rigid.position);
    }

    public void Initialize(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        CharacterSelectIcon selectIcon = other.GetComponent<CharacterSelectIcon>();
        if (selectIcon != null)
        {
            currentSelection = selectIcon.getPlayerSetup();
        }
    }

	// Update is called once per frame
    void Update()
    {
        if (!view.isMine)
        {
            UpdatePositionNetworked();
        }
        else
        {
            if (controlEnabled)
            {
                UpdatePositionInput();
            }
        }

    }

    /// <summary>
    /// Updates our current position based off of player input.
    /// </summary>
    void UpdatePositionInput()
    {
        Vector2 velocity = rigid.velocity;

        Vector2 movementInput = input.movementDirection;

        Vector2 target = speed * movementInput;
        velocity = Vector2.MoveTowards(velocity, target, Time.deltaTime * acceleration);
        rigid.velocity = velocity;
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
        Vector2 targetPosition = Vector2.LerpUnclamped(previousTargetPosition, nextTargetPosition, lerpValue);

        //limit our movement each frame to our max speed (with respect to the different dimensions). This prevents the jittery appearance of extrapolated data.

        Vector2 newPosition = Vector2.MoveTowards(rigid.position, targetPosition, Time.deltaTime * speed);

        rigid.position = newPosition;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        TimestampedData<Vector2> positionData;

        if (stream.isWriting)
        {
            positionData = new TimestampedData<Vector2>(info.timestamp, rigid.position);

            // We own this player: send the others our data
            stream.SendNext(positionData.data);
        }
        else
        {
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
