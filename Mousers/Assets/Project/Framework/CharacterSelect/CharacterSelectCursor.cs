using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(IPlayerInputHolder))]
public class CharacterSelectCursor : MonoBehaviour {
    [SerializeField]
    protected float speed = 6;

    [SerializeField]
    protected float acceleration = 50;

    [SerializeField]
    public int playerNumber;

    Rigidbody2D rigid;
    IPlayerInputHolder input;
    GameObject currentSelection;
    // Use this for initialization
    void Awake () {
        rigid = GetComponent<Rigidbody2D>();
        input = GetComponent<IPlayerInputHolder>();
        currentSelection = null;
    }

    public void Initialize(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

	// Update is called once per frame
	void Update () {
        UpdatePositionInput();
	}

    void UpdatePositionInput()
    {
        Vector2 velocity = rigid.velocity;

        Vector2 movementInput = input.movementDirection;

        Vector2 target = speed * movementInput;
        velocity = Vector2.MoveTowards(velocity, target, Time.deltaTime * acceleration);
        rigid.velocity = velocity;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        try {
            currentSelection = other.GetComponent<CharacterSelectIcon>().getPlayerSetup();
        }
        catch
        {
            //Do nothing if the collision isn't a CharacterSelectIcon
        }
    }

    public GameObject getCurrentSelection()
    {
        return currentSelection;
    }
}
