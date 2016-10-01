using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class KeyboardMousePlayerInputHolder : PlayerInputHolder
{
    [SerializeField]
    protected KeyboardMousePlayerInput bindings;

    protected override IPlayerInput heldInput {
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
    public string horizontalMovementAxis = Tags.Axis.Horizontal;
    [SerializeField]
    public string verticalMovementAxis = Tags.Axis.Vertical;

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
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance = ((player.position.y - mouseRay.origin.y) / mouseRay.direction.y);
            Vector3 pointInWorld = mouseRay.origin + (distance * mouseRay.direction);

            Assert.AreApproximatelyEqual(pointInWorld.y, player.position.y);

            return pointInWorld - player.position;
        }
    }
}