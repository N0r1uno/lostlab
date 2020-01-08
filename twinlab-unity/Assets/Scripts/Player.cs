using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{

    void Update()
    {
        input.Get();
        ApplyMovement();
        ApplyAnimation();
    }

    override
    public bool IsPlayer => true; 
}
