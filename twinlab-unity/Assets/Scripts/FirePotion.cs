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
        foreach (Actor a in actors)
            a.TakeDamage(damage);
    }
}
