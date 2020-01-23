using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePotion : Potion
{

    public new void Start()
    {
        base.Start();
        type = Type.purple;
    }

    public override void Effect()
    {
        Debug.Log("I dont know what the fuck i should do");
        //chloroform?
    }
}
