using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class AddSpawnPoints : MonoBehaviour {

    static AddSpawnPoints main = null;
    public static AddSpawnPoints Main { get { return main; } }

    [SerializeField]
    protected Transform[] addSpawnPoints;
    public Transform[] SpawnPoints { get { return addSpawnPoints; } }

	void Start () {
        if(main != null) {
            Debug.LogError("There should only be one AddSpawnPoints script in any scene.");
        }

        main = this;
	}
	
	void OnDestroy() {
        if(main == this) {
            main = null;
        }
    }
}
