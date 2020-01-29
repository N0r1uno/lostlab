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
        List<Actor> actors = GetAllHitActors();
        foreach (Actor actor in actors)
        {
            float distanceMultiplicator = GetDistanceMultiplier(actor);
            actor.TakeDamage(distanceMultiplicator * damage);

            Freezable f = actor.GetComponent<Freezable>();
            if (f != null)
                f.Freeze(distanceMultiplicator * maxFreezeTime);
        }
            
        Debug.Log("Freeze stuff");
    }
}
