using UnityEngine;
using System.Collections;

public class TauntAbility : AbstractAbilityAction
{

    public float tauntRad = 20;
    private Collider[] taunted;

    protected override void Start()
    {
        base.Start();
    }

    public override void Activate()
    {
        Debug.Log("Taunting");
        taunted = Physics.OverlapSphere(this.gameObject.transform.position, tauntRad);
        Debug.Log(taunted.Length);
        foreach(Collider col in taunted){
            if (col.transform.root.CompareTag(Tags.Boss))
            {
                col.gameObject.GetComponent<Boss>().GetTaunted(this.transform.root.gameObject);
            }
            else
            {
                Debug.Log(col.transform.root.tag);
            }
        }
    }

}
