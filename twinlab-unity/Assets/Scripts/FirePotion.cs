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
        List<Actor> actors = GetAllHitActors();
        foreach (Actor actor in actors)
            actor.TakeDamage(GetDistanceMultiplier(actor) * damage);
    }
}
