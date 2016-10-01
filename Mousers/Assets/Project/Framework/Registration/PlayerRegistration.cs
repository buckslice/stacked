using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class PlayerRegistration : MonoBehaviour {

    [SerializeField]
    protected GameObject playerSetupPrefab;


    [SerializeField]
    protected PlayerInputHolder[] possibleBindings;

    /// <summary>
    /// Collection of all the players who have registered. The index of a registeredPlayer is the same as the index of its binding in possibleBindings.
    /// </summary>
    PlayerSetup[] registeredPlayers;

    void Start()
    {
        registeredPlayers = new PlayerSetup[possibleBindings.Length];
    }

    void Update()
    {
        for (int i = 0; i < possibleBindings.Length; i++)
        {
            if (possibleBindings[i].getRegistering && registeredPlayers[i] == null)
            {
                GameObject instantiatedPlayerSetup = (GameObject)Instantiate(playerSetupPrefab, Vector3.zero, Quaternion.identity);
                registeredPlayers[i] = instantiatedPlayerSetup.GetComponent<PlayerSetup>();
                registeredPlayers[i].Initalize(possibleBindings[i].heldInput);

                    //TODO: spawn visuals as well, using PhotonNetwork.Instantiate to show on all connected computers
            }
        }
    }
}
