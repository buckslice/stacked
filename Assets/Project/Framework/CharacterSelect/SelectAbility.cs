using UnityEngine;
using System.Collections;

public class SelectAbility : AbstractAbilityAction {
    bool _selected = false;
    private bool selected {
        get { return _selected; }
        set {
            _selected = value;
            if (_selected) {
                visuals.color = Color.green;
            }
        }
    }
    private CharacterSelectCursor cursor;
    PlayerInputHolder inputHolder;
    SpriteRenderer visuals;

    protected override void Start()
    {
        base.Start();
        cursor = GetComponentInParent<CharacterSelectCursor>();
        inputHolder = GetComponentInParent<PlayerInputHolder>();
        visuals = GetComponentInParent<SpriteRenderer>();
        selected = false;
    }

    public override bool Activate(PhotonStream stream)
    {
        if (!selected && cursor.CurrentSelection != null) {
            print("Derp");

            if (stream.isWriting) {
                GameObject instantiatedPlayerSetup = (GameObject)Instantiate(cursor.CurrentSelection, Vector3.zero, Quaternion.identity);
                instantiatedPlayerSetup.GetComponent<PlayerSetup>().Initalize(inputHolder.heldInput, cursor.playerNumber);
            }

            selected = true;

            return true;
        } else {
            return false;
        }
    }
}
