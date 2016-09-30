using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface used to denote a class which provides player input.
/// </summary>
public interface IPlayerInput
{
    /// <summary>
    /// A vector representing the direction the player should move in. Magnitude should be in the range [0, 1]
    /// </summary>
    Vector3 movementDirection { get; }
    /// <summary>
    /// A vector representing the direction the player should face.
    /// </summary>
    Quaternion rotationDirection { get; }
}

/// <summary>
/// This is the script through which all final gameplay input will be handled. Child classes are for drag-drop construction.
/// </summary>
public class PlayerInputHolder : MonoBehaviour, IPlayerInput {
    protected virtual IPlayerInput heldInput { get; set; }

    public Vector3 movementDirection { get { return heldInput.movementDirection; } }
    public Quaternion rotationDirection { get { return heldInput.rotationDirection; } }
}