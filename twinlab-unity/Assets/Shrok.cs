using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrok : Actor
{
    [Header("Shrok")]
    public float range;
    private Player target;

    void Start()
    {
        Initialize();
        target = FindObjectOfType<Player>();
    }

    void Update()
    {
        CalculateInput();
        ApplyMovement();
        //ApplyAnimation();
    }

    public void CalculateInput()
    {
        //TODO
        if (Vector2.Distance(transform.position, target.transform.position) <= range)
        {
            Debug.Log(target.transform.position.x - transform.position.x);
            if (target.transform.position.x - transform.position.x > 0)
                input.SetHorizontal(1);
            else
                input.SetHorizontal(-1);
        }
        else input.SetHorizontal(0);
    }      
}
