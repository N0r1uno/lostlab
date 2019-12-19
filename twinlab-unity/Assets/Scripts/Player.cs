using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    void Update()
    {
        input.Get();
        ApplyMovement();
    }

    override
    public bool IsPlayer => true; 
}
