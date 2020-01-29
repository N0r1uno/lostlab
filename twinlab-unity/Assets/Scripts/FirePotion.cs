using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePotion : Potion
{

    public new void Start()
    {
        base.Start();
        type = Type.fire;
    }

    public override void Effect()
    {
        List<Actor> actors = getAllHitActors();
        foreach (Actor actor in actors)
        {
            float damageMultiplicator = 1-(Vector3.Distance(actor.transform.position, transform.position)/range);
            actor.TakeDamage(damageMultiplicator * damage);
        }
    }
}
