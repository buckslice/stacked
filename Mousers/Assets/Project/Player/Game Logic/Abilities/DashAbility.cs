using UnityEngine;
using System.Collections;

public class DashAbility : AbstractAbilityAction
{

    public int dashDistance = 10;

    protected override void Start()
    {
        base.Start();
    }
    public override void Activate()
    {
        transform.root.GetComponent<Rigidbody>().position = (transform.root.position + transform.root.forward * dashDistance);
    }
}
