using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public interface ISelection {
    bool CanSelect();
    /// <summary>
    /// Return result is if the action should be sent over the network
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    bool Select(ISelectable data);

    bool CanDeselect();
    bool Deselect();

    bool Ready { get; }
    IPlayerInputHolder Input { get; }
}

public interface ISelectable { }

public class SelectAbility : UntargetedAbilityConstraint {

    ISelection selection;
    RectTransform canvas;

    PointerEventData pointer = new PointerEventData(EventSystem.current);
    List<RaycastResult> results = new List<RaycastResult>();
    
    protected override void Start()
    {
        base.Start();
        selection = GetComponentInParent<ISelection>();
        canvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    public override void Activate() {
        throw new System.NotImplementedException();
    }

    public override bool isAbilityActivatible() {
        return selection.CanSelect();
    }

    public override bool Activate(PhotonStream stream)
    {
        Vector2 position;
        if (stream.isWriting) {
            position = transform.position;
        } else {
            position = canvas.TransformPoint((Vector2)stream.ReceiveNext());
        }
        pointer.position = position;

        results.Clear();

        EventSystem.current.RaycastAll(pointer, results);

        foreach (RaycastResult hit in results) {
            ISelectable selectable = hit.gameObject.GetComponentInParent<ISelectable>();

            if (selectable != null) {
                bool result = selection.Select(selectable);

                if (result && stream.isWriting) { //only send if we need to
                    stream.SendNext((Vector2)canvas.InverseTransformPoint(position));
                }

                return result;
            }
        }
        return false;
    }
}
