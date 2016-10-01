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
        // temp to just test spawning a boss

        if (Input.GetKeyDown(KeyCode.P)) {
            PhotonNetwork.Instantiate(Tags.Resources.Boss, new Vector3(Random.Range(-40, 40), 0.0f, Random.Range(-40, 40)), Quaternion.identity, 0);
        }
            
	}

    public void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.isMasterClient ? "You are the master client" : "You are not the master client");
        Debug.LogFormat("Ping: {0}", PhotonNetwork.GetPing());

        GameObject player = PhotonNetwork.Instantiate(playerPrefabName, Vector3.zero, Quaternion.identity, 0);
    }

}
