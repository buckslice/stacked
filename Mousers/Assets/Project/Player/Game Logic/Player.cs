using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Identifies the object as a player
/// </summary>
public class Player : MonoBehaviour {

    [SerializeField]
    protected float playerID = -1;

    public float PlayerID { get { return playerID; } }

    /// <summary>
    /// Constructor-like function to set up this class.
    /// </summary>
    /// <param name="playerID"></param>
    public void Initialize(float playerID)
    {
        this.playerID = playerID;
    }
}
