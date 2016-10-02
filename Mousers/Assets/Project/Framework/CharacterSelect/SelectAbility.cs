using UnityEngine;
using System.Collections;

public class SelectAbility : AbstractAbilityAction {
    private bool selected;
    private CharacterSelectCursor cursor;
    protected override void Start()
    {
        base.Start();
        cursor = GetComponentInParent<CharacterSelectCursor>();
        selected = false;
    }
    public override void Activate()
    {
        if (!selected && cursor.getCurrentSelection() != null)
        {
            print("Derp");
            GameObject instantiatedPlayerSetup = (GameObject)Instantiate(cursor.getCurrentSelection(), Vector3.zero, Quaternion.identity);
            instantiatedPlayerSetup.GetComponent<PlayerSetup>().Initalize(cursor.GetComponent<PlayerInputHolder>().heldInput, cursor.playerNumber);
            selected = true;
        }
    }
}
