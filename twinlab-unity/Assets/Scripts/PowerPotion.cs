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
        Debug.Log("Power stuff");
    }
}
