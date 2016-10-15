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
/// Contains extension methods for PlayerInput.
/// </summary>
public static class PlayerInputExtension
{
    public static bool AnyKey(this IPlayerInputHolder self)
    {
        return self.getBasicAttack ||
            self.getAbility1 ||
            self.getAbility2 ||
            self.getSubmit ||
            self.getStart;
    }

    public static bool AnyAxis(this IPlayerInputHolder self)
    {
        return self.movementDirection != Vector2.zero ||
            self.rotationDirection != Vector3.zero;
    }

    public static bool AnyInput(this IPlayerInputHolder self)
    {
        return self.AnyKey() || self.AnyAxis();
    }
}

/// <summary>
/// Interface used to denote a class which is or holds an IPlayerInput.
/// </summary>
public interface IPlayerInputHolder {
    /// <summary>
    /// A vector representing the direction the player should move in. Magnitude should be in the range [0, 1]. Vector is in screen space.
    /// </summary>
    Vector2 movementDirection { get; }
    /// <summary>
    /// A vector representing the direction the player should face. Vector is in world space.
    /// </summary>
    Vector3 rotationDirection { get; }
    /// <summary>
    /// GetKey for the menu submission.
    /// </summary>
    /// <returns></returns>
    bool getSubmit { get; }
    /// <summary>
    /// GetKey for the menu cancellation.
    /// </summary>
    /// <returns></returns>
    bool getCancel { get; }
    /// <summary>
    /// GetKey for the start binding.
    /// </summary>
    /// <returns></returns>
    bool getStart { get; }
    /// <summary>
    /// GetKey for the player's basic attack.
    /// </summary>
    bool getBasicAttack { get; }
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

    /// <summary>
    /// GetKeyDown for the menu submission.
    /// </summary>
    /// <returns></returns>
    bool getSubmitDown { get; }
    /// <summary>
    /// GetKeyDown for the menu cancellation.
    /// </summary>
    /// <returns></returns>
    bool getCancelDown { get; }
    /// <summary>
    /// GetKeyDown for the start binding.
    /// </summary>
    /// <returns></returns>
    bool getStartDown { get; }
    /// <summary>
    /// GetKeyDown for the player's basic attack.
    /// </summary>
    bool getBasicAttackDown { get; }
    /// <summary>
    /// GetKeyDown for the player's first ability.
    /// </summary>
    /// <returns></returns>
    bool getAbility1Down { get; }
    /// <summary>
    /// GetKeyDown for the player's second ability.
    /// </summary>
    /// <returns></returns>
    bool getAbility2Down { get; }
}

/// <summary>
/// This is the script through which all final gameplay input will be handled. Child classes are for drag-drop construction.
/// </summary>
public class PlayerInputHolder : MonoBehaviour, IPlayerInputHolder
{
    public virtual IPlayerInput heldInput { get; set; }

    public Vector2 movementDirection { get { return heldInput.movementDirection; } }
    public Vector3 rotationDirection { get { return heldInput.rotationDirection; } }
    public bool getSubmit { get { return heldInput.getSubmit; } }
    public bool getCancel { get { return heldInput.getCancel; } }
    public bool getStart { get { return heldInput.getStart; } }
    public bool getBasicAttack { get { return heldInput.getBasicAttack; } }
    public bool getAbility1 { get { return heldInput.getAbility1; } }
    public bool getAbility2 { get { return heldInput.getAbility2; } }

    public bool getSubmitDown { get { return heldInput.getSubmitDown; } }
    public bool getCancelDown { get { return heldInput.getCancelDown; } }
    public bool getStartDown { get { return heldInput.getStartDown; } }
    public bool getBasicAttackDown { get { return heldInput.getBasicAttackDown; } }
    public bool getAbility1Down { get { return heldInput.getAbility1Down; } }
    public bool getAbility2Down { get { return heldInput.getAbility2Down; } }

    protected void Start()
    {
        if (heldInput != null)
        {
            heldInput.Player = this.transform;
        }
    }
}

/// <summary>
/// An input which always returns no.
/// </summary>
[System.Serializable]
public class NullInput : IPlayerInput
{
    public Transform Player { set { ;} }
    public Vector2 movementDirection { get { return Vector2.zero; } }
    public Vector3 rotationDirection { get { return Vector3.zero; } }
    public bool getSubmit { get { return false; } }
    public bool getCancel { get { return false; } }
    public bool getStart { get { return false; } }
    public bool getBasicAttack { get { return false; } }
    public bool getAbility1 { get { return false; } }
    public bool getAbility2 { get { return false; } }
    public bool getSubmitDown { get { return false; } }
    public bool getCancelDown { get { return false; } }
    public bool getStartDown { get { return false; } }
    public bool getBasicAttackDown { get { return false; } }
    public bool getAbility1Down { get { return false; } }
    public bool getAbility2Down { get { return false; } }
}