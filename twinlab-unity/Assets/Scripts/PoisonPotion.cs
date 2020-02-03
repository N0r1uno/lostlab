using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : Potion
{
    public float time;
    public int takes;

    public new void Start()
    {
        base.Start();
        type = Type.poison;
    }

    public override void Effect()
    {
        List<Actor> actors = GetAllHitActors();
        foreach (Actor actor in actors)
        {
            actor.GetComponent<PoisonEffect>().Poison(time, damage, takes);
        }
    }
}
