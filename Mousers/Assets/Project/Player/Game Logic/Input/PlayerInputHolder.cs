using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Interface used to denote a class which provides player input.
/// </summary>
public interface IPlayerInput : IPlayerInputHolder
{
    /// <summary>
    /// The transform component of the player.
    /// </summary>
    Transform Player { set; }
}

/// <summary>
/// Interface used to denote a class which is or holds an IPlayerInput.
/// </summary>
public interface IPlayerInputHolder
{
    /// <summary>
    /// A vector representing the direction the player should move in. Magnitude should be in the range [0, 1]. Vector is in screen space.
    /// </summary>
    Vector2 movementDirection { get; }
    /// <summary>
    /// A vector representing the direction the player should face. Vector is in world space.
    /// </summary>
    Vector3 rotationDirection { get; }
    /// <summary>
    /// GetKey for the registration binding.
    /// </summary>
    /// <returns></returns>
    bool getRegistering { get; }
    /// <summary>
    /// GetKey for the player's first ability.
    /// </summary>
    /// <returns></returns>
    bool getAbility1 { get; }
    /// <summary>
    /// GetKey for the player's second ability.
    /// </summary>
    /// <returns></returns>
    bool getAbility2 { get; }
}

/// <summary>
/// This is the script through which all final gameplay input will be handled. Child classes are for drag-drop construction.
/// </summary>
public class PlayerInputHolder : MonoBehaviour, IPlayerInputHolder
{
    public virtual IPlayerInput heldInput { get; set; }

    public Vector2 movementDirection { get { return heldInput.movementDirection; } }
    public Vector3 rotationDirection { get { return heldInput.rotationDirection; } }
    public bool getRegistering { get { return heldInput.getRegistering; } }
    public bool getAbility1 { get { return heldInput.getAbility1; } }
    public bool getAbility2 { get { return heldInput.getAbility2; } }

    protected void Start()
    {
        if (heldInput != null)
        {
            heldInput.Player = this.transform;
        }
    }
}