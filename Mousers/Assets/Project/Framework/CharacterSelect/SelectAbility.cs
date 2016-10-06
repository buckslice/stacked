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
        if (!selected && cursor.CurrentSelection != null)
        {
            print("Derp");
            if (view.isMine)
            {
                GameObject instantiatedPlayerSetup = (GameObject)Instantiate(cursor.CurrentSelection, Vector3.zero, Quaternion.identity);
                instantiatedPlayerSetup.GetComponent<PlayerSetup>().Initalize(cursor.GetComponent<PlayerInputHolder>().heldInput, cursor.playerNumber);
            }
            selected = true;
        }
    }

    public override void ActivateWithRemoteData(object data)
    {
        Activate();
    }

    public override void ActivateRemote()
    {
        networkedActivation.ActivateRemote();
    }
}
