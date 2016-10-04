using UnityEngine;
using System.Collections;

public class TauntAbility : AbstractAbilityAction
{
    [SerializeField]
    protected float tauntRad = 20;

    int layermask;

    protected override void Awake()
    {
        base.Awake();
        layermask = LayerMask.GetMask(Tags.Layers.Default, Tags.Layers.Boss, Tags.Layers.Enemy);
    }

    public override void Activate()
    {
        Collider[] taunted = Physics.OverlapSphere(this.gameObject.transform.position, tauntRad, layermask);
        foreach (Collider col in taunted)
        {
            if (col.transform.root.CompareTag(Tags.Boss))
            {
                col.gameObject.GetComponent<Boss>().SetTaunt(this.transform.root.gameObject);
            }
        }
    }

    public override void ActivateWithData(object data)
    {
        Activate();
    }

    public override void ActivateRemote()
    {
        networkedActivation.ActivateRemote();
    }
}
