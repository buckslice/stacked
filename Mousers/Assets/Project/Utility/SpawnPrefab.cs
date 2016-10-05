using UnityEngine;
using System.Collections;

// this script automatically spawns a prefab after the level is loaded
public class SpawnPrefab : MonoBehaviour {
    
    public Object prefab;
    public float spawnDelay = 5.0f;

	// Use this for initialization
	void Start () {
        if (prefab) {
            Invoke("Spawn", spawnDelay);
        }
	}
	
	void Spawn() {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
