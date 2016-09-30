using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class KeyboardMousePlayerInputHolder : PlayerInputHolder, IPlayerInput
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

    public Vector3 movementDirection
    {
        get
        {
            Vector3 result = new Vector3(Input.GetAxisRaw(horizontalMovementAxis), 0, Input.GetAxisRaw(verticalMovementAxis));
            result = Vector3.ClampMagnitude(result, 1f);
            return result;
        }
    }
    public Quaternion rotationDirection
    {
        get
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance = (-mouseRay.origin.y / mouseRay.direction.y);
            Vector3 pointInWorld = mouseRay.origin + (distance * mouseRay.direction);

            Assert.AreApproximatelyEqual(pointInWorld.y, 0);

            return Quaternion.LookRotation(pointInWorld);
        }
    }
}