using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An action which triggers as a targeted action via raycasting against an area.
/// </summary>
[RequireComponent(typeof(IShape))]
public class AreaCast : AbstractAbilityAction, ITargetedAbilityTrigger {

    [SerializeField]
    protected LayerMask layermask;

    IShape shape;

    public event UntargetedAbilityTrigger abilityTriggerEvent = delegate { };
    public event TargetedTrigger targetedAbilityTriggerEvent = (target) => { };

    protected override void Start() {
        base.Start();
        Assert.IsTrue(GetComponents<IShape>().Length == 1);
        shape = GetComponent<IShape>();
    }

    public override bool Activate(PhotonStream stream) {
        foreach (Collider collider in shape.Cast(layermask)) {
            targetedAbilityTriggerEvent(collider.gameObject);
        }

        return false; //any networking action is handled by the child ativation
    }
}

public interface IShape {
    Collider[] Cast(LayerMask layermask);
}