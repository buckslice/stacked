using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// lets other scripts easily reference useful parts of canvas layout
public class CanvasHelper : MonoBehaviour {

    public Transform floatingHealthBarGroup;

    public GameObject playerHealthBarPrefab;
    public GameObject regularHealthBarPrefab;
    public GameObject bossHealthBarPrefab;

    public CanvasScaler scaler;

}
