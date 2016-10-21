using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public interface ISelection {
    bool CanSelect();
    bool Select(ISelectable data);

    bool CanDeselect();
    bool Deselect();

    bool Ready { get; }
}

public interface ISelectable { }

public class SelectAbility : UntargetedAbilityConstraint {

    ISelection selection;

    PointerEventData pointer = new PointerEventData(EventSystem.current);
    List<RaycastResult> results = new List<RaycastResult>();
    
    protected override void Start()
    {
        base.Start();
        selection = GetComponentInParent<ISelection>();
    }

    public override void Activate() {
        throw new System.NotImplementedException();
    }

    public override bool isAbilityActivatible() {
        return selection.CanSelect();
    }

    public override bool Activate(PhotonStream stream)
    {
        pointer.position = new Vector3(transform.position.x, transform.position.y);
        results.Clear();

        EventSystem.current.RaycastAll(pointer, results);

        foreach (RaycastResult hit in results) {
            ISelectable selectable = hit.gameObject.GetComponent<ISelectable>();

            if (selectable != null) {
                return selection.Select(selectable);
            }
        }
        return false;
    }
}
