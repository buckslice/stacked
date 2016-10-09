using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An action which spawns a pooled object
/// </summary>
public class InstantiateAbility : AbstractAbilityAction {

    PhotonView view;

    [SerializeField]
    protected string prefabName;

    protected override void Start() {
        base.Start();
        view = GetComponentInParent<PhotonView>();
    }

    public override bool Activate(PhotonStream stream)
    {
        if (view.isMine) {
            PhotonNetwork.Instantiate(prefabName, transform.position, transform.rotation, 0, new object[] { view.viewID });
        }
        return false;
    }
}
