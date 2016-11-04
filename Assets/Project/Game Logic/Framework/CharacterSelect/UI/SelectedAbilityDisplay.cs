using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class SelectedAbilityDisplay : MonoBehaviour {

    [SerializeField]
    protected Text bindingLabel;

    GameObject visuals;

    public void Initialize(string bindingText, GameObject originalVisuals) {
        this.bindingLabel.text = bindingText;

        if (visuals != null) {
            Destroy(visuals);
        }

        visuals = Instantiate(originalVisuals, this.transform) as GameObject;
        RectTransform visualsTransform = visuals.GetComponent<RectTransform>();
        visualsTransform.Reset();
        visualsTransform.SetAsFirstSibling();
    }
}
