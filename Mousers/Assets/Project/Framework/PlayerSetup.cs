using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class PlayerSetup : MonoBehaviour {

    [SerializeField]
    protected string playerPrefabName = Tags.Resources.Player;

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.isMasterClient ? "You are the master client" : "You are not the master client");
        Debug.LogFormat("Ping: {0}", PhotonNetwork.GetPing());

        GameObject player = PhotonNetwork.Instantiate(playerPrefabName, Vector3.zero, Quaternion.identity, 0);
    }
}
