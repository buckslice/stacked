using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A stackable which cannot carry objects on top of it
/// </summary>
public class LightStackable : Stackable {

    public override void Grab(Stackable target) {
        //do nothing; we are unable to grab
    }
}
