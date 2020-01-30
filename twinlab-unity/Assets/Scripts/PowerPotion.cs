using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPotion : Potion
{

    public new void Start()
    {
        base.Start();
        type = Type.power;
    }

    public override void Effect()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, range);
        for (int i = 0; i < collisions.Length; i++)
        {
            Cable cable = collisions[i].GetComponent<Cable>();
            if (cable != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, collisions[i].transform.position - transform.position);
                if (hit.collider.gameObject.Equals(collisions[i].gameObject))
                {
                    cable.GiveSignal();
                }
            }
        }
    }
}
