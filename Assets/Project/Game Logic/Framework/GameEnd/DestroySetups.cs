using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// To be used in victory/defeat scenes to destroy all player setup data (so the can select new ones in character select)
/// </summary>
public class DestroySetups : MonoBehaviour {

    public bool destroyPlayerSetups = true;
    public bool destroyBossSetups = true;

    public void Activate() {
        if (destroyPlayerSetups) {
            PlayerSetup.DestroyAllPlayerSetups();
        }

        if (destroyBossSetups) {
            BossSetup.DestroyAllBossSetups();
        }
    }

}
