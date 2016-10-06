using UnityEngine;
using System.Collections;

public class TauntAbility : AbstractAbilityAction
{
    [SerializeField]
    protected float tauntRad = 20;

    int layermask;
    Player playerReference;

    protected override void Awake()
    {
        base.Awake();
        layermask = LayerMask.GetMask(Tags.Layers.Default, Tags.Layers.Boss, Tags.Layers.Enemy);
    }

    protected override void Start()
    {
        base.Start();
        playerReference = GetComponentInParent<Player>();
    }

    public override void Activate()
    {
        Collider[] taunted = Physics.OverlapSphere(this.gameObject.transform.position, tauntRad, layermask);
        foreach (Collider col in taunted)
        {
            if (col.transform.root.CompareTag(Tags.Boss))
            {
                col.gameObject.GetComponentInParent<BossAggro>().SetTaunt(playerReference);
            }
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
