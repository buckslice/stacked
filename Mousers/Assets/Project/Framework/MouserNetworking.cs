using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Bean interacting with Photon Networking.
/// </summary>
public class MouserNetworking : MonoBehaviour {

    static MouserNetworking main = null;
    public static MouserNetworking Main { get { return main; } }

    [SerializeField]
    protected MouserNetworkingMode networkingMode;

    /// <summary>
    /// Version number used to filter rooms and prevent version mis-matches.
    /// </summary>
    [SerializeField]
    protected string version = "1";

	void Awake () {
        if (main != null)
        {
            DestroyImmediate(this.transform.root.gameObject);
            return;
        }
        else
        {
            main = this;
            DontDestroyOnLoad(this.transform.root.gameObject);
        }

        PhotonNetwork.offlineMode = networkingMode == MouserNetworkingMode.ONEMACHINE;
        PhotonNetwork.autoJoinLobby = false; //might want to change this when we leave prototyping
        PhotonNetwork.automaticallySyncScene = true;

        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }
	}

    /// <summary>
    /// Wrapper for PhotonNetwork.LoadLevel(), intended to be used directly by Buttons. This is intended for development only; for non-development use, call PhotonNetwork.LoadLevel(int) directly.
    /// </summary>
    /// <param name="levelName">Name of the level to load.</param>
    public void LoadLevel(string levelName)
    {
        PhotonNetwork.LoadLevel(levelName);
    }

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
    }

    public void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.isMasterClient ? "You are the master client" : "You are not the master client");
        Debug.LogFormat("Ping: {0}", PhotonNetwork.GetPing());
    }
}

/// <summary>
/// The mode of networking we will be using for the current game session.
/// </summary>
public enum MouserNetworkingMode
{
    ONEMACHINE, ONLINE
};
