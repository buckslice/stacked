using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class PlayerRegistration : MonoBehaviour {

    class RegisteredPlayerGrouping
    {
        //TODO: add references to the containing RegisteredPlayerGrouping to its component objects
        public readonly RegisteredPlayer registeredPlayer;
        public readonly GameObject registrationUI;
        public RegisteredPlayerGrouping(RegisteredPlayer registeredPlayer, GameObject registrationUI)
        {
            this.registeredPlayer = registeredPlayer;
            this.registrationUI = registrationUI;
        }
    }

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

    private RegisteredPlayerGrouping[] registeredPlayers;

    void Start()
    {
        registeredPlayers = new RegisteredPlayerGrouping[4];
        registeredBindings = new bool[possibleBindings.Length];
    }

    void Update()
    {
        for (int i=0; i<possibleBindings.Length; i++)
        {
            if (possibleBindings[i].getRegistering && !registeredBindings[i])
            {
                for (int j = 0; j < registeredPlayers.Length; j++)
                {
                    if (registeredPlayers[j] == null)
                    {
                        GameObject instantiatedRegisteredPlayer = (GameObject)Instantiate(registeredPlayerPrefab, Vector3.zero, Quaternion.identity);
                        RegisteredPlayer registeredPlayer = instantiatedRegisteredPlayer.GetComponent<RegisteredPlayer>();
                        registeredPlayer.Initalize(possibleBindings[i].heldInput, j);

                        GameObject instantiatedRegistrationUI = PhotonNetwork.Instantiate(playerRegistrationUIPrefabName, new Vector3(0, 0, 0), Quaternion.identity, 0);

                        registeredPlayers[j] = new RegisteredPlayerGrouping(registeredPlayer, instantiatedRegistrationUI);

                        registeredBindings[i] = true;
                        break;
                    }
                }
            }
        }
    }
}
