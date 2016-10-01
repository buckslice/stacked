using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// If the player is not a masterClient, disable this gameObject.
/// </summary>
public class MasterOnly : MonoBehaviour {
	void Start () {
        if (!PhotonNetwork.isMasterClient)
        {
            this.gameObject.SetActive(false);
        }
	}
}
