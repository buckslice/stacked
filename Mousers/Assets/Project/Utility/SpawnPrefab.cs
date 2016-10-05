using UnityEngine;
using System.Collections;

// this script automatically spawns a prefab after the level is loaded
public class SpawnPrefab : MonoBehaviour {
    
    public Object prefab;
    public string gameObjectName;
    public float spawnDelay = 5.0f;

	// Use this for initialization
	void Start () {
        if (prefab) {
            Invoke("Spawn", spawnDelay);
        }
	}
	
	void Spawn() {
        GameObject go = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
        if (gameObjectName.Length > 0) {
            go.name = gameObjectName;
        }
    }
}
