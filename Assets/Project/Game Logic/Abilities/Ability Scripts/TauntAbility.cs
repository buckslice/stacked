using UnityEngine;
using System.Collections;

public class TauntAbility : AbstractAbilityAction
{
    [SerializeField]
    protected float tauntRad = 20;

    int layermask;
    IDamageHolder playerReference;

    protected override void Awake()
    {
        base.Awake();
        layermask = LayerMask.GetMask(Tags.Layers.Default, Tags.Layers.Boss, Tags.Layers.Enemy);
    }

    protected override void Start()
    {
        base.Start();
        playerReference = GetComponentInParent<IDamageHolder>();
    }

    public override bool Activate(PhotonStream stream)
    {
        Collider[] taunted = Physics.OverlapSphere(this.gameObject.transform.position, tauntRad, layermask);
        foreach (Collider col in taunted)
        {
            if (col.transform.root.CompareTag(Tags.Boss))
            {
                Player player = (Player)playerReference.GetRootDamageTracker();
                col.gameObject.GetComponentInParent<BossAggro>().SetTaunt(player);
            }
        }

        return true;
    }
}
