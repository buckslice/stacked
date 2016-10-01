using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class to hold player data, and to spawn said player after the next scene change.
/// </summary>
public class PlayerSetup : MonoBehaviour {

    [SerializeField]
    protected string playerPrefabName = Tags.Resources.Player;
    //public GameObject player1SpawnPoint; //TODO: add a class for spawn points, which register themselves in a static dictionary

    [SerializeField]
    private GameObject healthBarPrefab;

    void Start () { //move to be awake
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        DontDestroyOnLoad(this.transform.root.gameObject);
	}

    void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //TODO: maybe make sure it's the right scene?
        CreatePlayer();
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        Destroy(this.transform.root.gameObject);
        //player was created, our job is done. May want to change this so that the player's spawning data is persisted.
    }
	
	void Update () {
        // temp to just test spawning a boss

        if (Input.GetKeyDown(KeyCode.P)) {
            PhotonNetwork.Instantiate(Tags.Resources.Boss, new Vector3(Random.Range(-40, 40), 0.0f, Random.Range(-40, 40)), Quaternion.identity, 0);
        }
            
	}

    public void CreatePlayer()
    {
        // spawn player and healthbar and link them up
        GameObject player = PhotonNetwork.Instantiate(playerPrefabName, Vector3.zero, Quaternion.identity, 0); //TODO: use spawn point
        Transform canvasRoot = GameObject.FindGameObjectWithTag(Tags.CanvasRoot).transform;
        Transform playerHealthBarGroup = canvasRoot.Find(Tags.UIPaths.PlayerHealthBarGroup);
        GameObject healthBar = (GameObject)Instantiate(healthBarPrefab, playerHealthBarGroup);
        HealthBar bar = healthBar.GetComponent<HealthBar>();
        player.GetComponent<Health>().bar = bar;

        player.AddComponent<DerpAbility>();
    }
}
