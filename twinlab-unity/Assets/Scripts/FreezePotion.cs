using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePotion : Potion
{

    public new void Start()
    {
        base.Start();
        type = Type.freeze;
    }

    public override void Effect()
    {
        Debug.Log("Freeze stuff");
    }
}
