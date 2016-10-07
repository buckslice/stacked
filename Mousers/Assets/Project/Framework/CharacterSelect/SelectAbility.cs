﻿using UnityEngine;
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

    public override bool Activate(PhotonStream stream)
    {
        if (!selected && cursor.CurrentSelection != null) {
            print("Derp");

            if (stream.isWriting) {
                GameObject instantiatedPlayerSetup = (GameObject)Instantiate(cursor.CurrentSelection, Vector3.zero, Quaternion.identity);
                instantiatedPlayerSetup.GetComponent<PlayerSetup>().Initalize(cursor.GetComponent<PlayerInputHolder>().heldInput, cursor.playerNumber);
            }

            selected = true;

            return true;
        } else {
            return false;
        }
    }
}
