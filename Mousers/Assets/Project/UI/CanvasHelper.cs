using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// lets other scripts easily reference useful parts of canvas layout
public class CanvasHelper : MonoBehaviour {

    public Transform playerHealthBarGroup;
    public Transform bossHealthBarGroup;
    public Transform floatingHealthBarGroup;

    public Object playerHealthBarPrefab;
    public Object bossHealthBarPrefab;
    public Object floatingHealthBarPrefab;

    public CanvasScaler scaler;
    
}
