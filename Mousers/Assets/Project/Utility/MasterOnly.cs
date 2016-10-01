using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// If the player is not a masterClient, disable this gameObject.
/// </summary>
public class MasterOnly : MonoBehaviour {
    public void OnJoinedRoom()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            this.gameObject.SetActive(false);
        }
	}
}
