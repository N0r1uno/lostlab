using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : Potion
{

    public new void Start()
    {
        base.Start();
        type = Type.poison;
    }

    public override void Effect()
    {
        Debug.Log("Poison stuff");
    }
}
