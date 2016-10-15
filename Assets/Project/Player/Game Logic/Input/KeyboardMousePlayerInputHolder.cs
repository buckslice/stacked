using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Intended to be used only in development to create players using the inspector. For all other purposes, use a PlayerInputHolder and set heldInput via script.
/// </summary>
public class KeyboardMousePlayerInputHolder : PlayerInputHolder
{
    [SerializeField]
    protected KeyboardMousePlayerInput bindings;

    public override IPlayerInput heldInput
    {
        get
        {
            return bindings;
        }
        set
        {
            bindings = (KeyboardMousePlayerInput)value;
            bindings.Player = this.transform;
        }
    }
}

[System.Serializable]
public class KeyboardMousePlayerInput : IPlayerInput
{
    [SerializeField]
    public string horizontalMovementAxis = Tags.Input.Horizontal;
    [SerializeField]
    public string verticalMovementAxis = Tags.Input.Vertical;

    [SerializeField]
    public KeyCode registeringKey = Tags.Input.Registering;
    [SerializeField]
    public KeyCode startingKey = Tags.Input.Starting;
    [SerializeField]
    public KeyCode basicAttackKey = Tags.Input.BasicAttack;
    [SerializeField]
    public KeyCode ability1Key = Tags.Input.Ability1;
    [SerializeField]
    public KeyCode ability2Key = Tags.Input.Ability2;

    Transform player;
    public Transform Player { set { player = value; } }

    public Vector2 movementDirection
    {
        get
        {
            Vector2 result = new Vector3(Input.GetAxisRaw(horizontalMovementAxis), Input.GetAxisRaw(verticalMovementAxis));
            result = Vector2.ClampMagnitude(result, 1f);
            return result;
        }
    }
    public Vector3 rotationDirection
    {
        get
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance = ((player.position.y - ray.origin.y) / ray.direction.y);
            Vector3 pointInWorld = ray.origin + (distance * ray.direction);

            Assert.AreApproximatelyEqual(pointInWorld.y, player.position.y);

            return pointInWorld - player.position;
        }
    }
    public bool getRegistering { get { return Input.GetKey(registeringKey); } }
    public bool getStarting { get { return Input.GetKey(startingKey); } }
    public bool getBasicAttack { get { return Input.GetKey(basicAttackKey); } }
    public bool getAbility1 { get { return Input.GetKey(ability1Key); } }// || Input.GetMouseButton(0); } }
    public bool getAbility2 { get { return Input.GetKey(ability2Key); } }// || Input.GetMouseButton(1); } }

    public bool getRegisteringDown { get { return Input.GetKeyDown(registeringKey); } }
    public bool getStartingDown { get { return Input.GetKeyDown(startingKey); } }
    public bool getBasicAttackDown { get { return Input.GetKeyDown(basicAttackKey); } }
    public bool getAbility1Down { get { return Input.GetKeyDown(ability1Key); } }
    public bool getAbility2Down { get { return Input.GetKeyDown(ability2Key); } }
}