using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePotion : Potion
{
    [Header("Freeze Potion Spcific")]
    public float maxFreezeTime = 2f;

    public new void Start()
    {
        base.Start();
        type = Type.freeze;
    }

    public override void Effect()
    {
        List<Actor> actors = getAllHitActors();
        foreach (Actor actor in actors)
        {
            float damageMultiplicator = 1 - (Vector3.Distance(actor.transform.position, transform.position) / range);
            actor.TakeDamage(damageMultiplicator * damage);

            Freezable f = actor.GetComponent<Freezable>();
            if (f != null)
                f.Freeze(damageMultiplicator * maxFreezeTime);
        }
            
        Debug.Log("Freeze stuff");
    }
}
