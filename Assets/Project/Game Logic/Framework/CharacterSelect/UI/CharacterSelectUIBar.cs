using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelectUIBar : MonoBehaviour {
    
    [SerializeField]
    protected Text tooltip;

    public void setTooltip(string keyName) {
        tooltip.text = string.Format("{0} to select", keyName);
    }
}
