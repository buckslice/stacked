using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class PlayerRegistration : MonoBehaviour {

    [SerializeField]
    protected GameObject registeredPlayerPrefab;

    [SerializeField]
    protected string playerRegistrationUIPrefabName = Tags.Resources.RegistrationUI;


    [SerializeField]
    protected PlayerInputHolder[] possibleBindings;

    /// <summary>
    /// Collection of all the players who have registered. The index of a registeredPlayer is the same as the index of its binding in possibleBindings.
    /// </summary>
    /// 
    private bool[] registeredBindings; 

    private RegisteredPlayer[] registeredPlayers;

    void Start()
    {
        registeredPlayers = new RegisteredPlayer[4];
        registeredBindings = new bool[possibleBindings.Length];
    }

    void Update()
    {
        for (int i=0; i<possibleBindings.Length; i++)
        {
            if (possibleBindings[i].getRegistering && !registeredBindings[i])
            {
                for (int j = 0; j<registeredPlayers.Length; j++)
                {
                    if (registeredPlayers[j]== null)
                    {
                        GameObject instantiatedRegisteredPlayer = (GameObject)Instantiate(registeredPlayerPrefab, Vector3.zero, Quaternion.identity);
                        registeredPlayers[j] = instantiatedRegisteredPlayer.GetComponent<RegisteredPlayer>();
                        registeredPlayers[j].Initalize(possibleBindings[i].heldInput, j);

                        PhotonNetwork.Instantiate(playerRegistrationUIPrefabName, new Vector3(0,0,0), Quaternion.identity, 0);
                        //TODO: spawn visuals as well, using PhotonNetwork.Instantiate to show on all connected computers
                        registeredBindings[i] = true;
                        break;
                    }
                }
            }
        }
    }
}
